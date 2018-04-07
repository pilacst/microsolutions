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
	[Authorize(Roles = "RegisteredUser,Administrator,Host")]
	public class ReportController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private MicroSolutionsContext db = new MicroSolutionsContext();
		// GET: Report
		public ActionResult ExpiredParts()
        {
			var expiredParts = from prts in db.PartsForInvoice.ToList()
							   join inv in db.Invoice.ToList() on prts.Invoice_Id equals inv.Id
							   where (prts.EndDate <= DateTime.Now)
							   select prts;

			var partsForList = new List<PartsForInvoiceViewModel>();

			foreach (var item in expiredParts)
			{
				var partsForInvoice = new PartsForInvoiceViewModel();
				partsForInvoice.Invoice = db.Invoice.Where(i => i.Id == item.Invoice_Id).FirstOrDefault();
				partsForInvoice.PartNumber = item.PartNumber;
				partsForInvoice.EndDate = item.EndDate.ToString("dd/MM/yyyy");
				partsForList.Add(partsForInvoice);
			}

            return View(partsForList);
        }

		public ActionResult FilterByExpirationPeriod()
		{
			return View();
		}
    }
}