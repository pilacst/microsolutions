using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
	[Authorize(Roles = "Administrator,Host")]
	public class UploadExcelController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private MicroSolutionsContext db = new MicroSolutionsContext();
		// GET: UploadExcel
		public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public ActionResult UploadExcelFile(HttpPostedFileBase file1)
		{
			try
			{
				///Limit
				var invoiceCount = db.Invoice.ToList().Count;
				if (invoiceCount > 50)
				{
					TempData["Message"] = "This is a trial version. Please contact developer.";
					TempData["MessageType"] = "alert-danger";
					return RedirectToAction("Index");
				}


				if (Request.Files.Count > 0)
				{
					try
					{
						object[,] dataSet = null;
						int noOfCol = 0;
						int noOfRow = 0;
						HttpFileCollectionBase file = Request.Files;
						if ((file != null) && (file.Count > 0))
						{
							byte[] fileBytes = new byte[Request.ContentLength];
							var data = Request.InputStream.Read(fileBytes, 0, Convert.ToInt32(Request.ContentLength));
							using (var package = new ExcelPackage(Request.InputStream))
							{
								var currentSheet = package.Workbook.Worksheets;
								var workSheet = currentSheet.First();
								noOfCol = workSheet.Dimension.End.Column;
								noOfRow = workSheet.Dimension.End.Row;
								dataSet = new object[noOfRow, noOfCol];
								dataSet = (object[,])workSheet.Cells.Value;
							}
						}
						UploadDataInvoice(dataSet, noOfCol, noOfRow);

						TempData["Message"] = "Excel uploaded successfully";
						TempData["MessageType"] = "alert-success";

						return RedirectToAction("Index");
					}
					catch (Exception ex)
					{
						logger.Log(LogLevel.Error, "Exception occurred in uploading excel" + ex.Message);
						throw;
					}

				}
				TempData["Message"] = "Excel uploaded failed. Please check the error log.";
				TempData["MessageType"] = "alert-danger";

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in uploading excel" + ex.Message);
				throw;
			}
		}

		public virtual void UploadDataInvoice(object[,] dataSet, int numberOfColumns, int numberOfRows)
		{
			var rowId = 0;
			try
			{
				var customers = db.Customer.ToList();
				var vendors = db.Vendors.ToList();
				var itemTypes = db.ItemType.ToList();
				var expirationPeriod = db.ExpirationPeriod.ToList();
				var suppliers = db.Supplier.ToList();

				var partForInvoiceList = new List<PartsForInvoice>();
				var invoiceModelList = new List<Invoice>();
				for (int rows = 0; rows < numberOfRows; rows++)
				{
					if ((rows == 0) || (dataSet[rows, 1] == null))
						continue;

					rowId = rows+1;

					var invoiceModel = new Invoice();

					var partForInvoice = new PartsForInvoice();


					var validExperiance = (dataSet[rows, 11] == null) ? "N/A" : dataSet[rows, 11].ToString().Trim();
					var validVendor = (dataSet[rows, 4] == null) ? "N/A" : dataSet[rows, 4].ToString().Trim();
					var validItemType = (dataSet[rows, 5] == null) ? "N/A" : dataSet[rows, 5].ToString().Trim();
					var validSupplier = (dataSet[rows, 10] == null) ? "N/A" : dataSet[rows, 10].ToString().Trim();
					var validCustomer = (dataSet[rows, 3] == null) ? "N/A" : dataSet[rows, 3].ToString().Trim();
					var startingDate = (dataSet[rows, 12] == null) ? "0" : dataSet[rows, 12].ToString().Trim();
					var endDate = (dataSet[rows, 13] == null) ? "0" : dataSet[rows, 13].ToString().Trim();
					var quantity = (dataSet[rows, 8] == null) ? "0" : dataSet[rows, 8].ToString().Trim();


					if (invoiceModelList?.Where(lst => lst.InvoiceNumber == dataSet[rows, 1].ToString()).FirstOrDefault() != null)
					{
						partForInvoice.Vendor = vendors.Where(ven => ven.VenderName.Trim() == validVendor)?.FirstOrDefault();
						partForInvoice.ItemType = itemTypes.Where(itm => itm.ItemTypeName.Trim() == validItemType)?.FirstOrDefault();
						partForInvoice.PartNumber = (dataSet[rows, 6] == null) ? string.Empty : dataSet[rows, 6].ToString().Trim();
						partForInvoice.Description = (dataSet[rows, 7] == null) ? string.Empty : dataSet[rows, 7].ToString().Trim();
						partForInvoice.Quantity = Int32.Parse(quantity);
						partForInvoice.SerialNumber = (dataSet[rows, 9] == null) ? string.Empty : dataSet[rows, 9].ToString().Trim();
						partForInvoice.Supplier = suppliers.Where(sup => sup.SupplierName.Trim() == validSupplier)?.FirstOrDefault();
						partForInvoice.StartingDate = DateTime.ParseExact(new DateTime(1899, 12, 31).AddDays(Double.Parse(startingDate)).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
						partForInvoice.EndDate = DateTime.ParseExact(new DateTime(1899, 12, 31).AddDays(Double.Parse(endDate)).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
						partForInvoice.ExpirationPeriod = expirationPeriod.Where(exp => exp.ExpirationPeriodName.Trim() == validExperiance)?.FirstOrDefault();

						invoiceModelList?.Where(lst => lst.InvoiceNumber.Trim() == dataSet[rows, 1].ToString().Trim()).FirstOrDefault().PartsForInvoice.Add(partForInvoice);
					}
					else
					{
						partForInvoiceList = new List<PartsForInvoice>();
						invoiceModel.InvoiceNumber = dataSet[rows, 1].ToString().Trim();
						var invoiceDate = (dataSet[rows, 2] == null) ? "0" : dataSet[rows, 2].ToString().Trim();
						invoiceModel.InvoiceDate = DateTime.ParseExact(new DateTime(1899, 12, 31).AddDays(Double.Parse(invoiceDate)).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
						invoiceModel.Customer = customers.Where(cus => cus.CustomerName.Trim() == validCustomer)?.FirstOrDefault();
						invoiceModel.Status = true;
						invoiceModel.UserName = "Exceluploader";

						partForInvoice.Vendor = vendors.Where(ven => ven.VenderName.Trim() == validVendor)?.FirstOrDefault();
						partForInvoice.ItemType = itemTypes.Where(itm => itm.ItemTypeName.Trim() == validItemType)?.FirstOrDefault();
						partForInvoice.PartNumber = (dataSet[rows, 6] == null) ? string.Empty : dataSet[rows, 6].ToString().Trim();
						partForInvoice.Description = (dataSet[rows, 7] == null) ? string.Empty : dataSet[rows, 7].ToString().Trim();
						partForInvoice.Quantity = Int32.Parse(quantity);
						partForInvoice.SerialNumber = (dataSet[rows, 9] == null) ? string.Empty : dataSet[rows, 9].ToString().Trim();
						partForInvoice.Supplier = suppliers.Where(sup => sup.SupplierName.Trim() == validSupplier)?.FirstOrDefault();
						partForInvoice.StartingDate = DateTime.ParseExact(new DateTime(1899, 12, 31).AddDays(Double.Parse(startingDate)).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
						partForInvoice.EndDate = DateTime.ParseExact(new DateTime(1899, 12, 31).AddDays(Double.Parse(endDate)).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
						partForInvoice.ExpirationPeriod = expirationPeriod.Where(exp=>exp.ExpirationPeriodName.Trim() == validExperiance)?.FirstOrDefault();
						partForInvoiceList.Add(partForInvoice);
						invoiceModel.PartsForInvoice = partForInvoiceList;
						invoiceModelList.Add(invoiceModel);
					}
				}

				foreach (var item in invoiceModelList)
				{
					AddInvoice(item);
				}
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in uploading excel row Id : " + rowId + " : " + ex.Message);
				throw;
			}
		}

		public virtual void AddInvoice(Invoice invoiceVm)
		{
			try
			{
				var customerIdIfNotAvailable = db.Customer.Where(cus=>cus.CustomerName == "N/A").FirstOrDefault().Id;
				var vendorIdIfNotAvailable = db.Vendors.Where(cus => cus.VenderName == "N/A").FirstOrDefault().Id;
				var itemTypeIdIfNotAvailable = db.ItemType.Where(cus => cus.ItemTypeName == "N/A").FirstOrDefault().Id;
				var expirationPeriodIdIfNotAvailable = db.ExpirationPeriod.Where(cus => cus.ExpirationPeriodName == "N/A").FirstOrDefault().Id;
				var supplierIdIfNotAvailable = db.Supplier.Where(cus => cus.SupplierName == "N/A").FirstOrDefault().Id;

				var invoiceModel = new Invoice();
				var partForInvoiceList = new List<PartsForInvoice>();
				invoiceModel.CustomerId = invoiceVm.Customer?.Id ?? customerIdIfNotAvailable;
				invoiceModel.Customer = invoiceVm.Customer;
				invoiceModel.InvoiceDate = invoiceVm.InvoiceDate;
				invoiceModel.InvoiceNumber = invoiceVm.InvoiceNumber;

				if (invoiceVm.PartsForInvoice != null && invoiceVm.PartsForInvoice.Count > 0)
				{
					foreach (var item in invoiceVm.PartsForInvoice)
					{
						var partForInvoice = new PartsForInvoice();
						partForInvoice.Id = Guid.NewGuid().ToString();
						partForInvoice.Description = item.Description;
						partForInvoice.EndDate = item.EndDate;
						partForInvoice.ExpirationPeriodId = (item.ExpirationPeriod == null) ? expirationPeriodIdIfNotAvailable : item.ExpirationPeriod.Id;
						partForInvoice.ItemTypeId = (item.ItemType == null) ? itemTypeIdIfNotAvailable : item.ItemType.Id;
						partForInvoice.PartNumber = item.PartNumber;
						partForInvoice.Quantity = item.Quantity;
						partForInvoice.SerialNumber = item.SerialNumber;
						partForInvoice.SupplierId = (item.Supplier == null) ? supplierIdIfNotAvailable : item.Supplier.Id;
						partForInvoice.StartingDate = item.StartingDate;
						partForInvoice.VendorId = (item.Vendor == null) ? vendorIdIfNotAvailable : item.Vendor.Id;
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
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in uploading excel" + ex.Message);
				throw;
			}
		}
	}
}