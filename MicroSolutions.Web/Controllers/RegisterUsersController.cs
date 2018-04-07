using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using MicroSolutions.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
	/// <summary>
	/// Register users for the system and update modified users information.
	/// </summary>
	[Authorize(Roles = "Host,Administrator")]
    public class RegisterUsersController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private MicroSolutionsContext db = new MicroSolutionsContext();

		// GET: RegisterUsers
		public ActionResult Index()
        {
			try
			{
				var users = db.webpages_Membership.ToList();
				var usersViewModel = ArrangeUsers(users);

				return View(usersViewModel);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in RegisterUsers -> Index: " + ex.Message);
				throw;
			}
        }

		public JsonResult EditUser(int Id) {
			try
			{
				var userViewModel = new RegisteredUsersViewModel();

				var user = db.webpages_Membership.Find(Id);
				var userRole = Roles.GetRolesForUser(user.UserProfile.UserName).FirstOrDefault();
				userViewModel.UserId = user.UserId;
				userViewModel.UserName = user.UserProfile.UserName;
				userViewModel.UserRoleId = db.webpages_UsersInRoles.Where(u => u.UserId == user.UserId).FirstOrDefault()?.RoleId;
				userViewModel.CreatedDate = user.CreateDate;
				userViewModel.ModifiedDate = user.PasswordChangedDate;
				userViewModel.IsActiveUser = (user.IsConfirmed == true) ? "Yes" : "No";
				userViewModel.IsActiveUserState = user.IsConfirmed;
				userViewModel.UserRole = userRole;
				return Json(userViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in RegisterUsers -> EditUser: " + ex.Message);
				throw;
			}
		}

		public JsonResult UpdateUser(RegisteredUsersViewModel user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var userToUpdate = db.webpages_Membership.Find(user.UserId);

					var userRole = db.webpages_UsersInRoles.Where(r => r.UserId == user.UserId).FirstOrDefault();
					if (userRole != null)
						Roles.RemoveUserFromRole(userToUpdate.UserProfile.UserName, userRole.webpages_Roles.RoleName);
					Roles.AddUserToRole(userToUpdate.UserProfile.UserName, user.UserRole);

					userToUpdate.IsConfirmed = user.IsActiveUserState;

					if (TryUpdateModel(userToUpdate, new string[] { "IsConfirmed" }))
					{
						db.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
						db.SaveChanges();
					}
				}

				var users = db.webpages_Membership.ToList();
				var usersViewModel = ArrangeUsers(users);

				return Json(usersViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in RegisterUsers -> UpdateUser: " + ex.Message);
				throw;
			}
		}

		public JsonResult AddNewUser(RegisteredUsersViewModel user)
		{
			try
			{
				if (!WebSecurity.UserExists(user.UserName))
				{
					WebSecurity.CreateUserAndAccount(user.UserName, user.Password);
					Roles.AddUserToRole(user.UserName, user.UserRole);
				}

				var users = db.webpages_Membership.ToList();
				var usersViewModel = ArrangeUsers(users);
				return Json(usersViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in RegisterUsers -> AddNewUser: " + ex.Message);
				throw;
			}
		}

		public IList<RegisteredUsersViewModel> ArrangeUsers(IList<webpages_Membership> users)
		{
			try
			{
				var usersViewModel = new List<RegisteredUsersViewModel>();
				foreach (var userItem in users)
				{
					var userViewModel = new RegisteredUsersViewModel();
					userViewModel.UserId = userItem.UserId;
					userViewModel.UserName = userItem.UserProfile.UserName;
					userViewModel.UserRole = Roles.GetRolesForUser(userItem.UserProfile.UserName).FirstOrDefault();
					userViewModel.CreatedDate = userItem.CreateDate;
					userViewModel.ModifiedDate = userItem.PasswordChangedDate;
					userViewModel.IsActiveUser = (userItem.IsConfirmed == true) ? "Yes" : "No";
					usersViewModel.Add(userViewModel);
				}
				return usersViewModel;
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in RegisterUsers -> ArrangeUsers: " + ex.Message);
				throw;
			}
		}
	}
}