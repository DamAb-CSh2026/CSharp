using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    public interface IEmployee
    {
        double CalculateSalary(Employee employee)
    }

    public class PermanentCalculateSalary:IEmployee
    {
        double CalculateSalary(Employee employee)
        {
            return employee.BaseSalary * 1.2;
        }
    }

    public class ContractCalculateSalary:IEmployee
    {
        double CalculateSalary(Employee employee)
        {
            return employee.BaseSalary * 1.1;
        }
    }

    public class InternCalcualate:IEmployee
    {
        double CalculateSalary(Employee employee)
        {
            return employee.BaseSalary * 0.8;
        }
    }
    public class Employee

    {

        public string Name { get; set; }

        public double BaseSalary { get; set; }

        public string EmployeeType { get; set; } // "Permanent", "Contract", "Intern"

    }
}
