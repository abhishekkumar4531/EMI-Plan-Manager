using CustomerLoanAllocation.Models.Plans.Core;
using System.ComponentModel.DataAnnotations;

namespace CustomerLoanAllocation.Models.Plans.CreatePlan
{
    public class CreatePlanDetails
    {
        public CreatePlanDetails()
        {
            PlanDetails = new PlanDetails();
            PlanDetailsList = new List<PlanDetails>();
        }

        public PlanDetails PlanDetails { get; set; }
        public List<PlanDetails> PlanDetailsList { get; set; }
    }
}
