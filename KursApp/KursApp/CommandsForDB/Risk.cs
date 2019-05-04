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

        public Risk(double probability, double effect)
        {
            Probability = probability;
            Effect = effect;
        }

        public int Id { get; set; }
        public string RiskName { get; set; }
        public string SoursOfRisk { get; set; }
        public string Effects { get; set; }
        public string Description { get; set; }
        public string TypeOfProject { get; set; }
        double probability;
        public double Probability
        {
            get
            {
                return probability;
            }
            set
            {
                if (value > 1 || value < 0) throw new ArgumentException("Значение должно лежать в промежутке (0;1]");
                probability = value;
            }
        }
        double effect;
        public double Effect
        {
            get
            {
                return effect;
            }
            set
            {
                if (value > 1 || value < 0) throw new ArgumentException("Значение должно лежать в промежутке (0;1]");
                effect = value;
            }
        }
        public override string ToString() => $"Name: {RiskName}\nType: {TypeOfProject}";
    }
}
