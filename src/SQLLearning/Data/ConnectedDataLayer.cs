using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLLearning.Data
{
    public class ConnectedDataLayer
    {
        private readonly SqlConnection connection;

        public ConnectedDataLayer(SqlConnection connection)
        {
            this.connection = connection;
        }

        public void QueryDataAsync(string queryString)
        {
            SqlCommand cmd = new SqlCommand(queryString, connection);
            List<Department> departments = new List<Department>();

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                try
                {
                    while (rdr.Read())
                    {
                        departments.Add(new Department()
                        {
                            DepartmentID = rdr.GetInt16(rdr.GetOrdinal("DepartmentID")),
                            GroupName = rdr.GetString(rdr.GetOrdinal("GroupName")),
                            Name = rdr.GetString(rdr.GetOrdinal("Name")),
                            ModifiedDate = rdr.GetDateTime(rdr.GetOrdinal("ModifiedDate"))
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    departments = departments.OrderBy(d => d.DepartmentID).ToList();

                    foreach(var department in departments)
                    {
                        Console.WriteLine(department.ToString());
                    }
                    Console.WriteLine("=======================================================");
                } 
            }
        }
        public void InsertData(string insertString)
        {
            try
            {
                SqlCommand insertCmd = new SqlCommand(insertString, connection);
                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void UpdateData(string updateString)
        {
            try
            {
                SqlCommand updateCmd = new SqlCommand(updateString, connection);
                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteData(string delString)
        {
            try
            {
                SqlCommand delCmd = new SqlCommand(delString, connection);
                delCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void GetSingleValueData(string getCountString)
        {
            try
            {
                SqlCommand getCountCmd = new SqlCommand(getCountString, connection);
                var count = (int)getCountCmd.ExecuteScalar();
                Console.WriteLine($"Number of Departments: {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
