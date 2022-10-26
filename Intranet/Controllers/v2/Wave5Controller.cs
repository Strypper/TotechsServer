using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controllers.v2
{
    [Route("/api/v2/[controller]/[action]")]
    public class Wave5Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
