using CustomerLoanAllocation.Models.Plans.Core;
using CustomerLoanAllocation.Models.Plans.CreatePlan;

namespace CustomerLoanAllocation.DataAccess.Interfaces
{
    public interface IPlansDataAccess
    {
        Task<bool> CreatePlan(CreatePlanDetails createPlanDetails);
        Task<CreatePlanDetails> FetchPlans(int planId);
        Task<Boolean> UpdatePlanDetails(PlanDetails planDetails);
        Task<Boolean> DeletePaln(int planId);
    }
}
