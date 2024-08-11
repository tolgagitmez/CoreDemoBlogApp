using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class NewslatterController : Controller
	{
		NewslatterManager nm = new NewslatterManager(new EfNewslatterRepository());
		[HttpGet]
		public PartialViewResult SubscribeMail()
		{
			return PartialView();
		}

		[HttpPost]
		public IActionResult SubscribeMail(Newslatter p)
		{
			p.MailStatus = true;
			nm.AddNewsletter(p);
			return PartialView();
		}
	}
}
