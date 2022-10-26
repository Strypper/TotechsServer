using Microsoft.AspNetCore.Mvc;

namespace Intranet.Controllers.v1
{
    public class Wave5Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
