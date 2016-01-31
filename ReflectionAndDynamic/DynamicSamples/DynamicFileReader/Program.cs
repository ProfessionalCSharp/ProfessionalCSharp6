using static System.Console;

namespace DynamicFileReader
{
    class Program
    {
        static void Main()
        {
            var helper = new DynamicFileHelper();
            var employeeList = helper.ParseFile("EmployeeList.txt");
            foreach (var employee in employeeList)
            {
                WriteLine($"{employee.FirstName} {employee.LastName} lives in {employee.City}, {employee.State}.");
            }
            ReadLine();
        }
    }
}
