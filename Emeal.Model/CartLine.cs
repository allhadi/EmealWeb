using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emeal.Model
{
    public class CartLine
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public double Price { set; get; }
        public double Total { get { return Quantity * Price; } }

    }
}