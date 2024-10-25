using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Invoice
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public DateTime AddedOn { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalCostAfterDiscount { get; set; }
        public decimal TotalCostWithoutDiscount { get; set; }
        public decimal SpecialDiscount { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
        public string ProcessType { get; set; }
        public DateTime LastAccessed { get; set; }
        public List<Process> Processes { get; set; }
    }
}
