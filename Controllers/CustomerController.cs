using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.Customer.Core;
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

        [HttpPost]
        public async Task<IActionResult> EditCustomer(FetchCustomerDetails fetchCustomerDetails)
        {
            bool isUpdated = await _customersDataAccess.UpdateCustomerDetails(fetchCustomerDetails.CustomerDetails);

            if (isUpdated)
            {
                TempData["isCustomerUpdated"] = "Customer updated successfully.";
            }
            else
            {
                TempData["isCustomerUpdated"] = "Failed to update customer.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer([FromBody] int customerId)
        {
            bool isDeleted = await _customersDataAccess.DeleteCustomer(customerId);

			if (isDeleted)
			{
				return Ok(new { message = "Customer deleted successfully." });
			}
			else
			{
				return BadRequest(new { message = "Failed to delete customer." });
			}
		}
    }
}
