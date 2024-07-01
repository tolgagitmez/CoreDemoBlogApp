using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class NewslatterController : Controller
	{
		NewslatterManager nm = new NewslatterManager(new EfNewslatterRepository());
		[HttpGet]
		public PartialViewResult SubscribeMail()
		{
			return PartialView();
		}

		[HttpPost]
		public PartialViewResult SubscribeMail(Newslatter p)
		{
			p.MailStatus = true;
			nm.AddNewsletter(p);
			return PartialView();
		}
	}
}
