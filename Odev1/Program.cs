using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odev1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Employee Management System. Please press available buttons..");
           
            List<Employee> employees = new List<Employee>();
            employees.Add(new Developer(1, "Ali", 25, 5000, 5));
            employees.Add(new Developer(2, "Veli", 30, 6000, 6));
            employees.Add(new Remote_Software_Developer(4, "Mehmet", 40, 8000, 8, 100));
            employees.Add(new Analyst(3, "Ayse", 35, 7000, 7));
            employees.Add(new Project_Manager(5, "Fatma", 45, 9000, 9));
           

            while (true)
            {
                Console.WriteLine("\n1 - List Employees");
                Console.WriteLine("2 - List in descending order by salary");
                Console.WriteLine("3 - Find and show a specific employee by ID");
                Console.WriteLine("4 - Increase the salary of a specific employee");
                Console.WriteLine("5 - Increase the salaries of all workers");
                Console.WriteLine("6 - Show the company's annual salary budget");
                Console.WriteLine("0 - Exit");
                Console.Write("Choose an option: ");

                string choose = Console.ReadLine();

                if (choose == "0")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }
                else if (choose == "1")
                {
                    Console.WriteLine("\nAll Employees:");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine(employee.ToString());
                    }
                }
                else if (choose == "2")
                {
                    Console.WriteLine("\nEmployees sorted by salary (highest to lowest):");
                    foreach (var employee in employees.OrderByDescending(e => e.salary))
                    {
                        Console.WriteLine(employee.ToString());
                    }
                }
                else if (choose == "3")
                {
                    Console.Write("\nEnter the ID of the employee you want to find: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    var employee = employees.FirstOrDefault(e => e.id == id);
                    if (employee != null)
                    {
                        Console.WriteLine(employee.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                }
                else if (choose == "4")
                {
                    Console.Write("\nEnter the ID of the employee whose salary you want to increase: ");
                    int id = Convert.ToInt32(Console.ReadLine());

                    var employee = employees.FirstOrDefault(e => e.id == id);
                    if (employee != null)
                    {
                        Console.Write("Enter the amount of salary increase: ");
                        int salaryIncrease = Convert.ToInt32(Console.ReadLine());

                        employee.IncreaseSalary(salaryIncrease);
                        Console.WriteLine("Salary updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Employee not found.");
                    }
                }
                else if (choose == "5")
                {
                    Console.Write("\nEnter the amount to increase the salary of all employees: ");
                    int amount = Convert.ToInt32(Console.ReadLine());

                    foreach (var employee in employees)
                    {
                        employee.IncreaseSalary(amount);
                    }

                    Console.WriteLine("Salaries updated for all employees.");
                }
                else if (choose == "6")
                {
                    const int annualBudget = 500000;

                    int totalSalaries = employees.Sum(e => e.salary * 12); 
                    int difference = annualBudget - totalSalaries;

                    Console.WriteLine($"\nCompany Annual Budget: {annualBudget}₺");
                    Console.WriteLine($"Total Annual Salary Expenses: {totalSalaries}₺");
                    if (difference >= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Remaining Budget: {difference}₺ (Positive)");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Budget Deficit: {difference}₺ (Negative)");
                    }
                    Console.ResetColor(); 
                }
                else
                {
                    Console.WriteLine("Invalid option, please try again.");
                }
            }
        }
    }
}
        
