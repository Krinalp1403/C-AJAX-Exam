using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using Formatting = Newtonsoft.Json.Formatting;
using MyValidation;


namespace Employee
{

    class Program
    {


        public class Employee
        {
            public int EmployeeId { get; set; }
            public string Name { get; set; }
            public DateTime DateOfBirth { get; set; }
            public Gender Gender { get; set; }
            public string Designation { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Postcode { get; set; }
            public string PhoneNumber { get; set; }
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

                try
                {
                    Console.Write("Please Enter your choice : ");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddEmployee();
                            Console.ReadKey();
                            break;

                        case 2:
                            DeleteEmployee();
                            Console.ReadKey();
                            break;

                        case 3:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice! Please try again.");
                            break;

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Press any key to display Menu..!!");

                    Console.ReadLine();
                }
            }

        }


        static List<Employee> employees = new List<Employee>();
        static void AddEmployee()
        {

            try
            {
                string filePath = ConfigurationManager.AppSettings["EmployeePath"];

                List<int> employeeIDs = new List<int>();
                int newID;

                FileInfo fileInfo = new FileInfo(filePath);

                Employee e1 = new Employee();

                if (fileInfo.Length != 0)
                {   
                    if (File.Exists(filePath))
                    {

                        string json = File.ReadAllText(filePath);
                        JArray jArray = JArray.Parse(json);

                        foreach (JObject item in jArray)
                        {
                            int id = (int)item["EmployeeId"];
                            employeeIDs.Add(id);
                        }

                        newID = employeeIDs.Max() + 1;
                        e1.EmployeeId = newID;

                    }
                }
                else
                {
                    newID = 1;
                    e1.EmployeeId = newID;

                }

                Console.WriteLine("ID : " + e1.EmployeeId);

                Console.Write("Enter Name : ");
                e1.Name = Console.ReadLine().Trim();

                while (string.IsNullOrEmpty(e1.Name) || !e1.Name.IsValidName())
                {
                    Console.WriteLine("Invalid Name!");
                    Console.Write("Enter Name : ");
                    e1.Name = Console.ReadLine().Trim();
                }

                Console.Write("Enter DateOfBirth in dd-MMM-yyyy Format: ");
                string DateOfBirthString = Console.ReadLine().Trim();
                DateTime DateOfBirth;
                while (!DateTime.TryParseExact(DateOfBirthString, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOfBirth))
                {
                    Console.WriteLine("Invalid date format..!! Please enter date in the format dd-MMM-yyyy Ex: 14-Mar-2000 ..!!");
                    Console.Write("Enter DateOfBirth in dd-MMM-yyyy Format: ");
                    DateOfBirthString = Console.ReadLine().Trim();
                }
                
                e1.DateOfBirth = DateOfBirth;


                Console.Write("Please enter gender (1. Male ,2. Female): ");
                string genderInput = Console.ReadLine();

                Gender gender;
                while (!Enum.TryParse(genderInput, out gender))
                {
                    Console.Write("Invalid gender input. Please enter your gender (1. Male ,2. Female):");
                    genderInput = Console.ReadLine().ToUpper();

                }
                e1.Gender = gender;


                Console.Write("Designation : ");
                e1.Designation = Convert.ToString(Console.ReadLine().Trim());
                while (string.IsNullOrEmpty(e1.Designation) || !e1.Designation.IsValidName())
                {
                    Console.WriteLine("Invalid Designation!");
                    Console.Write("Designation : ");
                    e1.Designation = Convert.ToString(Console.ReadLine().Trim());

                }


                Console.Write("Enter City : ");
                e1.City = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.City) || !e1.City.IsValidName())
                {
                    Console.WriteLine("Invalid City!");
                    Console.Write("Enter City : ");
                    e1.City = Console.ReadLine().Trim();
                }

                Console.Write("Enter State : ");
                e1.State = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.State) || !e1.State.IsValidName())
                {
                    Console.WriteLine("Invalid State!");
                    Console.Write("Enter State : ");
                    e1.State = Console.ReadLine().Trim();
                }


                Console.Write("Enter Post Code : ");
                e1.Postcode = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.Postcode) || !e1.Postcode.IsValidPostCode())
                {
                    Console.WriteLine("Invalid PostCode!");
                    Console.Write("Enter Post Code : ");
                    e1.Postcode = Console.ReadLine().Trim();
                }


                Console.Write("Phone Number (Required, only int, Range Max 10 numbers): ");
                e1.PhoneNumber = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.PhoneNumber) || !e1.PhoneNumber.IsValidPhoneNumber() || e1.PhoneNumber.Any(char.IsLetter))
                {
                    Console.WriteLine("Invalid Phone Number!");
                    Console.Write("Phone Number (Required, only int, Range Max 10 numbers): ");
                    e1.PhoneNumber = Console.ReadLine().Trim();

                }

                Console.Write("Email Address (Required, Must be Valid): ");
                e1.Email = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.Email) || !e1.Email.IsValidEmail())
                {
                    Console.WriteLine("Invalid Email Address!");
                    Console.Write("Email Address (Required, Must be Valid): ");
                    e1.Email = Console.ReadLine().Trim();

                }

                bool isValidDate = false;
                while (!isValidDate)
                {
                    Console.Write("Enter your date of joining in dd-mmm-yyyy format: ");
                    string dateStr = Console.ReadLine();
                    DateTime dateObj;
                    if (DateTime.TryParseExact(dateStr, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.None, out dateObj))
                    {
                        if (dateObj > e1.DateOfBirth)
                        {
                            DateTime now = DateTime.Now;
                            TimeSpan experience = now - dateObj;

                            int totalMonths = (now.Month - dateObj.Month) + 12 * (now.Year - dateObj.Year);
                            int years = totalMonths / 12;
                            int months = totalMonths % 12;

                          
                            if(years <= 0 )
                            {
                                if (months <= 0)
                                {
                                    Console.WriteLine("No experience..!!");
                                    e1.TotalExperience = "No Experience..!!";

                                    e1.DateOfJoining  = dateStr;

                                    isValidDate = true;
                                }
                                else
                                {
                                    string total = months + " months";
                                    Console.WriteLine("Total experience: " + total);
                                    e1.TotalExperience = total;
                                    e1.DateOfJoining  = dateStr;
                                    isValidDate = true;
                                }
                              
                            }
                            else
                            {
                                string total = years + " years ," + months + " months";
                                Console.WriteLine("Total experience: " + total);
                                e1.TotalExperience = total;
                                e1.DateOfJoining  = dateStr;
                                isValidDate = true;
                            }
                         
                        }
                        else
                        {
                            Console.WriteLine("Error: Date of joining must be after date of birth.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Invalid date format. Please enter the date in dd-mmm-yyyy format.");
                    }
                }

                Console.Write("Enter Remarks : ");
                e1.Remarks = Console.ReadLine().Trim();
                while (string.IsNullOrEmpty(e1.Remarks))
                {
                    Console.WriteLine("Invalid Remarks!");
                    Console.Write("Enter Remarks : ");
                    e1.Remarks = Console.ReadLine().Trim();
                }


                int departmentNumber = 0;
                while (departmentNumber < 1 || departmentNumber > 6)
                {
                    Console.Write("Select department (1: Sales, 2: Marketing, 3: Development, 4: QA, 5: HR, 6: SEO) : ");
                    int.TryParse(Console.ReadLine(), out departmentNumber);
                }
                e1.Department = (Department)(departmentNumber);
         
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

                List<Employee> employees;
                if (fileInfo.Length != 0)
                {
                    if (File.Exists(filePath))
                    {
                        string jsonData = File.ReadAllText(filePath);
                        employees = JsonConvert.DeserializeObject<List<Employee>>(jsonData);
                    }
                    else
                    {
                        employees = new List<Employee>();
                    }

                    employees.Add(e1);

                    List<Employee> sortedEmployees = employees.OrderByDescending(e => e.MonthlySalary).ToList();

                    string updatedJsonData = JsonConvert.SerializeObject(sortedEmployees, Formatting.Indented);
                    File.WriteAllText(filePath, updatedJsonData);

                    Console.WriteLine("Employee data sorted by salary and saved to EmployeeData_07_04_2023.json file.");

                }
                else
                {
                    employees = new List<Employee>();

                    employees.Add(e1);
                    string jsondata = JsonConvert.SerializeObject(employees, Formatting.Indented);

                    File.WriteAllText(filePath, jsondata);

                    Console.WriteLine("Employee data saved to EmployeeData_07_04_2023.json file.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static void DeleteEmployee()
        {
            string filePath = ConfigurationManager.AppSettings["EmployeePath"];

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(json);

                Console.WriteLine("---------------------------------------------");

                Console.WriteLine("Displaying all Employees :");
                foreach (Employee employee in employees)
                {
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine($"ID: {employee.EmployeeId}");
                    Console.WriteLine($"Name: {employee.Name}");
                    Console.WriteLine($"DateOfBirth: {employee.DateOfBirth}");
                    Console.WriteLine($"Gender: {employee.Gender}");
                    Console.WriteLine($"Designation: {employee.Designation}");
                    Console.WriteLine($"City: {employee.City}");
                    Console.WriteLine($"State: {employee.State}");
                    Console.WriteLine($"Postcode: {employee.Postcode}");
                    Console.WriteLine($"Phone Number: {employee.PhoneNumber}");
                    Console.WriteLine($"Email: {employee.Email}");
                    Console.WriteLine($"Date of Joining: {employee.DateOfJoining }");
                    Console.WriteLine($"Total Experience: {employee.TotalExperience}");
                    Console.WriteLine($"Remarks: {employee.Remarks}");
                    Console.WriteLine($"Department: {employee.Department}");
                    Console.WriteLine($"Monthly Salary: {employee.MonthlySalary}");
                    Console.WriteLine("---------------------------------------------");

                }

                Console.Write("Enter ID to Delete  : ");
                int idToDelete = Convert.ToInt32(Console.ReadLine());
                    
                Employee employeeToDelete = employees.FirstOrDefault(e => e.EmployeeId == idToDelete);

                if (employeeToDelete != null)
                {
                    employees.Remove(employeeToDelete);

                    string updatedJson = JsonConvert.SerializeObject(employees, Formatting.Indented);

                    File.WriteAllText(filePath, updatedJson);

                    Console.WriteLine($"Employee with ID {idToDelete} deleted successfully!");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {idToDelete} not found.");
                }
            }
            else
            {
                Console.WriteLine("Sorry..!! No Employee Record found to be deleted");

            }
        }
    }
}