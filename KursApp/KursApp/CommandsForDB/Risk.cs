using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    public class Risk
    {
        public int Id { get; set; }
        public string RiskName { get; set; }
        public string SoursOfRisk { get; set; }
        public string Effects { get; set; }
        public string Description { get; set; }
        public string TypeOfProject { get; set; }
    }
}
