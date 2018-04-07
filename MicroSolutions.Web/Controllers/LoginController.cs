using NLog;
using System;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
	[AllowAnonymous]
	public class LoginController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		////
		//// GET: /Account/Login
		[AllowAnonymous]
		public ActionResult Index(string returnUrl)
		{
			//ViewBag.ReturnUrl = returnUrl;
			return View();
		}

		////
		//// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public ActionResult Index(LoginModel model, string returnUrl)
		{
			try
			{
				if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
				{
					MvcApplication.CurruntUser = User.Identity.Name;
					return RedirectToAction("Index", "Home");
				}

				ModelState.AddModelError("", "The user name or password provided is incorrect.");

				return View(model);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in login -> index: " + ex.Message);
				throw;
			}	
		}

		public ActionResult Register()
		{
			try
			{
				if (!WebSecurity.UserExists("user1")) WebSecurity.CreateUserAndAccount("user1", "abcABC123@@@");
				//if (!WebSecurity.UserExists("User")) WebSecurity.CreateUserAndAccount("User", "User@#123");
				//if (!WebSecurity.UserExists("Audit")) WebSecurity.CreateUserAndAccount("Audit", "Audit@#123");
				//Roles.CreateRole("administrator");
				//Roles.CreateRole("user");
				//Roles.CreateRole("audit");
				Roles.AddUserToRole("user1", "administrator");
;
			}
			catch (MembershipCreateUserException e)
			{
				ModelState.AddModelError("", "Error");
			}
			return View();
		}
	}
}