using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    public class Vertexcs
    {
        public Vertexcs(int parentId, string description, double cost, double probability, double x, double y)
        {
            ParentId = parentId;
            Description = description;
            Cost = cost;
            Probability = probability;
            X = x;
            Y = y;
        }
        public Vertexcs(int parentId, string description, double x, double y)
        {
            ParentId = parentId;
            Description = description;
            X = x;
            Y = y;
        }

        public Vertexcs(int id, int parentId, string description, double x, double y)
        {
            Id = id;
            ParentId = parentId;
            Description = description;
            X = x;
            Y = y;
        }

        public Vertexcs(int id, int parentId, string description, double cost, double probability, double x, double y)
        {
            Id = id;
            ParentId = parentId;
            Description = description;
            Cost = cost;
            Probability = probability;
            X = x;
            Y = y;
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Probability { get; set; }
        double x;
        public double X
        {
            get => x;
            set
            {
                x = Double.Parse($"{value:f3}");
            }
        }
        double val;
        public double Value
        {
            get => val;
            set
            {
                val = double.Parse($"{value:f3}");
            }
        }

        public double Y { get; set; }


    }
}
