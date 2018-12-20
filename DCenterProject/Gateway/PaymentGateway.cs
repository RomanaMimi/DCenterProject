using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCenterProject.Model;
using DCenterProject.Model.ViewModel;

namespace DCenterProject.Gateway
{
    public class PaymentGateway
    {
        public string connectionString = WebConfigurationManager.ConnectionStrings["DCenterConnectionString"].ConnectionString;

        public Payment GetDetailsWithBillNo(string bill_no)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT DISTINCT Patient_Name, Bill_No, P_Test_Date, Total_Amount FROM ViewAllPatientTest WHERE Bill_No='" + bill_no + "'";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            Payment p = new Payment();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    p.Patient_name = reader["Patient_Name"].ToString();
                    p.BillNo = reader["Bill_No"].ToString();
                    p.BillDate = reader["P_Test_Date"].ToString();
                    p.Total = Convert.ToDecimal(reader["Total_Amount"]);
                }
            }
            reader.Close();
            connection.Close();

            List<Test> pList = GetPatientTests(bill_no); /*get all requested tests of a patient*/
            if (pList.Count > 0)
            {
                p.PTestList = pList;
            }
            
            return p;
        }


        public List<Test> GetPatientTests(string billNo)
        {
            List<Test> aList = new List<Test>();
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT TestID, TestName, Fee FROM ViewAllPatientTest WHERE Bill_No='"+billNo+"'";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Test test1 = new Test();
                    test1.TestId = Convert.ToInt32(reader["TestID"]);
                    test1.TestName = reader["TestName"].ToString();
                    test1.Fee = Convert.ToDecimal(reader["Fee"]);
                    aList.Add(test1);
                }
            }
            reader.Close();
            connection.Close();
            return aList;
        }


        public int UpdateTotalAmount(Payment pay)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "UPDATE Patient SET Total_Amount='"+pay.Due+"' WHERE Bill_No='"+pay.BillNo+"'";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }

    }
}