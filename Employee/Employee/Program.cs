using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using static Employee.Program;
using Formatting = Newtonsoft.Json.Formatting;

namespace Employee
{
    public static class MyExtensions
    {
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            var regex = new Regex(pattern);
            var match = regex.Match(email);
            return match.Success;
        }

        public static bool IsValidPhoneNumber(this long number)
        {
            return number.ToString().Length <= 10;
        }

        public static bool IsValidSalary(this double salary)
        {
            return salary >= 10000 && salary <= 50000;
        }

        public static bool IsValidDepartment(this string desig)
        {
            return Enum.GetNames(typeof(Department)).Contains(desig);
        }
    }

    class Program
    {
        public enum Gender
        {
            M,
            F
        }
        public enum Department
        {
            Sales =1,
            Marketing=2,
            Development=3,
            QA=4,
            HR=5,
            SEO=6
        }
        public class Employee
        {
            public int EmployeeID { get; set; }
            public string Name { get; set; }
            public DateTime DOB { get; set; }
            public Gender Gender { get; set; }
            public string Designation { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public Int64 Postcode { get; set; }
            public long PhoneNumber { get; set; }
            public string Email { get; set; }
            public DateTime DateOfJoining { get; set; }
            public string TotalExperience { get; set; }
            public string Remarks { get; set; }
            public Department Department { get; set; }
            public double MonthlySalary { get; set; }
        }

       
        static void Main(string[] args)
        {
            while (true)

            {
                Console.WriteLine("\n-------------- Employee Management System ---------------");
                Console.WriteLine("1. Add Employee");
                Console.WriteLine("2. Delete Employee");
                Console.WriteLine("3. Exit");
    
                Console.Write("Please Enter your choice : ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        Console.ReadKey();
                        break;

                    case 2:
                        SaveEmployeesToJson();
                        Console.ReadKey();
                        break;

                    case 3:
                        Environment.Exit(0);// exit
                        break;

                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
            }
        }

        static List<Employee> employees = new List<Employee>();
        static int EmployeeIDCounter = 1;
        static void AddEmployee()
        {
            try
            {
                Employee e1 = new Employee();

                e1.EmployeeID = EmployeeIDCounter++;
                Console.WriteLine("ID : " + e1.EmployeeID);

                Console.Write("Enter Name : ");
                e1.Name = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(e1.Name))
                {
                    Console.WriteLine("Your Name is Required!");
                    Console.ReadKey();
                    return;
                }


                Console.Write("Enter DOB : ");
                var DOB = Convert.ToDateTime(Console.ReadLine());
                e1.DOB = DOB;


                Console.Write("Please enter your gender (M/F): ");
                string genderString = Console.ReadLine().ToUpper();
                Gender gender;

                if (!Enum.TryParse<Gender>(genderString, out gender))
                {
                    Console.WriteLine("Invalid gender input. Please try again.");
                    return;
                }

                Console.Write("Designation : ");
                e1.Designation = Convert.ToString(Console.ReadLine().Trim());

                Console.Write("Enter City : ");
                e1.City = Console.ReadLine().Trim();

                Console.Write("Enter State : ");
                e1.State = Console.ReadLine().Trim();

                Console.Write("Enter Post Code : ");
                e1.Postcode = Convert.ToInt64(Console.ReadLine().Trim());

                Console.Write("Phone Number (Required, only int, Range Max 10 numbers): ");
                long phoneNumber;
                if (!long.TryParse(Console.ReadLine().Trim(), out phoneNumber) || !phoneNumber.IsValidPhoneNumber())
                {
                    Console.WriteLine("Invalid Phone Number!");
                    Console.ReadKey();

                    return;
                }
                e1.PhoneNumber = phoneNumber;

                Console.Write("Email Address (Required, Must be Valid): ");
                e1.Email = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(e1.Email) || !e1.Email.IsValidEmail())
                {
                    Console.WriteLine("Invalid Email Address!");
                    Console.ReadKey();
                    return;
                }

                Console.Write("Enter Date of Joining : ");
                var DOJ = Convert.ToDateTime(Console.ReadLine());
                e1.DateOfJoining = DOJ;

                var today = Convert.ToDateTime(DateTime.Today);
                var TotalExperiences = today-DOJ; 
                Console.WriteLine("Total Experience : "+ TotalExperiences);

                Console.Write("Enter Remarks : ");
                e1.Remarks = Console.ReadLine().Trim();

                Console.Write("Department (Required, Should be from Enum, Developer, QA): ");
                string DepartmentStr = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(DepartmentStr) || !Enum.TryParse(DepartmentStr, out Department depart))
                {
                    Console.WriteLine("Designation is Required and Should be from Enum, Developer, QA!");
                    Console.ReadKey();

                    return;
                }
                e1.Department = depart;


                Console.Write("Salary (Required, Min 10,000 & Max 50,000): ");
                double salary;
                if (!double.TryParse(Console.ReadLine().Trim(), out salary) || !salary.IsValidSalary())
                {
                    Console.WriteLine("Invalid Salary!");
                    Console.ReadKey();

                    return;
                }
                e1.MonthlySalary = salary;


                Console.WriteLine("All Fields are Validated..");

                var fileName = @"D:\Krinal C# AJAX Exam\Employee\Files\EmployeeData_TodayDate.json";
                var jsonData = System.IO.File.ReadAllText(fileName);

                //if (jsonData.)
                //{
                    // Read existing json data
                    // De-serialize to object or create new list
                    var employeeList = JsonConvert.DeserializeObject<List<Employee>>(jsonData) ?? new List<Employee>();

                    employeeList.Add(e1);
                //}
         

                //employees.Add(e1);

                SaveEmployeesToJson();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void SaveEmployeesToJson()
        {
        





            string fileName = @"D:\Krinal C# AJAX Exam\Employee\Files\EmployeeData_TodayDate.json";

            string json = JsonConvert.SerializeObject(employees, Formatting.Indented);

            File.AppendAllText(fileName, json);
            Console.WriteLine("Employee data saved to  EmployeeData_TodayDate.json file.");


            //Employee e2 = JsonConvert.DeserializeObject<Employee>(json);
            //Console.WriteLine("--------------- JSON Deserialize ----------------");

            //Console.WriteLine("Reading from the file..");
            //string jsonDataFromFile = File.ReadAllText(fileName);
            //Employee e3 = JsonConvert.DeserializeObject<Employee>(jsonDataFromFile);

            //Console.WriteLine("Name : " + e3.Name);
            //Console.WriteLine("Age : " + e3.Email);
            //Console.WriteLine("City : " + e3.City);


        }
    }
}
