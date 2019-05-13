using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    public class Vertexcs
    {
        public Vertexcs( int parentId, string description, double cost, double probability, double x, double y, int row)
        {
            ParentId = parentId;
            Description = description;
            Cost = cost;
            Probability = probability;
            X = x;
            Y = y;
            Row = row;
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public double Probability { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public int Row { get; set; }


    }
}
