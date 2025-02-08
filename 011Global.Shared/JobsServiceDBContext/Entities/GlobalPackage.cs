using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Entities
{
    public partial class GlobalPackage
    {
        public int ServicePackageID { get; set; }
        public string PackageName { get; set; } = null!;
        public string PackageDescription { get; set; } = null!;
        public double Cost { get; set; }
    }
}