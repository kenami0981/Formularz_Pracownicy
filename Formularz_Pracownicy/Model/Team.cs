using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formularz_Pracownicy.Model
{
    public class Team
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Team(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Team() { }
    }
}
