using SQLLearning.Data;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Configuration;

namespace SQLLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            string queryString =
                "SELECT * from HumanResources.Department;" +
                "SELECT * from HumanResources.Employee;";

            string paramQueryString =
                "SELECT * from Person.Person WHERE LastName LIKE @LastNameLike + '%'";

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
                {
                    ConnectedDataLayer(connection, queryString);
                    //DisconnectedDataLayer(connection, dataAdapter, queryString);
                    //DisconnectedDataLayerWithParameters(connection, dataAdapter, paramQueryString);
                }
            }
        }

        static void ConnectedDataLayer(SqlConnection connection, string queryString)
        {
            try
            {
                var cdl = new ConnectedDataLayer(connection);

                string insertString = "" +
                    "insert into HumanResources.Department " +
                    "(Name, GroupName, ModifiedDate) " +
                    $"values ('Operations' , 'Information Technology' , '{DateTime.Now}')";

                string updateString = "" +
                    "update HumanResources.Department " +
                    "set GroupName = 'IT' " +
                    "where GroupName = 'Information Technology'";

                string delString =
                    "delete from HumanResources.Department " +
                    "where Name = 'Operations'";

                string getCountString =
                    "select count(*) " +
                    "from HumanResources.Department";

                connection.Open();
                cdl.QueryDataAsync(queryString);
                cdl.InsertData(insertString);
                cdl.QueryDataAsync(queryString);
                cdl.UpdateData(updateString);
                cdl.QueryDataAsync(queryString);
                cdl.DeleteData(delString);
                cdl.QueryDataAsync(queryString);
                cdl.GetSingleValueData(getCountString);

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void DisconnectedDataLayer(SqlConnection connection, SqlDataAdapter dataAdapter, string queryString)
        {
            var ddl = new DisconnectedDataLayer(connection, dataAdapter);

            string tableName = "Departments";
            var data = ddl.QueryData(queryString, tableName);

            #region Adding Row to Table
            var table = data.Tables[tableName];
            var newRow = table.NewRow();

            newRow["Name"] = "Operations";
            newRow["GroupName"] = "IT";
            newRow["ModifiedDate"] = DateTime.Now;
            table.Rows.Add(newRow);
            #endregion

            try
            {
                ddl.InsertData(data, tableName);
                data = ddl.QueryData(queryString, tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            #region Getting Row to Delete
            var rowsToDelete = data.Tables[tableName]
                .AsEnumerable()
                .Where(r => r.Field<string>("GroupName") == "IT")
                .ToList();
            #endregion

            var deletedData = ddl.DeleteData(data, rowsToDelete, tableName);
        }
        static void DisconnectedDataLayerWithParameters(SqlConnection connection, SqlDataAdapter dataAdapter, string paramQueryString)
        {
            var tableName = "Persons";
            var ddl = new DisconnectedDataLayer(connection, dataAdapter);
            var param = new SqlParameter()
            {
                ParameterName = "@LastNameLike",
                Value = "Hernan"
            };

            var personData = ddl.QueryData(paramQueryString, param, tableName);

            foreach (DataRow row in personData.Tables[tableName].AsEnumerable())
            {
                Console.WriteLine($"{row.Field<string>("FirstName")} {row.Field<string>("LastName")}");
            }
        }
    }
}
