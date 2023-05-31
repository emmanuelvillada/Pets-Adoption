using Microsoft.AspNetCore.Mvc;

namespace Pets_Adpotion.Controllers
{
    public class AdopterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
