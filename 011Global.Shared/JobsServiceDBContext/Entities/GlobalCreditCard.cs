using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalCreditCard
    {
        public int CreditCardID {  get; set; }

        public int CustomerID {  get; set; }

        public string CreditCardNumber { get; set; } = null!;

        public string LastFourNumbers { get; set; } = null!;

        public string CardHolder { get; set; } = null!;

        public string SecurityCode { get; set; } = null!;

        public string ExpirationMonth { get; set; } = null!;

        public string ExpirationYear { get; set;} = null!;

        public DateTime CreationDate {  get; set; }
    }
}
