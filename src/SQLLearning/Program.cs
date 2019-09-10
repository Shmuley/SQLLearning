using SQLLearning.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SQLLearning
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString =
                "Data Source=HFSQL01;Initial Catalog=AdventureWorks2017;"
                + "Integrated Security=true";

            string queryString =
                "SELECT * from HumanResources.Department";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var cdl = new ConnectedDataLayer(connection);
                var ddl = new DisconnectedDataLayer(connection);

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
                    data = ddl.InsertData(data, tableName);
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

                #region Connected Layer
                //try
                //{
                //    string insertString = "" +
                //        "insert into HumanResources.Department " +
                //        "(Name, GroupName, ModifiedDate) " +
                //        "values ('Operations' , 'Information Technology' , '{DateTime.Now}')";

                //    string updateString = "" +
                //        "update HumanResources.Department " +
                //        "set GroupName = 'IT' " +
                //        "where GroupName = 'Information Technology'";

                //    string delString =
                //        "delete from HumanResources.Department " +
                //        "where Name = 'Operations'";

                //    string getCountString =
                //        "select count(*) " +
                //        "from HumanResources.Department";

                //    connection.Open();
                //    cdl.QueryDataAsync(queryString);
                //    cdl.InsertData(insertString);
                //    cdl.UpdateData(updateString);
                //    cdl.DeleteData(delString);
                //    cdl.GetSingleValueData(getCountString);

                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                #endregion
            }
        }
    }
}