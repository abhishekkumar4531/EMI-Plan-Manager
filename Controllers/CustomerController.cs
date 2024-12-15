using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.Customer.FetchCustomer;
using Microsoft.AspNetCore.Mvc;

namespace CustomerLoanAllocation.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomersDataAccess _customersDataAccess;
        public CustomerController(ICustomersDataAccess customersDataAccess)
        {
            _customersDataAccess = customersDataAccess;
        }

        public async  Task<IActionResult> Index()
        {
            FetchCustomerDetails fetchCustomerDetails = new FetchCustomerDetails();

            fetchCustomerDetails = await _customersDataAccess.FetchCustomerDetails(0);

            return View(fetchCustomerDetails);
        }

        public async Task<IActionResult> AddCustomer(FetchCustomerDetails fetchCustomerDetails)
        {
            bool isAdded = false;

            isAdded = await _customersDataAccess.AddCustomer(fetchCustomerDetails);

            if(isAdded)
            {
                TempData["isCustomerAdded"] = "New Customer added successfuly.";
            }
            else
            {
                TempData["isCustomerAdded"] = "Something went wromg! Customer not added.";
            }

            return RedirectToAction("", "Customers");
        }
    }
}
