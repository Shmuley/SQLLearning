using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            SqlDataReader rdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand(queryString, connection);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        Console.Write($"{rdr[i]}, ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                Console.WriteLine("----------------------------------------------");
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
