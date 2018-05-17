using MicroSolutions.DAL;
using MicroSolutions.Data.Models;
using MicroSolutions.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace MicroSolutions.Web.Controllers
{
	[Authorize(Roles = "Host,Administrator")]
    public class CustomerController : Controller
    {
		private static Logger logger = LogManager.GetCurrentClassLogger();
	
		private MicroSolutionsContext db = new MicroSolutionsContext();

		// GET: Customer
        [HttpGet]
		public ActionResult Index()
        {
			try
			{
				var customersVm = ReturnAllCustomersViewModel();
				return View(customersVm);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Customer -> index: " + ex.Message);
				throw;
			}
        }

		[HttpGet]
		public ActionResult AddCustomer() {
			try
			{
				var customerViewModel = new CustomerViewModel();
				return View(customerViewModel);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Customer -> AddCustomer: " + ex.Message);
				throw;
			}
		}

		[HttpPost]
		public ActionResult AddCustomer(CustomerViewModel customerViewModel)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var isExistingCustomer = db.Customer.ToList().Where(c => c.CustomerName.Equals(customerViewModel.CustomerName, StringComparison.OrdinalIgnoreCase)).ToList().Count > 0;

					if (isExistingCustomer)
					{
						TempData["Message"] = "Customer already exist in the list.";
						TempData["MessageType"] = "alert-danger";
						return RedirectToAction("Index");
					}

					var customer = new Customer();
					customer.CustomerName = customerViewModel.CustomerName;
					customer.ContactPersonName = customerViewModel.ContactPersonName;
					customer.ContactNumber = customerViewModel.ContactNumber;
					customer.Address = customerViewModel.Address;
					customer.Memorandum = customerViewModel.Memorandum;
					customer.Status = true;
					customer.UserName = WebSecurity.CurrentUserName;
					customer.ModifiedDate = DateTime.Now;

					TryUpdateModel(customer, "Id,CustomerName,ContactPersonName,ContactNumber,Address,Memorandum,Status");
					db.Customer.Add(customer);
					db.Entry(customer).State = System.Data.Entity.EntityState.Added;
					db.SaveChanges();
					TempData["Message"] = "Customer saved successfully. Customer name is: " + customerViewModel.CustomerName;
					TempData["MessageType"] = "alert-success";
				}
				else
				{
					TempData["Message"] = string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
					TempData["MessageType"] = "alert-danger";
					return View(customerViewModel);
				}
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Customer -> AddCustomer[POST]: " + ex.Message);
				throw;
			}
		}

		public ActionResult Edit(int id)
		{
			try
			{
				var customerToUpdate = db.Customer.ToList().Where(c => c.Id == id).FirstOrDefault();
                var customerVM = new CustomerViewModel();

                if (customerToUpdate == null)
                {
                    TempData["Message"] = "Customer does not exist.";
                    TempData["MessageType"] = "alert-danger";
                    return RedirectToAction("Index");
                }
				
				customerVM.CustomerName = customerToUpdate.CustomerName;
				customerVM.ContactPersonName = customerToUpdate.ContactPersonName;
				customerVM.ContactNumber = customerToUpdate.ContactNumber;
				customerVM.Address = customerToUpdate.Address;
				customerVM.Memorandum = customerToUpdate.Memorandum;
				customerVM.Status = customerToUpdate.Status;
				return View(customerVM);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Customer -> Edit[GET]: " + ex.Message);
				throw;
			}
		}

		[HttpPost]
		public ActionResult Edit(CustomerViewModel customerVM)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var customerToUpdate = db.Customer.Find(customerVM.Id);
					var customer = new Customer();
					customerToUpdate.CustomerName = customerVM.CustomerName;
					customerToUpdate.ContactPersonName = customerVM.ContactPersonName;
					customerToUpdate.ContactNumber = customerVM.ContactNumber;
					customerToUpdate.Address = customerVM.Address;
					customerToUpdate.Memorandum = customerVM.Memorandum;
					customerToUpdate.Status = customerVM.Status;
					customerToUpdate.UserName = WebSecurity.CurrentUserName;
					customerToUpdate.ModifiedDate = DateTime.Now;
					TryUpdateModel(customerToUpdate, "Id,CustomerName,ContactPersonName,ContactNumber,Address,Memorandum,Status,UserName");
					db.Entry(customerToUpdate).State = System.Data.Entity.EntityState.Modified;
					db.SaveChanges();
					TempData["Message"] = "Customer saved successfully. Customer name is: " + customerVM.CustomerName;
					TempData["MessageType"] = "alert-success";
				}
				else
				{
					TempData["Message"] = string.Join(",", string.Join(" , ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
					TempData["MessageType"] = "alert-danger";
					return View(customerVM);
				}
				var customers = ReturnAllCustomersViewModel();
				return View("Index", customers);
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Customer -> Edit[POST]: " + ex.Message);
				throw;
			}
		}

		public IList<CustomerViewModel> ReturnAllCustomersViewModel()
		{
			try
			{
				var customersVm = new List<CustomerViewModel>();
				var customers = db.Customer.ToList().Where(c => c.Status == true);
				foreach (var item in customers)
				{
					var customerVm = new CustomerViewModel();
					customerVm.Id = item.Id;
					customerVm.CustomerName = item.CustomerName;
					customerVm.ContactPersonName = item.ContactPersonName;
					customerVm.ContactNumber = item.ContactNumber;
					customerVm.Address = item.Address;
					customerVm.Memorandum = item.Memorandum;
					customersVm.Add(customerVm);
				}
				return customersVm;
			}
			catch (Exception ex)
			{
				logger.Log(LogLevel.Error, "Exception occurred in Customer -> ReturnAllCustomersViewModel: " + ex.Message);
				throw;
			}
		}
	}
}