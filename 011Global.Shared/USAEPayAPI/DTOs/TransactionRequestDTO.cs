using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _011Global.Shared.USAEPayAPI.DTOs
{
    public class TransactionRequestDTO
    {
        public string command { get; set; } = "sale";
        public double amount {  get; set; }
        public CreditCardDTO creditcard { get; set; }
        public string software {  get; set; }
    }
    public class CreditCardDTO
    {
        public string cardholdername {  get; set; }
        public string number {  get; set; }
        public string expiration {  get; set; }
        public string avs_street { get; set; }
        public string avs_postalcode {  get; set; }
    }
}
