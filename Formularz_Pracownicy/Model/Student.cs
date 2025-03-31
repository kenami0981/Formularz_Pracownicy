using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1.Model
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; private set; }
        public string Id { get; private set; }
        public StudyField StudyField { get; set; }

        public Student(string firstName, string lastName, DateTime? birthDate,
            string id, StudyField studyField)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Id = id;
            StudyField = studyField;
        }

        public int GetAge()
        {
            //return DateTime.Now.Year - BirthDate.Year;
            return 20;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Id}), wiek: {GetAge()}";
        }
    }
}
