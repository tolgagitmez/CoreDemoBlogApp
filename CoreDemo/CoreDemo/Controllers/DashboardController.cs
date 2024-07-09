using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    
    public class DashboardController : Controller
    {
        BlogManager bm = new BlogManager(new EfBlogRepository());
        [AllowAnonymous]
        public IActionResult Index()
        {
            Context c = new Context();
            ViewBag.v1=c.Blogs.Count().ToString();
            ViewBag.v2=c.Blogs.Where(x => x.WriterID == 4).Count().ToString();
            ViewBag.v3 = c.Categories.Count().ToString();
            return View();
        }
    }
}
