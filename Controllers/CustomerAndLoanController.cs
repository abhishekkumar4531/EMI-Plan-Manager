using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.CustomerAndLoan;
using Microsoft.AspNetCore.Mvc;

namespace CustomerLoanAllocation.Controllers
{
    public class CustomerAndLoanController : Controller
    {
        private ICustomerAndLoanDataAccess _customerAndLoanDataAccess;

        public CustomerAndLoanController(ICustomerAndLoanDataAccess customerAndLoanDataAccess)
        {
            _customerAndLoanDataAccess = customerAndLoanDataAccess;
        }

        public async Task<IActionResult> Index()
        {
            CustomerAndLoanInput customerAndLoanInput = new CustomerAndLoanInput();

            customerAndLoanInput = await _customerAndLoanDataAccess.FetchCustomerAndLoanDetails();

            return View(customerAndLoanInput);
        }

        public async Task<IActionResult> AllocateLoanToCustomer()
        {
            return View();
        }
    }
}
