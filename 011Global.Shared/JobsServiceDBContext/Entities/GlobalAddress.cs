using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalAddress : IEquatable<GlobalAddress>
    {
        public int AddressID {  get; set; }
        public short CountryISO2 { get; set; }
        public string StateISO2 { get; set; } = null!;
        public string City { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime CreationDate { get; set; }

        public bool Equals(GlobalAddress? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if(other.CountryISO2 != this.CountryISO2) return false;
            if(other.StateISO2 != this.StateISO2) return false;
            if(other.City != this.City) return false;
            if(other.ZipCode != this.ZipCode) return false;
            if(other.Address != this.Address) return false;
            return true;
        }
    }
}
