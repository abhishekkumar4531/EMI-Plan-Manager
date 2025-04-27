using CustomerLoanAllocation.DataAccess.Interfaces;
using CustomerLoanAllocation.Models.Customer.Core;
using CustomerLoanAllocation.Models.Customer.FetchCustomer;
using System.Data.SqlClient;

namespace CustomerLoanAllocation.DataAccess.Services
{
    public class CustomerDataAccess : ICustomersDataAccess
    {
        private IConfiguration _configuration;

        public CustomerDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> AddCustomer(FetchCustomerDetails fetchCustomerDetails)
        {
            bool isAdded = false;

            try
            {
                int rows = 0;
                string CS = _configuration.GetConnectionString("ProjectDataBase");

                string AddCustomerQuery =
                @"
                    INSERT INTO EMPLOYEES
                    (   
                        EMPFIRSTNAME,
                        EMPLASTNAME,
                        EMPMOBILENUMBER,
                        EMPEMAILADDRESS,
                        EMPADDRESS,
                        EMPIMAGEURL,
                        EMPPASSWORD,
                        EMPGENDER,
                        ACTIVE
                    )
                    VALUES      
                    ( 
                        @FIRSTNAME,
                        @LASTNAME,
                        @MOBILENUMBER,
                        @EMAILADDRESS,
                        @ADDRESS,
                        '',
                        @PASSWORD,
                        @GENDER,
                        1
                    ); 
                ";

                CustomerDetails customerDetails = new CustomerDetails();
                customerDetails = fetchCustomerDetails.CustomerDetails;

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(AddCustomerQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@FIRSTNAME", customerDetails.CustomerFirstName);
                        cmd.Parameters.AddWithValue("@LASTNAME", customerDetails.CustomerLastName);
                        cmd.Parameters.AddWithValue("@MOBILENUMBER", customerDetails.CustomerMobile);
                        cmd.Parameters.AddWithValue("@EMAILADDRESS", customerDetails.CustomerEmail);
                        cmd.Parameters.AddWithValue("@ADDRESS", customerDetails.CustomerAddress);
                        cmd.Parameters.AddWithValue("@PASSWORD", customerDetails.CustomerPassword);
                        cmd.Parameters.AddWithValue("@GENDER", customerDetails.CustomerGender);

                        rows = cmd.ExecuteNonQuery();
                    }
                }

                if (rows > 0)
                {
                    isAdded = true;
                }
            }
            catch (Exception ex)
            {

            }

            return isAdded;
        }

        public async Task<FetchCustomerDetails> FetchCustomerDetails(int customerId)
        {
            FetchCustomerDetails fetchCustomerDetails = new FetchCustomerDetails();

            try
            {
                string CS = _configuration.GetConnectionString("ProjectDataBase");

                string FetchCustomerDetailsQuery =
                @"
                    SELECT EMPID,
                           EMPFIRSTNAME,
                           EMPLASTNAME,
                           EMPMOBILENUMBER,
                           EMPEMAILADDRESS,
                           EMPADDRESS,
                           EMPIMAGEURL,
                           EMPGENDER
                    FROM   EMPLOYEES
                    WHERE ACTIVE = 1
                ";

                if(customerId != 0)
                {
                    FetchCustomerDetailsQuery += " AND EMPID = @CUSTOMER_ID ";
                }

                List<CustomerDetails> customerDetailsList = new List<CustomerDetails>();

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(FetchCustomerDetailsQuery, con))
                    {
                        if (customerId != 0)
                        {
                            cmd.Parameters.AddWithValue("@CUSTOMER_ID", customerId);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CustomerDetails customerDetails = new CustomerDetails();

                                    customerDetails.CustomerId = Convert.ToInt32(dr["EMPID"]);
                                    customerDetails.CustomerFirstName = Convert.ToString(dr["EMPFIRSTNAME"]);
                                    customerDetails.CustomerLastName = Convert.ToString(dr["EMPLASTNAME"]);
                                    customerDetails.CustomerMobile = Convert.ToString(dr["EMPMOBILENUMBER"]);
                                    customerDetails.CustomerEmail = Convert.ToString(dr["EMPEMAILADDRESS"]);
                                    customerDetails.CustomerAddress = Convert.ToString(dr["EMPADDRESS"]);
                                    customerDetails.CustomerImage = Convert.ToString(dr["EMPIMAGEURL"]);
                                    customerDetails.CustomerGender = (Convert.ToString(dr["EMPGENDER"]) == "") ? "UN" : Convert.ToString(dr["EMPGENDER"]);

                                    customerDetailsList.Add(customerDetails);
                                }

                                fetchCustomerDetails.CustomerDetailsList = customerDetailsList;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return fetchCustomerDetails;
        }

        public async Task<Boolean> UpdateCustomerDetails(CustomerDetails customerDetails)
        {
            bool updateStatus = false;

            try
            {
                string CS = _configuration.GetConnectionString("ProjectDataBase");

                string UpdateCustomerDetailsQuery =
                @"
                    UPDATE EMPLOYEES
                    SET    EMPFIRSTNAME = @FIRSTNAME,
                           EMPLASTNAME = @LASTNAME,
                           EMPMOBILENUMBER = @MOBILENUMBER,
                           EMPEMAILADDRESS = @EMAILADDRESS,
                           EMPADDRESS = @ADDRESS,
                           EMPIMAGEURL = @IMAGEURL,
                           EMPGENDER = @GENDER
                    WHERE  EMPID = @CUSTOMER_ID 
                ";

                using(SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(UpdateCustomerDetailsQuery, con))
                    {

                        cmd.Parameters.AddWithValue("@FIRSTNAME", customerDetails.CustomerFirstName);
                        cmd.Parameters.AddWithValue("@LASTNAME", customerDetails.CustomerLastName);
                        cmd.Parameters.AddWithValue("@MOBILENUMBER", customerDetails.CustomerMobile);
                        cmd.Parameters.AddWithValue("@EMAILADDRESS", customerDetails.CustomerEmail);
                        cmd.Parameters.AddWithValue("@ADDRESS", customerDetails.CustomerAddress);
                        cmd.Parameters.AddWithValue("@IMAGEURL", (customerDetails.CustomerImage == null) ? ' ' : customerDetails.CustomerImage);
                        cmd.Parameters.AddWithValue("@GENDER", customerDetails.CustomerGender);
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", customerDetails.CustomerId);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            updateStatus = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                updateStatus = true;
            }

            return updateStatus;
        }

        public async Task<Boolean> DeleteCustomer(int CustomerId)
        {
            bool deleteStatus = false;

            try
            {
                string CS = _configuration.GetConnectionString("ProjectDataBase");

                string DeleteCustomerQuery =
                @"
                    UPDATE EMPLOYEES
                    SET    ACTIVE = 0
                    WHERE  EMPID = @CUSTOMER_ID 
                ";

                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(DeleteCustomerQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@CUSTOMER_ID", CustomerId);

                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            deleteStatus = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                deleteStatus = false;
            }

            return deleteStatus;
        }

    }
}
