using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DCenterProject.Model;

namespace DCenterProject.Gateway
{
    public class TestReqGateway
    {
        public string connectionString = WebConfigurationManager.ConnectionStrings["DCenterConnectionString"].ConnectionString;

        public List<Test> GetAllTests()
        {
            List<Test> aList = new List<Test>();
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT TestID, TestName, Fee FROM Tests Order by TestName";
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


        public decimal IsTestExist(int testId)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT Fee FROM Tests WHERE TestID='"+testId+"'";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            decimal Fee = 0;
            Test aTest = new Test();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                aTest.Fee = Convert.ToDecimal(reader["Fee"]);
                Fee = aTest.Fee;
            }
            reader.Close();
            connection.Close();
            return Fee;
        }

        public int SavePatient(Patient patient)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "INSERT INTO Patient(Patient_Name, Birth_Date, Cell_No, Bill_No, Total_Amount) VALUES ('"+patient.PatientName+"', '"+patient.BirthDate+"', '"+patient.CellNo+"', '"+patient.BillNo+"', '"+patient.TotalFee+"')";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }

        public int SaveReqTest(TestRequest tReq)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "INSERT INTO PatientTest(P_Test_Id, P_Bill_No, P_Test_Date, P_Test_Time) VALUES ('" + tReq.TestReqId + "', '" + tReq.BillNo + "', '" + tReq.TestReqDate + "', '"+tReq.TestReqTime+"')";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }
    }
}