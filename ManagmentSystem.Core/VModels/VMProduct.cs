using System;
using System.Collections.Generic;
using System.Text;

namespace ManagmentSystem.Core.VModels
{
    public class VMProduct
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public bool Status { get; set; }
        public bool IsDiscount { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryAName { get; set; }
        public string CategoryEName { get; set; }
    }

    public class VMProductPost
    {
        public string Id { get; set; }
        public string AName { get; set; }
        public string EName { get; set; }
        public string Description { get; set; }
        public double Quantity { get; set; }
        public bool Status { get; set; }
        public bool IsDiscount { get; set; }
        public string CategoryId { get; set; }
    }
}
