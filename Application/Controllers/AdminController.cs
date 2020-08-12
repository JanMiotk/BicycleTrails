using System.Collections.Generic;
using System.IO;
using DirectoriesCreator.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Serializer.Interfaces;
using Storage.Interfaces;
using X.PagedList;
using System.Threading.Tasks;
using System.Linq;

namespace TrasyRowerowe.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly ISerializer _serializer;
        private readonly IDirectoryService _directoryService;
        private IDataBaseService _dataBaseService;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AdminController(ISerializer serializer, IDirectoryService directoryService, IDataBaseService dataBaseService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _serializer = serializer;
            _directoryService = directoryService;
            _dataBaseService = dataBaseService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoadDataIntoDatabase(string path)
        {
            for (int i = 1; i <= new DirectoryInfo(path).GetFiles().Length; i++)
            {
                List<DetailedTrail> trails;

                try
                {
                    trails = _serializer.Deserialize<DetailedTrail>(i, path, "ListOfTrails");
                }
                catch
                {
                    continue;
                }
                _dataBaseService.LoadToDatabase(trails);
                trails = null;
            }
            return RedirectToAction("Index");
        }
        public IActionResult ReturnFilesToLoad()
        {
            return View(_directoryService.ReturnPathDirectotiesToLoad());
        }
        public IActionResult ReturnUsersList()
        {
            return View(_userManager.Users);
        }
        public IActionResult ReturnTrailsFromDataBase(int? page)
        {
            int numberOfpage = (page ?? 1) - 1;
            IPagedList<DetailedTrail> newPage = new StaticPagedList<DetailedTrail>(_dataBaseService.GetListOfTrails(numberOfpage),
                numberOfpage + 1, 10, _dataBaseService.GetAmountOfSites());
            return View(newPage);
        }
        public IActionResult DeleteTrail(int id)
        {
            _dataBaseService.DeleteRecord(id);
            return RedirectToAction("ReturnTrailsFromDataBase");
        }

        [Authorize(Policy = "SuperAdmin")]
        public async Task<IActionResult>  DeleteUser(string id)
        {
            var redirectUrl = Url.Action("GetTokenFromGoogle", "Admin", new {id });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> AuthorizeAction (string id)
        {

            ExternalLoginInfo externalinformations = await _signInManager.GetExternalLoginInfoAsync();

            var role = externalinformations.Principal.Claims.Last();

            if (role != null && role.Value.Equals("student.jan.miotk@gmail.com"))
            {
                var user = await _userManager.FindByIdAsync(id);
                IdentityResult identityResult = await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("ReturnUsersList");

        }
    }
}