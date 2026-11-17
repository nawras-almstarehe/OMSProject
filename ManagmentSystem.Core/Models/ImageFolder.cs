using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagmentSystem.Core.Models
{
    public class ImageFolder
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ImagePath { get; set; }
        public DateTime LastAccessed { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
