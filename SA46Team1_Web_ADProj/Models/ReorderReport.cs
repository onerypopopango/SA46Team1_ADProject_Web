//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SA46Team1_Web_ADProj.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ReorderReport
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string UoM { get; set; }
        public int Quantity { get; set; }
        public int ReOrderLevel { get; set; }
        public int ReOrderQuantity { get; set; }
        public int POBackOrdered { get; set; }
        public int SRBackOrdered { get; set; }
        public int SuggestedQty { get; set; }
    }
}