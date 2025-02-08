using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.USAEPayAPI.DTOs;

namespace _011Global.Shared.USAEPayAPI.Interfaces
{
    public interface IUSAEpayAPIHelper
    {
        public Task<HttpResponseMessage> PostTransactionAsync(TransactionRequestDTO transactionRequestDTO);
    }
}
