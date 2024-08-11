using CoreDemo.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreDemo.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(UserSignInViewModel p)
		{
			if (ModelState.IsValid)
			{
				// Kullanıcıyı UserManager ile bul
				var user = await _userManager.FindByNameAsync(p.username);

				if (user == null)
				{
					// Kullanıcı bulunamadıysa hata mesajı gösterebilirsiniz
					ModelState.AddModelError("", "Kullanıcı bulunamadı.");
					return View();
				}

				// Kullanıcının kilitli olup olmadığını kontrol et
				if (await _userManager.IsLockedOutAsync(user))
				{
					ModelState.AddModelError("", "Hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
					return View();
				}

				// Manuel olarak parolayı doğrula
				if (!await _userManager.CheckPasswordAsync(user, p.password))
				{
					ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
					return View();
				}

				// PasswordSignInAsync ile oturum açmayı dene
				var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);

				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Dashboard");
				}
				else if (result.IsLockedOut)
				{
					ModelState.AddModelError("", "Hesabınız kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
				}
				else if (result.IsNotAllowed)
				{
					ModelState.AddModelError("", "Giriş yapmanıza izin verilmiyor.");
				}
				else if (result.RequiresTwoFactor)
				{
					// İki faktörlü kimlik doğrulama gerekli ise yönlendirme yapabilirsiniz
					return RedirectToAction("SendCode", new { ReturnUrl = "/" });
				}
				else
				{
					ModelState.AddModelError("", "Geçersiz giriş denemesi.");
				}
			}

			return View();
		}

		//[HttpPost]
		//public async Task<IActionResult> Index(Writer p)
		//{
		//	Context c = new Context();
		//	var datavalue = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
		//	if (datavalue != null)
		//	{
		//		var claims = new List<Claim>
		//		{
		//			new Claim(ClaimTypes.Name,p.WriterMail)
		//		};
		//		var useridentity = new ClaimsIdentity(claims, "a");
		//		ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
		//		await HttpContext.SignInAsync(principal);
		//		return RedirectToAction("Index", "Dashboard");
		//	}
		//	else
		//	{
		//		return View();
		//	}
		//}

	}
}
