using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Process
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString(); public string Code { get; set; }
        public double Quantity { get; set; }
        public DateTime AddedOn { get; set; }
        public decimal Price { get; set; }
        public decimal PriceItem { get; set; }
        public decimal Discount { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public string ProcessType { get; set; }
        public DateTime LastAccessed { get; set; }
        public string InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}
