using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    public class Risk
    {
        public Risk(int id, string riskName, string soursOfRisk, string effects, string description, string typeOfProject)
        {
            Id = id;
            RiskName = riskName;
            SoursOfRisk = soursOfRisk;
            Effects = effects;
            Description = description;
            TypeOfProject = typeOfProject;
        }

        public int Id { get; set; }
        public string RiskName { get; set; }
        public string SoursOfRisk { get; set; }
        public string Effects { get; set; }
        public string Description { get; set; }
        public string TypeOfProject { get; set; }
        public override string ToString() => $"Name: {RiskName}\nType: {TypeOfProject}";
    }
}
