using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Type = DCenterProject.Model.Type;

namespace DCenterProject.Gateway
{
    public class TypeGateway
    {
        //TODO: connection( add connection address)
        public string connectionString = WebConfigurationManager.ConnectionStrings["DCenterConnectionString"].ConnectionString;

        public int SaveType(Type type)
        {
            //connection
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "INSERT INTO Types (TypeName) VALUES('" + type.TypeName + "')";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }


        public bool IsTypeExist(string typeName1) // checking same product exists or not
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString;
            string query = "SELECT * FROM Types WHERE TypeName='" + typeName1 + "'";
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = query;
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            bool isExist = reader.HasRows; // if table has list of products(product rows)->return true, otherwise false
            reader.Close();
            connection.Close();
            return isExist;
        }


        public List<Type> GetAllTypes()
        {
            List<Type> typeList = new List<Type>();
            SqlConnection connection = new SqlConnection(connectionString);
            //query
            string query = "SELECT * FROM Types ORDER BY TypeName";
            //execute
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows) // if table has list of products(product rows) or not
            {
                while (reader.Read()) //then continue its reading for each row
                {
                    Type aType = new Type();
                    aType.TypeId = Convert.ToInt32(reader["TypeID"]);
                    aType.TypeName = reader["TypeName"].ToString();
                    typeList.Add(aType); // add types on list

                }
            }
            reader.Close();
            connection.Close();

            return typeList;
        }
    }
}