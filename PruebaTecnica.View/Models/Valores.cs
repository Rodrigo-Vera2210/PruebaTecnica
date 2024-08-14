using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.View.Models
{
    public class Valores
    {
        public List<decimal> Avg_Data { get; set; } = new List<decimal>();
        public List<decimal> Min_Data { get; set; } = new List<decimal>();
        public List<decimal> Max_Data { get; set; } = new List<decimal>();
    }
}
