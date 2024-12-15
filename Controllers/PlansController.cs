using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.Plans.CreatePlan;
using CustomerLoanAllocation.Models.Plans.FetchSelectedPlan;
using Microsoft.AspNetCore.Mvc;

namespace CustomerLoanAllocation.Controllers
{
    public class PlansController : Controller
    {
        private IPlansDataAccess _plansDataAccess;

        public PlansController(IPlansDataAccess plansDataAccess)
        {
            _plansDataAccess = plansDataAccess;
        }

        public async Task<IActionResult> Index()
        {
            CreatePlanDetails createPlanList = new CreatePlanDetails();

            createPlanList = await _plansDataAccess.FetchPlans(0);

            return View(createPlanList);
        }

        public async Task<IActionResult> CreatePlan(CreatePlanDetails createPlanDetails)
        {
            bool isCreated = false;

            isCreated = await _plansDataAccess.CreatePlan(createPlanDetails);

            if (isCreated)
            {
                TempData["IsCreated"] = createPlanDetails.PlanDetails.PlanName + " created successfully.";
            }
            else
            {
                TempData["IsCreated"] = createPlanDetails.PlanDetails.PlanName + " not created!!!.";
            }

            return RedirectToAction("", "Plans");
        }

        public async Task<IActionResult> FetchSelectedPlan(int  PlanId)
        {
            CreatePlanDetails createPlanDetails = new CreatePlanDetails();

            //createPlanDetails = await _plansDataAccess.FetchPlans(fetchSelectedPlanInput.PlanId);
            createPlanDetails = await _plansDataAccess.FetchPlans(PlanId);

            return Json(createPlanDetails.PlanDetailsList[0]);
        }
    }
}
