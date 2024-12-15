using CustomerLoanAllocation.Models.Customer.FetchCustomer;

namespace CustomerLoanAllocation.DataAccess.Interfaces
{
    public interface ICustomersDataAccess
    {
        Task<bool> AddCustomer(FetchCustomerDetails fetchCustomerDetails);
        Task<FetchCustomerDetails> FetchCustomerDetails(int customerId);
    }
}
