using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1.Model
{
    public class StudyFieldCollection : ObservableCollection<StudyField>
    {
        public StudyFieldCollection()
        {
            Add(new StudyField { Name = "Matematyka", Description = "matematyka" });
            Add(new StudyField { Name = "Informatyka", Description = "informatyka" });
            Add(new StudyField { Name = "Fizyka", Description = "fizyka" });
        }
    }
}
