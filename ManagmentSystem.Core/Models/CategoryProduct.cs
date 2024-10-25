using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class CategoryProduct
    {
        public string Id { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime LastAccessed { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
