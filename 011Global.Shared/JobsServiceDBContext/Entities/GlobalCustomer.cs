using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Reflection.Metadata;

namespace _011Global.Shared.JobsServiceDBContext.Entities;

public partial class GlobalCustomer
{
    public int CustomerID { get; set; }

    public string CustomerEmail { get; set; } = null!;

    public string CustomerName { get; set; } = null!;

    public string CustomerLastName { get; set; } = null!;

    public int ShippingAddressID { get; set; }

    public int BillingAddressID { get; set; }

    public float MonthlyFee { get; set; }

    public DateTime CreationDate { get; set; }

    public int PreferredCreditCardId {  get; set; }

    public List<GlobalTransaction> GlobalTransactions { get; } = new();

    public List<GlobalCreditCard> GlobalCreditCards { get; } = new();
    public GlobalAddress BillingAddress { get; set; } = new();
    public GlobalAddress ShippingAddress { get; set; } = new();
    

}
