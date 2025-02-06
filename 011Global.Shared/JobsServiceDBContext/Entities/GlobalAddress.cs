using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalAddress
    {
        public int AddressID {  get; set; }
        public short CountryISO2 { get; set; }
        public string StateISO2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}
