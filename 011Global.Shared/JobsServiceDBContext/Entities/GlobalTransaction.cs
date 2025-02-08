using _011Global.Shared.JobsServiceDBContext.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalTransaction
    {
        public int TransactionID {  get; set; }
        public int CustomerID {  get; set; }  
        public double Amount { get; set; }
        public byte TransactionStatusID { get; set; }
        public string PaymentGWTransID { get; set; } = null!;
        public string ResponseCode { get; set; } = null!;
        public string SubErrorDesc1 {  get; set; } = null!;
        public string SubErrorDesc2 { get; set; } = null!;
        public string SubErrorDesc3 { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public int CreditCardID {  get; set; }

        [ForeignKey("CustomerID")]
        public virtual GlobalCustomer GlobalCustomer { get; set; }

        public virtual GlobalTransactionsStatus GlobalTransactionsStatus { get; set; } = null!;
    }
}
