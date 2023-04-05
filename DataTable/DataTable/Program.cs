using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTable
{
    public class program
    {
        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("***********************");
            Console.WriteLine("* Student Data Table: *");
            Console.WriteLine("***********************");

            Console.ForegroundColor = ConsoleColor.Green;
            var students = new List<Student>
            {
            new Student { ID=1, Name="Mary", Address="India", MobileNumber = "1234567898" },
            new Student { ID=2, Name="George", Address="America", MobileNumber = "876543290" },
            new Student { ID=3, Name="Sriya", Address="Italy", MobileNumber = "3456781980" },
            new Student { ID=4, Name="John", Address="Africa", MobileNumber = "4563098760" },
            new Student { ID=5, Name="Smith", Address="Germany", MobileNumber = "3457812987" }
            };

            ConsoleDisplayFormatter.PrintSeperatorLine();
            ConsoleDisplayFormatter.PrintRow("ID", "Name", "Address", "MobileNumber");
            ConsoleDisplayFormatter.PrintSeperatorLine();

            foreach(var student in students)
            {
                ConsoleDisplayFormatter.PrintRow(student.ID.ToString(), student.Name, student.Address, student.MobileNumber);
            }
            ConsoleDisplayFormatter.PrintSeperatorLine();


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("************************");
            Console.WriteLine("* Employee Data Table: *");
            Console.WriteLine("************************");

            Console.ForegroundColor = ConsoleColor.Red;
            var employees = new List<Employee>
            {
                new Employee{EmpID = 1, ManagerName = "Joy"},
                new Employee{EmpID = 2, ManagerName = "Shan"},
                new Employee{EmpID = 3, ManagerName = "Ruby"},
                new Employee{EmpID = 4, ManagerName = "Diya"},
            };

            ConsoleDisplayFormatter.PrintSeperatorLine();
            ConsoleDisplayFormatter.PrintRow("EmpID", "ManagerName");
            ConsoleDisplayFormatter.PrintSeperatorLine();

            foreach (var employee in employees)
            {
                ConsoleDisplayFormatter.PrintRow(employee.EmpID.ToString(), employee.ManagerName);
            }
            ConsoleDisplayFormatter.PrintSeperatorLine();

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
