using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult ErrorService(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return NotFound("Nie znaleziono strony");
                    break;
            }
            return View();
        }
    }
}