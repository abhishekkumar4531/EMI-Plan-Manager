using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.Customer.Core;
using CustomerLoanAllocation.Models.Customer.FetchCustomer;
using CustomerLoanAllocation.Models.CustomerAndLoan;
using CustomerLoanAllocation.Models.Plans.Core;
using CustomerLoanAllocation.Models.Plans.CreatePlan;

namespace CustomerLoanAllocation.DataAccess.Services
{
    public class CustomerAndLoanDataAccess : ICustomerAndLoanDataAccess
    {
        private ICustomersDataAccess _customersDataAccess;
        private IPlansDataAccess _plansDataAccess;

        public CustomerAndLoanDataAccess(ICustomersDataAccess customersDataAccess, IPlansDataAccess plansDataAccess)
        {
            _customersDataAccess = customersDataAccess;
            _plansDataAccess = plansDataAccess;
        }

        public async Task<CustomerAndLoanInput> FetchCustomerAndLoanDetails()
        {
            CustomerAndLoanInput customerAndLoanInput = new CustomerAndLoanInput();

            try
            {
                List<CustomerDetails> customerDetails = new List<CustomerDetails>();
                List<PlanDetails> planDetails = new List<PlanDetails>();

                FetchCustomerDetails fetchCustomerDetails = new FetchCustomerDetails();

                fetchCustomerDetails = await _customersDataAccess.FetchCustomerDetails(0);

                customerDetails = fetchCustomerDetails.CustomerDetailsList;

                CreatePlanDetails createPlanDetails = new CreatePlanDetails();

                createPlanDetails = await _plansDataAccess.FetchPlans(0);

                planDetails = createPlanDetails.PlanDetailsList;

                customerAndLoanInput.CustomerDetailsList = customerDetails;
                customerAndLoanInput.PlanDetailsList = planDetails;
            }
            catch (Exception ex)
            {

            }

            return customerAndLoanInput;
        }
    }
}
