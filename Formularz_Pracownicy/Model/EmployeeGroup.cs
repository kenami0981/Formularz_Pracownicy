using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularz_Pracownicy.Model
{
    public class EmployeeGroup
    {
        public string Name { get; set; }
        public ObservableCollection<Employee> Employee { get; private set; }
        public void AddStudent(Employee student)
        {
            Employee.Add(student);
        }

        public void RemoveStudent(Employee student)
        {
            Employee.Remove(student);
        }

        public EmployeeGroup(string name)
        {
            Name = name;
            Employee = new ObservableCollection<Employee>();
        }
    }
}
