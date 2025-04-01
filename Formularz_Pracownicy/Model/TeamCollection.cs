using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularz_Pracownicy.Model
{
    public class TeamCollection : ObservableCollection<Team>
    {
        public TeamCollection()
        {
            Add(new Team { Name = "Programista", Description = "Programista_Opis" });
            Add(new Team { Name = "Projektant", Description = "Projektant_Opis" });
            Add(new Team { Name = "Tester", Description = "Tester_Opis" });
        }
    }
}
