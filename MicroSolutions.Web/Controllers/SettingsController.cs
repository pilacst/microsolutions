using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using MicroSolutions.Web.Models;
using NLog;
using System;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
	[Authorize(Roles = "Host,Administrator")]
	public class SettingsController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private MicroSolutionsContext db = new MicroSolutionsContext();

		// GET: Settings
		public ActionResult Index()
		{
			try
			{
				SettingsViewModel settings = new SettingsViewModel();
				settings.ExpirationPeriod = db.ExpirationPeriod.ToList().Where(ep => ep.Status == true).ToList();
				settings.ItemType = db.ItemType.ToList().Where(it => it.Status == true).ToList();
				settings.Vendors = db.Vendors.ToList().Where(vd => vd.Status == true).ToList();
				settings.Supplier = db.Supplier.ToList().Where(su => su.Status == true);
				settings.EmailSettings = db.EmailSettings.ToList();
				return View(settings);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> index: " + ex.Message);
				throw;
			}
		}

		public ActionResult ExpirationPeriod()
		{
			return View();
		}

		[HttpGet]
		public JsonResult EditExpirationPeriod(int id)
		{
			try
			{
				var expirationPeriod = db.ExpirationPeriod.Find(id);
				ExpirationPeriodViewModel expirationViewModel = new ExpirationPeriodViewModel();
				expirationViewModel.ExpirationPeriodId = expirationPeriod.Id;
				expirationViewModel.ExpirationPeriodName = expirationPeriod.ExpirationPeriodName;
				expirationViewModel.ExpirationPeriodValue = expirationPeriod.ExpirationPeriodValue;

				return Json(expirationViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> EditExpirationPeriod[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult UpdateExpirationPeriod(ExpirationPeriodViewModel expirationPeriod)
		{
			try
			{
				var expirationPeriodDetails = new ExpirationPeriodViewModel();
				var expirationPeriodList = db.ExpirationPeriod.ToList();
				var ExpirationPeriodToUpdate = new ExpirationPeriod();
				if (expirationPeriod == null || !ModelState.IsValid)
					return Json(expirationPeriodList);

				ExpirationPeriodToUpdate = db.ExpirationPeriod.Find(expirationPeriod.ExpirationPeriodId);

				if (ExpirationPeriodToUpdate == null)
					return Json(expirationPeriodList);

				ExpirationPeriodToUpdate.ExpirationPeriodName = expirationPeriod.ExpirationPeriodName;
				ExpirationPeriodToUpdate.ExpirationPeriodValue = expirationPeriod.ExpirationPeriodValue;
				ExpirationPeriodToUpdate.UserName = WebSecurity.CurrentUserName;
				ExpirationPeriodToUpdate.ModifiedDate = DateTime.Now;
				if (TryUpdateModel(ExpirationPeriodToUpdate, "ExpirationPeriodName,ExpirationPeriodValue"))
				{
					db.Entry(ExpirationPeriodToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					expirationPeriodList = db.ExpirationPeriod.ToList();
				}

				return Json(expirationPeriodDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> UpdateExpirationPeriod[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult DeleteExpirationPeriod(ExpirationPeriodViewModel expirationPeriod)
		{
			try
			{
				var expirationPeriodDetails = new ExpirationPeriodViewModel();
				var expirationPeriodList = db.ExpirationPeriod.ToList();
				var ExpirationPeriodToUpdate = new ExpirationPeriod();
				if (expirationPeriod == null || !ModelState.IsValid)
					return Json(expirationPeriodList);

				ExpirationPeriodToUpdate = db.ExpirationPeriod.Find(expirationPeriod.ExpirationPeriodId);

				if (ExpirationPeriodToUpdate == null)
					return Json(expirationPeriodList);

				ExpirationPeriodToUpdate.Status = false;
				ExpirationPeriodToUpdate.UserName = WebSecurity.CurrentUserName;
				ExpirationPeriodToUpdate.ModifiedDate = DateTime.Now;
				if (TryUpdateModel(ExpirationPeriodToUpdate, "Status"))
				{
					db.Entry(ExpirationPeriodToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					expirationPeriodList = db.ExpirationPeriod.ToList();
				}

				return Json(expirationPeriodDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> DeleteExpirationPeriod[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult AddExpirationPeriod(ExpirationPeriodViewModel expirationPeriod)
		{
			try
			{
				var expirationPeriodList = db.ExpirationPeriod.ToList();
				if (ModelState.IsValid)
				{
					var expirationPeriodModel = new ExpirationPeriod();
					expirationPeriodModel.ExpirationPeriodName = expirationPeriod.ExpirationPeriodName;
					expirationPeriodModel.ExpirationPeriodValue = expirationPeriod.ExpirationPeriodValue;
					expirationPeriodModel.Status = true;
					expirationPeriodModel.UserName = WebSecurity.CurrentUserName;
					expirationPeriodModel.ModifiedDate = DateTime.Now;
					UpdateModel(expirationPeriodModel);
					db.ExpirationPeriod.Add(expirationPeriodModel);
					db.Entry(expirationPeriodModel).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
					expirationPeriodList.Add(expirationPeriodModel);
				}
				return Json(expirationPeriodList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> AddExpirationPeriod: " + ex.Message);
				throw;
			}
		}

		public ActionResult ItemType()
		{
			return View();
		}

		[HttpGet]
		public JsonResult EditItemTypeItem(int id)
		{
			try
			{
				var itemType = db.ItemType.Find(id);
				ItemTypeViewModel ItemTypeViewModel = new ItemTypeViewModel();
				ItemTypeViewModel.Id = itemType.Id;
				ItemTypeViewModel.ItemTypeName = itemType.ItemTypeName;

				return Json(ItemTypeViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> EditItemTypeItem[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult UpdateItemType(ItemTypeViewModel itemType)
		{
			try
			{
				var itemTypeDetails = new ItemTypeViewModel();
				var itemTypeItemList = db.ItemType.ToList();
				var itemTypeToUpdate = new ItemType();
				if (itemType == null || !ModelState.IsValid)
					return Json(itemType);

				itemTypeToUpdate = db.ItemType.Find(itemType.Id);

				if (itemTypeToUpdate == null)
					return Json(itemTypeItemList);

				itemTypeToUpdate.ItemTypeName = itemType.ItemTypeName;
				itemTypeToUpdate.UserName = WebSecurity.CurrentUserName;
				itemTypeToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(itemTypeToUpdate, new string[] { "ItemTypeName" }))
				{
					db.Entry(itemTypeToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					itemTypeItemList = db.ItemType.ToList();
				}

				return Json(itemTypeDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> UpdateItemType[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult DeleteItemType(ItemTypeViewModel itemType)
		{
			try
			{
				var itemTypeItemDetails = new ItemTypeViewModel();
				var itemTypeList = db.ItemType.ToList();
				var itemTypeToUpdate = new ItemType();
				if (itemType == null || !ModelState.IsValid)
					return Json(itemType);

				itemTypeToUpdate = db.ItemType.Find(itemType.Id);

				if (itemTypeToUpdate == null)
					return Json(itemTypeList);

				itemTypeToUpdate.Status = false;
				itemTypeToUpdate.ItemTypeName = itemType.ItemTypeName;
				itemTypeToUpdate.UserName = WebSecurity.CurrentUserName;
				itemTypeToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(itemTypeToUpdate, "Status"))
				{
					db.Entry(itemTypeToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					itemTypeList = db.ItemType.ToList();
				}

				return Json(itemTypeItemDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> DeleteItemType[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult AddItemType(ItemTypeViewModel itemType)
		{
			try
			{
				var itemTypeList = db.ItemType.ToList();
				if (ModelState.IsValid)
				{
					var itemTypeModel = new ItemType();
					itemTypeModel.ItemTypeName = itemType.ItemTypeName;
					itemTypeModel.Status = true;
					itemTypeModel.UserName = WebSecurity.CurrentUserName;
					itemTypeModel.ModifiedDate = DateTime.Now;
					UpdateModel(itemTypeModel);
					db.ItemType.Add(itemTypeModel);
					db.Entry(itemTypeModel).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
					itemTypeList.Add(itemTypeModel);
				}
				return Json(itemTypeList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> AddItemType[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public ActionResult Vendors()
		{
			return View();
		}

		[HttpGet]
		public JsonResult EditVendor(int id)
		{
			try
			{
				var vendor = db.Vendors.Find(id);
				VendorsViewModel vendorViewModel = new VendorsViewModel();
				vendorViewModel.Id = vendor.Id;
				vendorViewModel.VenderName = vendor.VenderName;

				return Json(vendorViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> EditVendor[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult UpdateVendor(VendorsViewModel vendor)
		{
			try
			{
				var vendorDetails = new VendorsViewModel();
				var vendorList = db.Vendors.ToList();
				var vendorToUpdate = new Vendors();
				if (vendor == null || !ModelState.IsValid)
					return Json(vendor);

				vendorToUpdate = db.Vendors.Find(vendor.Id);

				if (vendorToUpdate == null)
					return Json(vendorList);

				vendorToUpdate.VenderName = vendor.VenderName;
				vendorToUpdate.UserName = WebSecurity.CurrentUserName;
				vendorToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(vendorToUpdate, new string[] { "VenderName" }))
				{
					db.Entry(vendorToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					vendorList = db.Vendors.ToList();
				}

				return Json(vendorDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> UpdateVendor[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult DeleteVendor(VendorsViewModel vendor)
		{
			try
			{
				var vendorDetails = new VendorsViewModel();
				var vendorList = db.Vendors.ToList();
				var vendorToUpdate = new Vendors();
				if (vendor == null || !ModelState.IsValid)
					return Json(vendor);

				vendorToUpdate = db.Vendors.Find(vendor.Id);

				if (vendorToUpdate == null)
					return Json(vendorList);

				vendorToUpdate.Status = false;
				vendorToUpdate.UserName = WebSecurity.CurrentUserName;
				vendorToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(vendorToUpdate, new string[] { "Status" }))
				{
					db.Entry(vendorToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					vendorList = db.Vendors.ToList();
				}

				return Json(vendorDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> DeleteVendor[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult AddVendor(VendorsViewModel vendor)
		{
			try
			{
				var vendorList = db.Vendors.ToList();
				if (ModelState.IsValid)
				{
					var vendorModel = new Vendors();
					vendorModel.VenderName = vendor.VenderName;
					vendorModel.Status = true;
					vendorModel.UserName = WebSecurity.CurrentUserName;
					vendorModel.ModifiedDate = DateTime.Now;
					UpdateModel(vendorModel);
					db.Vendors.Add(vendorModel);
					db.Entry(vendorModel).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
					vendorList.Add(vendorModel);
				}
				return Json(vendorList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> AddVendor[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult EditSupplier(int id)
		{
			try
			{
				var supplier = db.Supplier.Find(id);
				SupplierViewModel supplierViewModel = new SupplierViewModel();
				supplierViewModel.Id = supplier.Id;
				supplierViewModel.SupplierName = supplier.SupplierName;

				return Json(supplierViewModel, JsonRequestBehavior.AllowGet);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> EditSupplier[HTTPGET]: " + ex.Message);
				throw;
			}
		}

		public JsonResult UpdateSupplier(SupplierViewModel supplier)
		{
			try
			{
				var supplierDetails = new SupplierViewModel();
				var supplierList = db.Supplier.ToList();
				var supplierToUpdate = new Supplier();
				if (supplier == null || !ModelState.IsValid)
					return Json(supplier);

				supplierToUpdate = db.Supplier.Find(supplier.Id);

				if (supplierToUpdate == null)
					return Json(supplierList);

				supplierToUpdate.SupplierName = supplier.SupplierName;
				supplierToUpdate.UserName = WebSecurity.CurrentUserName;
				supplierToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(supplierToUpdate, new string[] { "SupplierName" }))
				{
					db.Entry(supplierToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					supplierList = db.Supplier.ToList();
				}

				return Json(supplierDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> UpdateSupplier: " + ex.Message);
				throw;
			}
		}

		public JsonResult DeleteSupplier(SupplierViewModel supplier)
		{
			try
			{
				var supplierDetails = new SupplierViewModel();
				var supplierList = db.Supplier.ToList();
				var supplierToUpdate = new Supplier();
				if (supplier == null || !ModelState.IsValid)
					return Json(supplier);

				supplierToUpdate = db.Supplier.Find(supplier.Id);

				if (supplierToUpdate == null)
					return Json(supplierList);

				supplierToUpdate.Status = supplier.Status;
				supplierToUpdate.UserName = WebSecurity.CurrentUserName;
				supplierToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(supplierToUpdate, new string[] { "Status" }))
				{
					db.Entry(supplierToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					supplierList = db.Supplier.ToList();
				}

				return Json(supplierDetails);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> DeleteSupplier: " + ex.Message);
				throw;
			}
		}

		public JsonResult AddSupplier(SupplierViewModel supplier)
		{
			try
			{
				var supplierList = db.Supplier.ToList();
				if (ModelState.IsValid)
				{
					var supplierModel = new Supplier();
					supplierModel.SupplierName = supplier.SupplierName;
					supplierModel.Status = true;
					supplierModel.UserName = WebSecurity.CurrentUserName;
					supplierModel.ModifiedDate = DateTime.Now;
					UpdateModel(supplierModel);
					db.Supplier.Add(supplierModel);
					db.Entry(supplierModel).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
					supplierList.Add(supplierModel);
				}
				return Json(supplierList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> AddSupplier: " + ex.Message);
				throw;
			}
		}

		public ActionResult ViewEmailConfigurations(int id)
		{
			try
			{
				var emailViewModel = new EmailConfigurationViewModel();
				var config = db.EmailSettings.Find(id);
				emailViewModel.FromEmail = config.FromEmail;
				emailViewModel.FromEmailPassword = config.FromEmail;
				emailViewModel.PortNumber = config.PortNumber;
				emailViewModel.SmtpAddress = config.SmtpAddress;
				emailViewModel.ToEmail = config.ToEmail;
				emailViewModel.WarningPeriod = config.WarningPeriod;
				emailViewModel.Id = config.Id;
				return View("EmailAlert/_EditEmailAlertSettings", emailViewModel);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> ViewEmailConfigurations: " + ex.Message);
				throw;
			}
		}

		public ActionResult UpdateEmailConfigurations(EmailConfigurationViewModel email)
		{
			try
			{
				var emailDetailsToUpdate = db.EmailSettings.Find(email.Id);
				var emailList = db.EmailSettings.ToList();

				var emailInfo = new EmailSettings();
				if (email == null || !ModelState.IsValid)
					return View("EmailAlert/EmailAlertSetting", emailList);

				if (emailDetailsToUpdate == null)
					return View("EmailAlert/EmailAlertSetting", emailList);

				emailDetailsToUpdate.FromEmail = email.FromEmail;
				emailDetailsToUpdate.FromEmailPassword = email.FromEmailPassword;
				emailDetailsToUpdate.PortNumber = email.PortNumber;
				emailDetailsToUpdate.SmtpAddress = email.SmtpAddress;
				emailDetailsToUpdate.ToEmail = email.ToEmail;
				emailDetailsToUpdate.WarningPeriod = email.WarningPeriod;
				emailDetailsToUpdate.UserName = WebSecurity.CurrentUserName;
				emailDetailsToUpdate.ModifiedDate = DateTime.Now;

				if (TryUpdateModel(emailDetailsToUpdate, "FromEmail,FromEmailPassword,ToEmail,WarningPeriod,SmtpAddress,PortNumber"))
				{
					db.Entry(emailDetailsToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					emailList = db.EmailSettings.ToList();
				}

				return View("EmailAlert/EmailAlertSetting", emailList);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Settings -> UpdateEmailConfigurations: " + ex.Message);
				throw;
			}
		}
	}
}