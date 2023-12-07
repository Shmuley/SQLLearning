using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;

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
            List<Department> departments = [];
            List<Employee> employees = [];

            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                try
                {
                    while (rdr.Read())
                    {
                        departments.Add(new Department()
                        {
                            DepartmentID = rdr.GetFieldValue<short>("DepartmentID"),
                            GroupName = rdr.GetFieldValue<string>("GroupName"),
                            Name = rdr.GetFieldValue<string>("Name"),
                            ModifiedDate = rdr.GetFieldValue<DateTime>("ModifiedDate")
                        });
                    }

                    rdr.NextResult();

                    while (rdr.Read())
                    {
                        employees.Add(new Employee()
                        {
                            BusinessEntityID = rdr.GetFieldValue<int>("BusinessEntityID"),
                            BirthDate = rdr.GetFieldValue<DateTime>("BirthDate"),
                            CurrentFlag = rdr.GetFieldValue<bool>("CurrentFlag"),
                            Gender = rdr.GetFieldValue<string>("Gender"),
                            HireDate = rdr.GetFieldValue<DateTime>("HireDate"),
                            JobTitle = rdr.GetFieldValue<string>("JobTitle"),
                            LoginID = rdr.GetFieldValue<string>("LoginID"),
                            MaritalStatus = rdr.GetFieldValue<string>("MaritalStatus"),
                            ModifiedDate = rdr.GetFieldValue<DateTime>("ModifiedDate"),
                            NationalIDNumber = rdr.GetFieldValue<string>("NationalIDNumber"),
                            OrganizationLevel = rdr.GetFieldValue<short?>("OrganizationLevel"),
                            rowguid = rdr.GetFieldValue<Guid>("rowguid"),
                            SalariedFlag = rdr.GetFieldValue<bool>("SalariedFlag"),
                            SickLeaveHours = rdr.GetFieldValue<short>("SickLeaveHours"),
                            VacationHours = rdr.GetFieldValue<short>("VacationHours")
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

                    employees = employees.OrderBy(e => e.BusinessEntityID).ToList();

                    foreach (var emp in employees)
                    {
                        Console.WriteLine(emp.ToString());
                    }

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
