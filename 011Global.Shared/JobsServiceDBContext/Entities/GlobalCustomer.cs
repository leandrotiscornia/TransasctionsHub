using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Reflection.Metadata;

namespace _011Global.Shared.JobsServiceDBContext.Entities;

public partial class GlobalCustomer
{
    public int CustomerID { get; set; }
    public int ServicePackageID {  get; set; }

    public string CustomerEmail { get; set; } = null!;

    public string CustomerFirstName { get; set; } = null!;

    public string CustomerLastName { get; set; } = null!;

    public int ShippingAddressID { get; set; }

    public int BillingAddressID { get; set; }

    public DateTime CreationDate { get; set; }

    public int? PreferredCreditCardId {  get; set; }
    public bool Subscribed { get; set; } = true;

    
    public virtual List<GlobalTransaction> GlobalTransactions { get; } = new();
    public virtual List<GlobalCreditCard> GlobalCreditCards { get; } = new();
    [ForeignKey("BillingAddressID")]
    public virtual GlobalAddress BillingAddress { get; set; } = new();
    [ForeignKey("ShippingAddressID")]
    public virtual GlobalAddress ShippingAddress { get; set; } = new();
    [ForeignKey("ServicePackageID")]
    public virtual GlobalPackage GlobalPackage { get; set; } = new();
}
