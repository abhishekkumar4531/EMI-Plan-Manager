using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.Plans.Core;
using CustomerLoanAllocation.Models.Plans.CreatePlan;
using System.Data.SqlClient;

namespace CustomerLoanAllocation.DataAccess.Services
{
    public class PlansDataAccess : IPlansDataAccess
    {
        private IConfiguration _configuration;

        public PlansDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreatePlan(CreatePlanDetails createPlanDetails)
        {
            bool isCreated = false;

            try
            {
                int rows = 0;
                string CS = _configuration.GetConnectionString("ProjectDataBase");

                string CreatePlanQuery =
                @"
                    INSERT INTO STOREPLAN
                    (
                         PLANNAME,
                         PLANDURATION,
                         RATEOFINTEREST,
                         STATUS
                    )
                    VALUES      
                    ( 
                          @PLAN_NAME,
                          @PLAN_DURATION,
                          @RATE_OF_INTEREST,
                          1
                    );
                ";

                PlanDetails planDetails = new PlanDetails();
                planDetails = createPlanDetails.PlanDetails;

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(CreatePlanQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@PLAN_NAME", planDetails.PlanName);
                        cmd.Parameters.AddWithValue("@PLAN_DURATION", planDetails.DurationInMonth);
                        cmd.Parameters.AddWithValue("@RATE_OF_INTEREST", planDetails.RateOfInterest);

                        rows = cmd.ExecuteNonQuery();
                    }
                }

                if (rows > 0)
                {
                    isCreated = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isCreated;
        }

        public async Task<CreatePlanDetails> FetchPlans(int planId)
        {
            CreatePlanDetails createPlanDetailList = new CreatePlanDetails();

            try
            {
                string CS = _configuration.GetConnectionString("ProjectDataBase");

                string FetchPlansQuery =
                @"
                    SELECT ID,
                           PLANNAME,
                           PLANDURATION,
                           RATEOFINTEREST
                    FROM   STOREPLAN
                    WHERE  STATUS = 1
                ";

                if(planId > 0)
                {
                    FetchPlansQuery += " AND ID = @PLAN_ID ";
                }

                List<PlanDetails> plansDetailsList = new List<PlanDetails>();

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(FetchPlansQuery, con))
                    {
                        if (planId > 0)
                        {
                            cmd.Parameters.AddWithValue("@PLAN_ID", planId);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    PlanDetails planDetails = new PlanDetails();

                                    planDetails.PlanId = Convert.ToInt32(dr["ID"]);
                                    planDetails.PlanName = Convert.ToString(dr["PLANNAME"]);
                                    planDetails.DurationInMonth = Convert.ToInt32(dr["PLANDURATION"]);
                                    planDetails.RateOfInterest = Convert.ToDecimal(dr["RATEOFINTEREST"]);

                                    plansDetailsList.Add(planDetails);
                                }

                                createPlanDetailList.PlanDetailsList = plansDetailsList;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return createPlanDetailList;
        }
    }
}
