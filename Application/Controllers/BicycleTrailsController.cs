using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using System.Net;
using Models;
using Microsoft.AspNetCore.Authorization;
using Storage.Interfaces;

namespace TrasyRowerowe.Controllers
{
    [Authorize(Policy = "User")]
    public class BicycleTrailsController : Controller
    {
        private readonly IDataBaseService _dataBaseService;
        public BicycleTrailsController(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }
        public IActionResult ShowListOfTrails(int? page)
        {
            int numberOfpage = (page ?? 1) - 1;
            IPagedList<DetailedTrail> newPage = new StaticPagedList<DetailedTrail>(_dataBaseService.GetListOfTrailsForUser(numberOfpage),
                numberOfpage + 1, 5, _dataBaseService.GetAmountOfSites());
            return View(newPage);
        }
        public IActionResult ReturnSingleTrail(int id)
        {
            return View(_dataBaseService.ReturnSingleTrailFromDb(id));
        }
        [HttpPost]
        public IActionResult ReturnSingleTrail(string startx, string starty, string end, int id)
        {
            using (WebClient client = new WebClient())
            {
                string lan = end.Split(',')[1];
                string Lon = end.Split(',')[0];
                var trail = client.DownloadString($@"http://router.project-osrm.org/route/v1/driving/{starty},{startx};{Lon},{lan}?exclude=motorway");
                ViewBag.trail = trail;
                return View(_dataBaseService.ReturnSingleTrailFromDb(id));
            }
        }
    }
}