using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalTransactionsStatus
    {
        public int TransactionStatusID {  get; set; }
        public string TransactionStatus { get; set; } = null!;
    }
}
