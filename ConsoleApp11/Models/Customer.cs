using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11.Models
{
    internal class Customer : BasePerson
    {
        public DateTime LastPurchaseDate { get; set; }
        public int TotalVisits { get; set; }
    }
}
