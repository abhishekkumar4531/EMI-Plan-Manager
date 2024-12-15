using CustomerLoanAllocation.Models.CustomerAndLoan;

namespace CustomerLoanAllocation.DataAccess.Interfaces
{
    public interface ICustomerAndLoanDataAccess
    {
        Task<CustomerAndLoanInput> FetchCustomerAndLoanDetails();

    }
}
