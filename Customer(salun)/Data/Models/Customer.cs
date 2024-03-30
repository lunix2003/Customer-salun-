using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_salun_.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public char Sex { get; set; }
        public DateTime DoB { get; set; }
        public string PoB { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsHidden { get; set; }
    }
}
