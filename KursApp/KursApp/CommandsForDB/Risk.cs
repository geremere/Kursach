using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace KursApp
{
    public class Risk
    {
        public Risk(int id,string riskName, string soursOfRisk, string effects, string description, string typeOfProject)
        {
            Id = id;
            RiskName = riskName;
            SoursOfRisk = soursOfRisk;
            Effects = effects;
            Description = description;
            TypeOfProject = typeOfProject;
        }

        public Risk(double probability, double effect, int status)
        {
            Probability = probability;
            Influence = effect;
            Status = status;
        }

        public Risk(int id, string riskName, string soursOfRisk, string effects, string description,
            string typeOfProject, double probability, double influence,int status) 
        {
            Id = id;
            RiskName = riskName;
            SoursOfRisk = soursOfRisk;
            Effects = effects;
            Description = description;
            TypeOfProject = typeOfProject;
            Probability = probability;
            Influence = influence;
            Status = status;
        }
        public Risk(int id, string riskName, string soursOfRisk, string effects, string description,
            string typeOfProject, double probability, double influence, int status,int ownerId, string ownerLogin)
        {
            Id = id;
            RiskName = riskName;
            SoursOfRisk = soursOfRisk;
            Effects = effects;
            Description = description;
            TypeOfProject = typeOfProject;
            Probability = probability;
            Influence = influence;
            Status = status;
            OwnerId = ownerId;
            OwnerLogin = ownerLogin;
        }


        int status;
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }
        public int Id { get; set; }
        public string RiskName { get; set; }
        public string SoursOfRisk { get; set; }
        public string Effects { get; set; }
        public string Description { get; set; }
        public string TypeOfProject { get; set; }
        double probability;
        public Point point;
        public string OwnerLogin { get; set; }
        int ownerid;
        public double rank;
        public double Rank
        {
            get => Influence * Probability;
        }
        public int OwnerId
        {
            get
            {
                return ownerid;
            }
            set
            {
                //if (value <= 0) throw new ArgumentException("id > 0");
                ownerid = value;
            }
        }
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
        double influence;
        public double Influence
        {
            get
            {
                return influence;
            }
            set
            {
                if (value > 1 || value < 0) throw new ArgumentException("Значение должно лежать в промежутке (0;1]");
                influence = value;
            }
        }
        public override string ToString() => $"Name: {RiskName}\nType: {TypeOfProject}";
    }
}
