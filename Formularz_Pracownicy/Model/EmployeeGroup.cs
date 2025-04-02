using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Formularz_Pracownicy.Model
{
    public class EmployeeGroup
    {
        public string Name { get; set; }
        public ObservableCollection<Employee> Employee { get; private set; }
        public void AddEmployee (Employee employee)
        {
            Employee.Add(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            Employee.Remove(employee);
        }
        public void UpdateEmployee(Employee employee, string firstName, string lastName, DateTime? birthDate, string salary, Team team, string contract)
        {
            if (employee == null) return; // Sprawdzenie czy obiekt istnieje

            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.BirthDate = birthDate;
            employee.Salary = salary;
            employee.Team1 = team;
            employee.Contract = contract;
        }
        public EmployeeGroup(string name)
        {
            Name = name;
            Employee = new ObservableCollection<Employee>();
        }
    }
}
