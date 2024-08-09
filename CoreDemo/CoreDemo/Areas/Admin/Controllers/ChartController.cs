using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryChart()
        {
            List<CategoryClass> list = new List<CategoryClass>();
            list.Add(new CategoryClass
            {
                categorycount = 10,
                categoryname = "Teknoloji"
            });
            list.Add(new CategoryClass
            {
                categorycount = 14,
                categoryname = "Yazılım"
            });
            list.Add(new CategoryClass
            {
                categorycount = 5,
                categoryname = "Spor"
            });
            return Json(new { jsonlist = list });
        }
    }
}
