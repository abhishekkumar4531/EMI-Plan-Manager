using CustomerLoanAllocation.Models.Plans.CreatePlan;

namespace CustomerLoanAllocation.DataAccess.Interfaces
{
    public interface IPlansDataAccess
    {
        Task<bool> CreatePlan(CreatePlanDetails createPlanDetails);
        Task<CreatePlanDetails> FetchPlans(int planId);
    }
}
