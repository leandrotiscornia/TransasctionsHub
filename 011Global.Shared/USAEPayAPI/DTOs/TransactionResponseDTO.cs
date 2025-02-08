using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.USAEPayAPI.DTOs
{
    public class TransactionResponseDTO
    {
        public string refnum { get; set; } = null!;
        public string resultcode { get; set; } = "A";
    }
}
