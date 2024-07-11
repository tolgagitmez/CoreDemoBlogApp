using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{
    public class MessageController : Controller
    {
     
        Message2Menager mm = new Message2Menager(new EfMessage2Repository());
        [AllowAnonymous]
        public IActionResult InBox()
        {
            int id = 4;
            var values = mm.GetInboxListByWriter(id);
            return View(values); ;
        }
        [AllowAnonymous]
        public IActionResult MessageDetails(int id)
        {
            var value = mm.GetById(id);

           
            return View(value);

        }
    }
}
