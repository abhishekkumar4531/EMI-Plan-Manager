using CustomerLoanAllocation.Models.Customer.Core;
using CustomerLoanAllocation.Models.Plans.Core;

namespace CustomerLoanAllocation.Models.CustomerAndLoan
{
    public class CustomerAndLoanInput
    {
        public CustomerAndLoanInput()
        {
            PlanDetails = new PlanDetails();
            PlanDetailsList = new List<PlanDetails>();
            CustomerDetailsList = new List<CustomerDetails>();
        }

        public int CustomerId { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal EMIAmount { get; set; }
        public string LoanStartDate { get; set; }
        public string LoanEndDate { get; set; }

        public List<CustomerDetails> CustomerDetailsList { get; set; }
        public List<PlanDetails> PlanDetailsList { get; set; }
        public PlanDetails PlanDetails { get; set; }
    }
}
