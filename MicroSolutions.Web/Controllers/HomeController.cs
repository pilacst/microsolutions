using MicroSolutions.DAL;
using MicroSolutions.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroSolutions.Web.Controllers
{
	[Authorize(Roles = "Host,Administrator,RegisteredUser")]
	public class HomeController : Controller
	{

		private static Logger logger = LogManager.GetCurrentClassLogger();
		private MicroSolutionsContext db = new MicroSolutionsContext();
		

		public ActionResult Index()
		{
			try
			{
				var invoice = db.Invoice.ToList().Where(inv => inv.Status == true);
				var parts = db.PartsForInvoice.ToList().Where(part=>part.Status = true);

				var homeViewModel = new HomeVieModel();
				homeViewModel.TotalInvoices = invoice.ToList().Count;
				homeViewModel.TotalExpiredParts = parts.ToList().Where(prt => prt.Status == true && prt.EndDate <= DateTime.Now).ToList().Count;
				homeViewModel.TotalExpiredItemOnToday = parts.ToList().Where(prt => prt.Status == true && prt.EndDate.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")).ToList().Count;
				homeViewModel.TotalItemsExpiredWithinNextThreeMonths = parts.ToList().Where(prt => prt.Status == true && prt.EndDate.ToString("yyyy-MM-dd") == DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd")).ToList().Count;
				return View(homeViewModel);
			}
			catch (Exception ex)
			{

				throw;
			}
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}