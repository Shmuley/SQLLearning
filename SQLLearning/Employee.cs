using System;
using System.Collections.Generic;
using System.Text;

namespace SQLLearning.Data
{
    class Employee
    {
        public int BusinessEntityID { get; set; }
        public string NationalIDNumber { get; set; }
        public string LoginID { get; set; }
        public Int16? OrganizationLevel { get; set; }
        public string JobTitle { get; set; }
        public DateTime BirthDate { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public bool SalariedFlag { get; set; }
        public Int16 VacationHours { get; set; }
        public Int16 SickLeaveHours { get; set; }
        public bool CurrentFlag { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string? ToString()
        {
            return $"{BusinessEntityID.ToString()}, {LoginID}, {HireDate.ToString()}, {JobTitle}";
        }
    }
}
