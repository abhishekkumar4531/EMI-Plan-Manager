using CustomerLoanAllocation.Models.Customer.Core;
using CustomerLoanAllocation.Models.Customer.FetchCustomer;

namespace CustomerLoanAllocation.DataAccess.Interfaces
{
    public interface ICustomersDataAccess
    {
        Task<bool> AddCustomer(FetchCustomerDetails fetchCustomerDetails);
        Task<FetchCustomerDetails> FetchCustomerDetails(int customerId);
        Task<Boolean> UpdateCustomerDetails(CustomerDetails customerDetails);
        Task<Boolean> DeleteCustomer(int CustomerId);
    }
}
