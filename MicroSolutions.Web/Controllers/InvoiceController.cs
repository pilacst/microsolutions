using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using MicroSolutions.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
	[Authorize(Roles = "Host,Administrator")]
	public class InvoiceController : Controller
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private MicroSolutionsContext db = new MicroSolutionsContext();

		// GET: Invoice
		public ActionResult Index()
		{
			try
			{
				var invoiceVmList = new List<InvoiceViewModel>();

				var invoice = db.Invoice.ToList().Where(inv => inv.Status == true);

				foreach (var item in invoice)
				{
					var invoiceVm = new InvoiceViewModel();
					invoiceVm.Customer = new Customer();
					invoiceVm.Customer.CustomerName = db.Customer.ToList().Find(cus => cus.Id == item.Customer.Id).CustomerName;
					invoiceVm.InvoiceDate = item.InvoiceDate.ToString("d");
					invoiceVm.InvoiceNumber = item.InvoiceNumber;
					invoiceVm.Id = item.Id;
					invoiceVmList.Add(invoiceVm);
				}

				return View(invoiceVmList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> index: " + ex.Message);
				throw;
			}
		}

		[HttpGet]
		public ActionResult Add()
		{
			try
			{
				ViewBag.CustomerList = new SelectList(from customers in db.Customer.ToList().Where(c => c.Status == true) select customers, "Id", "CustomerName", 0);
				ViewBag.VendorList = new SelectList(from vendors in db.Vendors.ToList().Where(v => v.Status == true) select vendors, "Id", "VenderName", 0);
				ViewBag.ItemTypeList = new SelectList(from itemType in db.ItemType.ToList().Where(it => it.Status == true) select itemType, "Id", "ItemTypeName", 0);
				ViewBag.ExpirationPeriodList = new SelectList(from expirationPeriod in db.ExpirationPeriod.ToList().Where(ep => ep.Status == true) select expirationPeriod, "Id", "ExpirationPeriodName", 0);
				ViewBag.SupplierList = new SelectList(from suppliers in db.Supplier.ToList().Where(s => s.Status == true) select suppliers, "Id", "SupplierName", 0);
				var invoiceVm = new InvoiceViewModel();
				invoiceVm.InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");
				return View(invoiceVm);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> Add: " + ex.Message);
				throw;
			}
		}

		[HttpPost]
		public ActionResult Add(InvoiceViewModel invoiceVm)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					TempData["Message"] = "Failure in invoice save action. Check error logs for more details.";
					logger.Log(LogLevel.Error, "Exception occurred in Invoice -> Add[HTTPPOST]: " + string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
					TempData["MessageType"] = "alert-danger";
					return RedirectToAction("Index");
				}

                if(invoiceVm == null)
                {
                    TempData["Message"] = "No invoicess to add.";
                    TempData["MessageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }

                if (invoiceVm.PartsForInvoiceViewModel == null)
                {
                    TempData["Message"] = "No parts availabble for the invoice to add.";
                    TempData["MessageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }

                ///Limit
                var invoiceCount = db.Invoice.ToList().Count;
				if(invoiceCount > 50)
				{
					TempData["Message"] = "This is a trial version. Please contact developer.";
					TempData["MessageType"] = "alert-danger";
					return RedirectToAction("Index");
				}

				var invoiceModel = new Invoice();
				var partForInvoiceList = new List<PartsForInvoice>();
				invoiceModel.CustomerId = invoiceVm.Customer.Id;
				invoiceModel.InvoiceDate = DateTime.ParseExact(invoiceVm.InvoiceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				invoiceModel.InvoiceNumber = invoiceVm.InvoiceNumber;

                if (invoiceVm.PartsForInvoiceViewModel.Count > 0)
                {
                    var count = 0;
                    foreach (var item in invoiceVm.PartsForInvoiceViewModel)
                    {

                        if (count == 0)
                        {
                            count = count + 1;
                            continue;
                        }

                        var partForInvoice = new PartsForInvoice();
                        partForInvoice.Id = Guid.NewGuid().ToString();
                        partForInvoice.Description = item.Description;
                        var endDate = (string.IsNullOrEmpty(item.EndDate) || string.IsNullOrWhiteSpace(item.EndDate)) ? DateTime.Now.AddYears(20).ToString("dd/MM/yyyy") : item.EndDate;
                        var startingDate = (string.IsNullOrEmpty(item.StartingDate) || string.IsNullOrWhiteSpace(item.StartingDate)) ? DateTime.Now.AddYears(20).ToString("dd/MM/yyyy") : item.StartingDate;

                        partForInvoice.EndDate = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
						partForInvoice.ExpirationPeriodId = item.ExpirationPeriodId;
						partForInvoice.ItemTypeId = item.ItemTypeId;
						partForInvoice.PartNumber = item.PartNumber;
						partForInvoice.Quantity = item.Quantity;
						partForInvoice.SerialNumber = item.SerialNumber;
						partForInvoice.SupplierId = item.SupplierId;
						partForInvoice.StartingDate = DateTime.ParseExact(startingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
						partForInvoice.VendorId = item.VendorId;
						partForInvoice.Status = true;
						partForInvoice.UserName = WebSecurity.CurrentUserName;
						partForInvoiceList.Add(partForInvoice);
					}

					invoiceModel.PartsForInvoice = partForInvoiceList;
				}

				invoiceModel.Status = true;
				invoiceModel.UserName = WebSecurity.CurrentUserName;

				TryUpdateModel(invoiceModel, "InvoiceNumber,InvoiceDate,CustomerId,Status,UserName");
				db.Invoice.Add(invoiceModel);
				db.Entry(invoiceModel).State = System.Data.Entity.EntityState.Added;
				db.SaveChanges();

				TempData["Message"] = "Invoice saved successfully. Invoice number is: " + invoiceVm.InvoiceNumber;
				TempData["MessageType"] = "alert-success";

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> Add[HTTPPOST]: " + ex.Message);
				throw;
			}
		}

		[HttpGet]
		public ActionResult Edit(int Id)
		{
			try
			{
				ViewBag.CustomerList = new SelectList(from customers in db.Customer.ToList().Where(c => c.Status == true) select customers, "Id", "CustomerName", 0);

				var invoiceVm = new InvoiceViewModel();
				var partForInvoiceVmList = new List<PartsForInvoiceViewModel>();
				var partForInvoiceVm = new PartsForInvoiceViewModel();

				var invoice = db.Invoice.ToList().Find(inv => inv.Id == Id);

                if (invoice == null)
                    return RedirectToAction("Index");

				invoiceVm.Customer = new Customer();
				invoiceVm.Customer = db.Customer.ToList().Find(cus => cus.Id == invoice.CustomerId);
				invoiceVm.InvoiceDate = invoice.InvoiceDate.ToString("dd/MM/yyyy");
				invoiceVm.InvoiceNumber = invoice.InvoiceNumber;
				invoiceVm.Status = invoice.Status;

				return View(invoiceVm);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> Edit[GET]: " + ex.Message);
				throw;
			}
		}

		[HttpPost]
		public ActionResult Edit(InvoiceViewModel invoiceVm)
		{
			try
			{
                if(invoiceVm == null)
                {
                    TempData["Message"] = "Failure edit invoice. Data is empty";
                    TempData["MessageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }

				if (ModelState.IsValid)
				{
                    var invoice = db.Invoice.Where(m=>m.Id == invoiceVm.Id).FirstOrDefault();

                    if (invoice == null)
                    {
                        TempData["Message"] = "Can not find invoice, Please re check if the invoice is valid.";
                        TempData["MessageType"] = "alert-danger";
                        return RedirectToAction("Index");
                    }

                    invoice.Id = invoiceVm.Id;
					invoice.CustomerId = invoiceVm.Customer.Id;
					invoice.InvoiceDate = DateTime.ParseExact(invoiceVm.InvoiceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
					invoice.InvoiceNumber = invoiceVm.InvoiceNumber;
					invoice.Status = invoiceVm.Status;
					invoice.UserName = WebSecurity.CurrentUserName;

					TryUpdateModel(invoice, "InvoiceNumber,InvoiceDate,CustomerId,Status,UserName");
					db.Invoice.Add(invoice);
					db.Entry(invoice).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					TempData["Message"] = "Invoice updated successfully. Invoice number is: " + invoiceVm.InvoiceNumber;
					TempData["MessageType"] = "alert-success";
				}
				else
				{
					TempData["Message"] = "Failure in invoice edit action. Check for following issues." + string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
					TempData["MessageType"] = "alert-danger";
				}
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> Edit[POST]: " + ex.Message);
				throw;
			}
		}

		[HttpGet]
		public ActionResult ListPartsForInvoice(int id)
		{
			try
			{
				var partsForInvoice = db.PartsForInvoice.ToList().Where(m => m.Invoice.Id == id && m.Status == true);
				
				var partsForInvoiceVmList = new List<PartsForInvoiceViewModel>();
				ViewBag.InvoiceId = id;
				Session["InvoiceId"] = id;
				foreach (var item in partsForInvoice)
				{
					var partsForInvoiceVm = new PartsForInvoiceViewModel();
					partsForInvoiceVm.Description = item.Description;
					partsForInvoiceVm.EndDate = item.EndDate.ToString("dd/MM/yyyy");
					partsForInvoiceVm.ExpirationPeriod = new ExpirationPeriod();
					partsForInvoiceVm.ExpirationPeriodId = item.ExpirationPeriodId;
					partsForInvoiceVm.ExpirationPeriod = db.ExpirationPeriod.ToList().Find(m => m.Id == item.ExpirationPeriodId);
					partsForInvoiceVm.InvoiceId = id;
					partsForInvoiceVm.ItemTypeId = item.ItemTypeId;
					partsForInvoiceVm.ItemType = new ItemType();
					partsForInvoiceVm.ItemType = db.ItemType.ToList().Find(itm => itm.Id == item.ItemTypeId);
					partsForInvoiceVm.PartNumber = item.PartNumber;
					partsForInvoiceVm.Quantity = item.Quantity;
					partsForInvoiceVm.SerialNumber = item.SerialNumber;
					partsForInvoiceVm.StartingDate = item.StartingDate.ToString("dd/MM/yyyy");
					partsForInvoiceVm.Status = item.Status;
					partsForInvoiceVm.SupplierId = item.Supplier.Id;
					partsForInvoiceVm.Supplier = item.Supplier;
					partsForInvoiceVm.VendorId = item.VendorId;
					partsForInvoiceVm.Vendor = new Vendors();
					partsForInvoiceVm.Vendor = db.Vendors.ToList().Find(ven => ven.Id == item.VendorId);
					partsForInvoiceVm.Id = item.Id;
					partsForInvoiceVmList.Add(partsForInvoiceVm);
				}
				return View(partsForInvoiceVmList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> ListPartsForInvoice[GET]: " + ex.Message);
				throw;
			}
		}

		[HttpGet]
		public ActionResult EditPart(string id)
		{
			try
			{
				ViewBag.CustomerList = new SelectList(from customers in db.Customer.ToList().Where(c => c.Status == true) select customers, "Id", "CustomerName", 0);
				ViewBag.VendorList = new SelectList(from vendors in db.Vendors.ToList().Where(v => v.Status == true) select vendors, "Id", "VenderName", 0);
				ViewBag.ItemTypeList = new SelectList(from itemType in db.ItemType.ToList().Where(it => it.Status == true) select itemType, "Id", "ItemTypeName", 0);
				ViewBag.ExpirationPeriodList = new SelectList(from expirationPeriod in db.ExpirationPeriod.ToList().Where(ep => ep.Status == true) select expirationPeriod, "Id", "ExpirationPeriodName", 0);
				ViewBag.SupplierList = new SelectList(from suppliers in db.Supplier.ToList().Where(s => s.Status == true) select suppliers, "Id", "SupplierName", 0);

				var partsForInvoice = db.PartsForInvoice.ToList().Where(m => m.Id == id).FirstOrDefault();
				var partsForInvoiceVm = new PartsForInvoiceViewModel();

				partsForInvoiceVm.Description = partsForInvoice.Description;
				partsForInvoiceVm.EndDate = partsForInvoice.EndDate.ToString("dd/MM/yyyy");
				partsForInvoiceVm.ExpirationPeriod = new ExpirationPeriod();
				partsForInvoiceVm.ExpirationPeriodId = partsForInvoice.ExpirationPeriodId;
				partsForInvoiceVm.ExpirationPeriod = db.ExpirationPeriod.ToList().Find(m => m.Id == partsForInvoice.ExpirationPeriodId);
				partsForInvoiceVm.InvoiceId = partsForInvoice.Invoice.Id;
				partsForInvoiceVm.ItemTypeId = partsForInvoice.ItemTypeId;
				partsForInvoiceVm.ItemType = new ItemType();
				partsForInvoiceVm.ItemType = db.ItemType.ToList().Find(itm => itm.Id == partsForInvoice.ItemTypeId);
				partsForInvoiceVm.PartNumber = partsForInvoice.PartNumber;
				partsForInvoiceVm.Quantity = partsForInvoice.Quantity;
				partsForInvoiceVm.SerialNumber = partsForInvoice.SerialNumber;
				partsForInvoiceVm.StartingDate = partsForInvoice.StartingDate.ToString("dd/MM/yyyy");
				partsForInvoiceVm.Status = partsForInvoice.Status;
				partsForInvoiceVm.SupplierId = partsForInvoice.Supplier.Id;
				partsForInvoiceVm.Supplier = partsForInvoice.Supplier;
				partsForInvoiceVm.VendorId = partsForInvoice.VendorId;
				partsForInvoiceVm.Vendor = new Vendors();
				partsForInvoiceVm.Vendor = db.Vendors.ToList().Find(ven => ven.Id == partsForInvoice.VendorId);
				partsForInvoiceVm.Id = partsForInvoice.Id;
				return View(partsForInvoiceVm);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> EditPart[GET]: " + ex.Message);
				throw;
			}
		}

		[HttpPost]
		public ActionResult EditPart(PartsForInvoiceViewModel partsForInvoiceVm)
		{
			try
			{
				if(!ModelState.IsValid)
				{
					TempData["Message"] = "Failure in invoice edit action. Check for following issues." + string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
					TempData["MessageType"] = "alert-danger";
					return RedirectToAction("Index");
				}

				var part = new PartsForInvoice();
				part.Description = partsForInvoiceVm.Description;
				part.EndDate = DateTime.ParseExact(partsForInvoiceVm.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				part.ExpirationPeriodId = partsForInvoiceVm.ExpirationPeriod.Id;
				part.ItemTypeId = partsForInvoiceVm.ItemType.Id;
				part.PartNumber = partsForInvoiceVm.PartNumber;
				part.Quantity = partsForInvoiceVm.Quantity;
				part.SerialNumber = partsForInvoiceVm.SerialNumber;
				part.StartingDate = DateTime.ParseExact(partsForInvoiceVm.StartingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				part.Status = partsForInvoiceVm.Status;
				part.SupplierId = partsForInvoiceVm.Supplier.Id;
				part.UserName = WebSecurity.CurrentUserName;
				part.VendorId = partsForInvoiceVm.Vendor.Id;
				part.Id = partsForInvoiceVm.Id;
				part.Invoice_Id = partsForInvoiceVm.InvoiceId;
				TryUpdateModel(part, "Description,EndDate,ExpirationPeriodId,ItemTypeId,PartNumber,Quantity,SerialNumber,StartingDate,Status,SupplierId,UserName,VendorId");
				db.PartsForInvoice.Add(part);
				db.Entry(part).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
				TempData["Message"] = "Parts details are updated successfully.";
				TempData["MessageType"] = "alert-success";
				return RedirectToAction("ListPartsForInvoice", new { Id = partsForInvoiceVm.InvoiceId });
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> EditPart[HttpPost]: " + ex.Message);
				throw;
			}
		}

		public virtual ActionResult AddPartToInvoice()
		{
			try
			{
				ViewBag.CustomerList = new SelectList(from customers in db.Customer.ToList().Where(c => c.Status == true) select customers, "Id", "CustomerName", 0);
				ViewBag.VendorList = new SelectList(from vendors in db.Vendors.ToList().Where(v => v.Status == true) select vendors, "Id", "VenderName", 0);
				ViewBag.ItemTypeList = new SelectList(from itemType in db.ItemType.ToList().Where(it => it.Status == true) select itemType, "Id", "ItemTypeName", 0);
				ViewBag.ExpirationPeriodList = new SelectList(from expirationPeriod in db.ExpirationPeriod.ToList().Where(ep => ep.Status == true) select expirationPeriod, "Id", "ExpirationPeriodName", 0);
				ViewBag.SupplierList = new SelectList(from suppliers in db.Supplier.ToList().Where(s => s.Status == true) select suppliers, "Id", "SupplierName", 0);
				var partsForInvoice = new PartsForInvoiceViewModel();
				Session["InvoiceIdForPart"] = Session["InvoiceId"];
				return View(partsForInvoice);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> AddPartToInvoice: " + ex.Message);
				throw;
			}
		}

		[HttpPost]
		public virtual ActionResult AddPartToInvoice(PartsForInvoiceViewModel partsForInvoiceVm)
		{
			try
			{
				var invoiceId = Int32.Parse(Session["InvoiceIdForPart"].ToString());
				var invoiceToUpdate = db.Invoice.ToList().Find(inv => inv.Id == invoiceId);
				var partlist = new List<PartsForInvoice>();
				var part = new PartsForInvoice();
				part.Description = partsForInvoiceVm.Description;
				part.EndDate = DateTime.ParseExact(partsForInvoiceVm.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				part.ExpirationPeriodId = partsForInvoiceVm.ExpirationPeriod.Id;
				part.ItemTypeId = partsForInvoiceVm.ItemType.Id;
				part.PartNumber = partsForInvoiceVm.PartNumber;
				part.Quantity = partsForInvoiceVm.Quantity;
				part.SerialNumber = partsForInvoiceVm.SerialNumber;
				part.StartingDate = DateTime.ParseExact(partsForInvoiceVm.StartingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				part.Status = partsForInvoiceVm.Status;
				part.SupplierId = partsForInvoiceVm.Supplier.Id;
				part.UserName = WebSecurity.CurrentUserName;
				part.VendorId = partsForInvoiceVm.Vendor.Id;
				part.Id = Guid.NewGuid().ToString();
				part.Invoice = new Invoice();
				part.Invoice = invoiceToUpdate;
				part.Invoice_Id = Int32.Parse(Session["InvoiceIdForPart"].ToString());
				//partlist.Add(part);
				//invoiceToUpdate.PartsForInvoice.Add(part);


				TryUpdateModel(part, "test", new string[] { "Invoice_Id,Description,EndDate,ExpirationPeriodId,ItemTypeId,PartNumber,Quantity,SerialNumber,StartingDate,Status,SupplierId,UserName,VendorId" }, new string[] { "Invoice" });
				db.PartsForInvoice.Add(part);
				db.Entry(invoiceToUpdate).State = System.Data.Entity.EntityState.Added;
				db.SaveChanges();
				return RedirectToAction("ListPartsForInvoice", new { Id = partsForInvoiceVm.InvoiceId });
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Invoice -> AddPartToInvoice[POST]: " + ex.Message);
				throw;
			}
		}
	}
}