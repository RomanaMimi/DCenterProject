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
    public class TestGateway
    {
        public string connectionString = WebConfigurationManager.ConnectionStrings["DCenterConnectionString"].ConnectionString;

        public int SaveTest(Test test)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "INSERT INTO Tests(TestName, Fee, TypeID) VALUES ('"+test.TestName+"', '"+test.Fee+"', '"+test.TypeId+"')";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }

        public bool IsTestExist(string testName)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT * FROM Tests WHERE TestName='"+testName+"'";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;
        }


        public List<ViewAllTestWithType> GetAllTestWithTypes()
        {
            List<ViewAllTestWithType> aList = new List<ViewAllTestWithType>();
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT * FROM ViewAllTestWithType ORDER BY TestName";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ViewAllTestWithType aTest = new ViewAllTestWithType();
                    aTest.TestName = reader["TestName"].ToString();
                    aTest.Fee = Convert.ToDecimal(reader["Fee"]);
                    aTest.TypeName = reader["TypeName"].ToString();
                    aList.Add(aTest);
                }
            }
            reader.Close();
            connection.Close();
            return aList;
        }
    }
}