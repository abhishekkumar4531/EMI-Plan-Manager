using CustomerLoanAllocation.Models.Customer.Core;

namespace CustomerLoanAllocation.Models.Customer.FetchCustomer
{
    public class FetchCustomerDetails
    {
        public FetchCustomerDetails()
        {
            CustomerDetails = new CustomerDetails();
            CustomerDetailsList = new List<CustomerDetails>();
        }

        public CustomerDetails CustomerDetails { get; set; }
        public List<CustomerDetails> CustomerDetailsList { get; set; }
    }
}
