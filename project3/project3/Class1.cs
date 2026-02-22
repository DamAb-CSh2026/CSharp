using System;
using System.Collections.Generic;

namespace OCP_Example
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public double BaseSalary { get; set; }

        public abstract double CalculateSalary();
    }


    public class PermanentEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 1.2;
        }
    }

    public class ContractEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 1.1;
        }
    }

    public class InternEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 0.8;
        }
    }

    public class FreelancerEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 1.5;
        }
    }

    public class ManagerEmployee : Employee
    {
        public override double CalculateSalary()
        {
            return BaseSalary * 2.0;
        }
    }

    public class SalaryCalculator
    {
        public void PrintSalary(Employee employee)
        {
            double salary = employee.CalculateSalary();
            Console.WriteLine($"{employee.Name} ({employee.GetType().Name}): {salary:C}");
        }

        public void PrintAllSalaries(List<Employee> employees)
        {
            Console.WriteLine("\n=== РАСЧЕТ ЗАРПЛАТЫ ===\n");
            foreach (var emp in employees)
            {
                PrintSalary(emp);
            }
        }
    }

    public static class EmployeeFactory
    {
        public static Employee CreateEmployee(string type, string name, double baseSalary)
        {
            switch (type.ToLower())
            {
                case "permanent":
                    return new PermanentEmployee { Name = name, BaseSalary = baseSalary };
                case "contract":
                    return new ContractEmployee { Name = name, BaseSalary = baseSalary };
                case "intern":
                    return new InternEmployee { Name = name, BaseSalary = baseSalary };
                case "freelancer":
                    return new FreelancerEmployee { Name = name, BaseSalary = baseSalary };
                case "manager":
                    return new ManagerEmployee { Name = name, BaseSalary = baseSalary };
                default:
                    throw new ArgumentException($"Неизвестный тип сотрудника: {type}");
            }
        }
    }
    class Program
    {
        static void Main()
        {
            var employees = new List<Employee>
            {
                new PermanentEmployee { Name = "Иван Петров", BaseSalary = 50000 },
                new ContractEmployee { Name = "Елена Смирнова", BaseSalary = 45000 },
                new InternEmployee { Name = "Алексей Иванов", BaseSalary = 30000 },
                new FreelancerEmployee { Name = "Мария Сидорова", BaseSalary = 40000 },
                new ManagerEmployee { Name = "Дмитрий Козлов", BaseSalary = 80000 }
            };

            Console.WriteLine("=== СОЗДАНИЕ СОТРУДНИКОВ ЧЕРЕЗ ФАБРИКУ ===\n");

            var newEmployees = new List<Employee>
            {
                EmployeeFactory.CreateEmployee("permanent", "Ольга Новикова", 55000),
                EmployeeFactory.CreateEmployee("freelancer", "Павел Морозов", 35000),
                EmployeeFactory.CreateEmployee("manager", "Светлана Волкова", 90000)
            };

            var calculator = new SalaryCalculator();

            calculator.PrintAllSalaries(employees);
            calculator.PrintAllSalaries(newEmployees);

            Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОГО ТИПА БЕЗ ИЗМЕНЕНИЯ СУЩЕСТВУЮЩЕГО КОДА ===\n");
            Console.WriteLine("Чтобы добавить новый тип сотрудника, нужно всего лишь:");
            Console.WriteLine("1. Создать новый класс, унаследованный от Employee");
            Console.WriteLine("2. Переопределить метод CalculateSalary()");
            Console.WriteLine("3. Все! Остальной код не меняется!\n");

            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}