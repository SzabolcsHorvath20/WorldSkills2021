using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldSkills20213
{
    public class project_class
    {
        public int id;
        public string name;
        public int responsible;

        public project_class(int id, string name, int responsible)
        {
            this.id = id;
            this.name = name;
            this.responsible = responsible;
        }

        public project_class()
        {

        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public int Responsible { get => responsible; set => responsible = value; }

        public override string ToString()
        {
            return id + "-" + name;     
        }
    }
}
