using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using static Employee.Program;
using Formatting = Newtonsoft.Json.Formatting;

namespace Employee
{
    public static class DateTimeExtensions
    {
        public static string ToCustomDateString(this DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy");
        }
    }

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
        public static bool IsValidName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            string pattern = @"^[a-zA-Z]+$";

            var regex = new Regex(pattern);
            var match = regex.Match(name);
            return match.Success;
        }

        public static bool IsValidPhoneNumber(this long number)
        {
            return number.ToString().Length >= 10;
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
            public string DateOfJoining { get; set; }
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
                e1. Name = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.Name) || !e1.Name.IsValidName())
                {
                    Console.WriteLine("Invalid Name!");
                    Console.Write("Enter Name Correctly : ");
                    e1.Name = Console.ReadLine().Trim();

                    Console.ReadKey();
                    
                }


                Console.Write("Enter DOB in dd-MMM-yyyy Format: ");
                string dobString = Console.ReadLine().Trim();

                DateTime dob;
                while (!DateTime.TryParseExact(dobString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dob))
                {
                    Console.WriteLine("Invalid date format! Please enter date in the format dd-MMM-yyyy.");
                    Console.Write("Enter DOB in dd-MMM-yyyy Format: ");
                    dobString = Console.ReadLine().Trim();
                }

                e1.DOB = dob;


            


                Console.Write("Please enter your gender (M/F): ");
                string genderString = Console.ReadLine().ToUpper();
                Gender gender;

                while (!Enum.TryParse<Gender>(genderString, out gender))
                {
                    Console.WriteLine("Invalid gender input. Please try again.");
                    return;
                }
                e1.Gender = gender;

                Console.Write("Designation : ");
                e1.Designation = Convert.ToString(Console.ReadLine().Trim());
                while (string.IsNullOrEmpty(e1.Designation) || !e1.Designation.IsValidName())
                {
                    Console.WriteLine("Invalid Designation!");
                    Console.ReadKey();
                    return;
                }
                

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



                Console.Write("Enter your date of joining in dd-mmm-yyyy format: ");
                string dateStr = Console.ReadLine();
                e1.DateOfJoining = dateStr;

                // Convert the date string to a DateTime object
                DateTime dateObj;
                if (DateTime.TryParseExact(dateStr, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.None, out dateObj))
                {
                    // Calculate the total experience in years
                    TimeSpan experience = DateTime.Now - dateObj;
                    double totalExperience = experience.TotalDays / 365;

                    var total = totalExperience.ToString("0.00") + " years";
                    // Print the total experience
                    Console.WriteLine("Total experience: " + total);
                    e1.TotalExperience = total;

                }
                else
                {
                    Console.WriteLine("Error: Invalid date format. Please enter the date in dd-mmm-yyyy format.");
                }









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


                double salary;
                bool validSalary = false;

                while (!validSalary)
                {
                    Console.Write("Salary (Required, Min 10,000 & Max 50,000): ");
                    if (!double.TryParse(Console.ReadLine().Trim(), out salary) || !salary.IsValidSalary())
                    {
                        Console.WriteLine("Invalid Salary!");
                    }
                    else
                    {
                        validSalary = true;
                        e1.MonthlySalary = salary;
                    }
                }



                Console.WriteLine("All Fields are Validated..");

                var fileName = @"F:\C# AJAX Exam\Employee\Files\EmployeeData_TodayDate.json";

                if (File.Exists(fileName))
                {
                    var jsonData = File.ReadAllText(fileName);

                    var employeeList = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
                    employeeList.AddRange(employees);

                    var updatedJsonData = JsonConvert.SerializeObject(employeeList, Formatting.Indented);
                    File.WriteAllText(fileName, updatedJsonData);

                    Console.WriteLine("Employee data appended to EmployeeData_TodayDate.json file.");
                }
                else
                {
                    employees.Add(e1);
                    var json = JsonConvert.SerializeObject(employees, Formatting.Indented);

                    File.WriteAllText(fileName, json);

                    Console.WriteLine("Employee data saved to EmployeeData_TodayDate.json file.");
                }

                //SaveEmployeesToJson();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //static void SaveEmployeesToJson()
        //{


       

        //}
    }
}
