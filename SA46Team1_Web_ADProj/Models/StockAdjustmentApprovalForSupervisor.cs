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
    
    public partial class StockAdjustmentApprovalForSupervisor
    {
        public string RequestId { get; set; }
        public System.DateTime DateRequested { get; set; }
        public string Requestor { get; set; }
        public string Approver { get; set; }
        public string ApproverID { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int ItemQuantity { get; set; }
        public float Amount { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }
}
