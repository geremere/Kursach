using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    public class Project
    {
        public Project(string name, string owner, string type)
        {
            Name = name;
            Owner = owner;
            Type = type;
        }

        public Project(int id, string name, string owner, string type)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Type = type;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Type { get; set; }
        public override string ToString() => $"{Name}\nOwner: {Owner}\t {Type}";
    }
}
