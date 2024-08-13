using BusinessLayer.Concrete;
using CoreDemo.Areas.Admin.Models;
using CoreDemo.Models;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {

        Message2Menager mm = new Message2Menager(new EfMessage2Repository());
        Context c = new Context();
        public IActionResult Inbox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetInboxListByWriter(writerID);
            return View(values); ; ;
        }

        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = c.Users.Where(x => x.UserName == username).Select(y => y.Email).FirstOrDefault();
            var writerID = c.Writers.Where(x => x.WriterMail == usermail).Select(y => y.WriterID).FirstOrDefault();
            var values = mm.GetSendBoxListByWriter(writerID);
            return View(values);
        }

        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ComposeMessage(AdminSenderMessageViewModel p)
        {

            var writerID = c.Users.Where(x => x.UserName == User.Identity.Name)
                      .Join(c.Writers,
                            u => u.Email,
                            w => w.WriterMail,
                            (u, w) => w.WriterID)
                      .FirstOrDefault();
            Message2 m = new Message2();
            m.SenderID = writerID;
            var receiverID = c.Writers.Where(x => x.WriterMail == p.Receiver).Select(y => writerID).FirstOrDefault();
            m.ReceiverID = receiverID;
            m.Subject = p.Subject;
            m.MessageStatus = true;
            m.MessageDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            mm.TAdd(m);
            return RedirectToAction("SendBox");
        }
    }
}
