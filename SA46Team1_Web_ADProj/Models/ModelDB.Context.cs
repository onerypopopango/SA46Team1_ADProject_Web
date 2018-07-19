﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SSISdbEntities : DbContext
    {
        public SSISdbEntities()
            : base("name=SSISdbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApprovalDelegation> ApprovalDelegations { get; set; }
        public virtual DbSet<Bin> Bins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CollectionPoint> CollectionPoints { get; set; }
        public virtual DbSet<CollectionPointTran> CollectionPointTrans { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<DepartmentDetail> DepartmentDetails { get; set; }
        public virtual DbSet<DisbursementDetail> DisbursementDetails { get; set; }
        public virtual DbSet<DisbursementHeader> DisbursementHeaders { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemTransaction> ItemTransactions { get; set; }
        public virtual DbSet<PODetail> PODetails { get; set; }
        public virtual DbSet<POHeader> POHeaders { get; set; }
        public virtual DbSet<POReceiptDetail> POReceiptDetails { get; set; }
        public virtual DbSet<POReceiptHeader> POReceiptHeaders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<StaffRequisitionDetail> StaffRequisitionDetails { get; set; }
        public virtual DbSet<StaffRequisitionHeader> StaffRequisitionHeaders { get; set; }
        public virtual DbSet<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public virtual DbSet<StockAdjustmentHeader> StockAdjustmentHeaders { get; set; }
        public virtual DbSet<StockCount> StockCounts { get; set; }
        public virtual DbSet<StockRetrievalDetail> StockRetrievalDetails { get; set; }
        public virtual DbSet<StockRetrievalHeader> StockRetrievalHeaders { get; set; }
        public virtual DbSet<StockTakeDetail> StockTakeDetails { get; set; }
        public virtual DbSet<StockTakeHeader> StockTakeHeaders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<SupplierPriceList> SupplierPriceLists { get; set; }
        public virtual DbSet<StockAdjustmentOverview> StockAdjustmentOverviews { get; set; }
        public virtual DbSet<GoodsReceivedList> GoodsReceivedLists { get; set; }
        public virtual DbSet<InventoryOverview> InventoryOverviews { get; set; }
        public virtual DbSet<PendingApproval> PendingApprovals { get; set; }
        public virtual DbSet<RequisitionList> RequisitionLists { get; set; }
        public virtual DbSet<RequisitionListDetail> RequisitionListDetails { get; set; }
    }
}
