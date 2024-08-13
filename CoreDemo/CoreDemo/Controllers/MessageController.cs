using BusinessLayer.Concrete;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{
    public class MessageController : Controller
    {
     
        Message2Menager mm = new Message2Menager(new EfMessage2Repository());
        Context c = new Context();
        public IActionResult InBox()
        {
            
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerID);
            return View(values); ;
        }
        [AllowAnonymous]

        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerID);
            return View(values);
        }
        public IActionResult MessageDetails(int id)
        {
            var value = mm.GetById(id);

           
            return View(value);

        }

        [HttpGet]
        public IActionResult SendMessage()
        {
            
            return View();
        
        }

        [HttpPost]

        public IActionResult SendMessage(SenderMessageViewModel p)
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            Message2 m = new Message2();
            var receiverID = c.Writers.Where(x => x.WriterMail == p.Receiver).Select(y => writerID).FirstOrDefault();
            m.ReceiverID = receiverID;
            m.Subject = p.Subject;
            m.MessageStatus = true;
            m.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            mm.TAdd(m);
            return RedirectToAction("Inbox");
        }
    }
}
