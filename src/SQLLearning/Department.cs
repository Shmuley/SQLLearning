using System;
using System.Collections.Generic;
using System.Text;

namespace SQLLearning
{
    class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString()
        {
            return $"Department ID: {DepartmentID.ToString()} Name: {Name}, Group Name: {GroupName}, Last Modified: {ModifiedDate.ToString()}";
        }
    }
}
