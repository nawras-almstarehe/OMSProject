using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public bool Status { get; set; }
        public bool IsDiscount { get; set; }
        public DateTime LastAccessed { get; set; }
        public ICollection<Category> Categories { get; set; }
        public List<CategoryProduct> CategoriesProductes { get; set; }
        public List<Process> Processes { get; set; }
        public List<ImageFolder> ImageFolders { get; set; }

    }
}
