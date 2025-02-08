using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalTransactionsStatus
    {
        public byte TransactionStatusID {  get; set; }
        public string TransactionStatus { get; set; } = null!;

        [ForeignKey("TransactionStatusId")]
        public virtual GlobalTransaction GlobalTransaction { get; set; }
    }

    public enum TransactionStatusCode
    {
        A = 1,
        P = 2,
        D = 3,
        E = 4,
        V = 5,
    }
}
