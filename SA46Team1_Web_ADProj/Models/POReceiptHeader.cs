
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
    
public partial class POReceiptHeader
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public POReceiptHeader()
    {

        this.POReceiptDetails = new HashSet<POReceiptDetail>();

    }


    public int ReceiptNo { get; set; }

    public string PONumber { get; set; }

    public string DeliveryOrderNo { get; set; }

    public System.DateTime ReceivedDate { get; set; }

    public string Receiver { get; set; }

    public string Remarks { get; set; }

    public float TotalAmount { get; set; }

    public string TransactionType { get; set; }



    public virtual Employee Employee { get; set; }

    public virtual POHeader POHeader { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<POReceiptDetail> POReceiptDetails { get; set; }

}

}
