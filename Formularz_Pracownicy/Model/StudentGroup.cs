using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1.Model
{
    public class StudentGroup
    {
        public string Name { get; set; }
        public ObservableCollection<Student> Students { get; private set; }
        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

        public StudentGroup(string name)
        {
            Name = name;
            Students = new ObservableCollection<Student>();
        }
    }
}
