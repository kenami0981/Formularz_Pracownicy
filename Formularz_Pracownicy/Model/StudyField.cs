using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1.Model
{
    public class StudyField
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public StudyField(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public StudyField() { }
    }
}
