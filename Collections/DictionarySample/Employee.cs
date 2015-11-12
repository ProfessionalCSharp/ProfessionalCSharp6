using System;

namespace Wrox.ProCSharp.Collections
{
    public class Employee
    {
        private string _name;
        private decimal _salary;
        private readonly EmployeeId _id;

        public Employee(EmployeeId id, string name, decimal salary)
        {
            _id = id;
            _name = name;
            _salary = salary;
        }

        public override string ToString() => $"{_id.ToString()}: {_name, -20} {_salary :C}";

    }

}
