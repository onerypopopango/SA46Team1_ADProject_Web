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
    
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            this.DepartmentDetails = new HashSet<DepartmentDetail>();
            this.DepartmentDetails1 = new HashSet<DepartmentDetail>();
            this.DepartmentDetails2 = new HashSet<DepartmentDetail>();
            this.DisbursementHeaders = new HashSet<DisbursementHeader>();
            this.POHeaders = new HashSet<POHeader>();
            this.POReceiptHeaders = new HashSet<POReceiptHeader>();
            this.StockAdjustmentHeaders = new HashSet<StockAdjustmentHeader>();
            this.StockAdjustmentHeaders1 = new HashSet<StockAdjustmentHeader>();
        }
    
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Designation { get; set; }
        public int ContactNo { get; set; }
        public string DepartmentCode { get; set; }
        public byte Approver { get; set; }
        public string ReportsTo { get; set; }
        public byte Active { get; set; }
        public string Password { get; set; }
    
        public virtual Department Department { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentDetail> DepartmentDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentDetail> DepartmentDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentDetail> DepartmentDetails2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DisbursementHeader> DisbursementHeaders { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POHeader> POHeaders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POReceiptHeader> POReceiptHeaders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockAdjustmentHeader> StockAdjustmentHeaders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StockAdjustmentHeader> StockAdjustmentHeaders1 { get; set; }
    }
}