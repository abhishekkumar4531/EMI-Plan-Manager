using System.ComponentModel.DataAnnotations;

namespace CustomerLoanAllocation.Models.Plans.Core
{
    public class PlanDetails
    {
        public PlanDetails()
        {
            DurationInMonth = 1;
            RateOfInterest = 1;
        }

        public int PlanId { get; set; }

        [Required]
        public string PlanName { get; set; }
        public int DurationInMonth { get; set; }
        public decimal RateOfInterest { get; set; }
    }
}
