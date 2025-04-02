using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularz_Pracownicy.Model
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Salary { get; set; }

        public string Contract { get; set; }
        public Team Team1 { get; set; }

        public Employee(string firstName, string lastName, DateTime? birthDate,
            string salary, Team team, string contract)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Salary = salary;
            Team1 = team;
            Contract = contract;
        }

        public int GetAge()
        {
            return DateTime.Now.Year - BirthDate.Value.Year;
            
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} (wiek: {GetAge()}), stanowisko: {Team1.Name}, typ umowy: {Contract}, pensja: {Salary}";
        }
    }
}
