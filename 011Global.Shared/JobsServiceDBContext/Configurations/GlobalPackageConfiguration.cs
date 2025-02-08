using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace _011Global.Shared.JobsServiceDBContext.Configurations
{
    public class GlobalPackageConfiguration : IEntityTypeConfiguration<GlobalPackage>
    {
        public void Configure(EntityTypeBuilder<GlobalPackage> entity)
        {
            entity.HasKey(e => e.ServicePackageID);

            entity.ToTable("Global_ServicePackages");

            entity.Property(e => e.ServicePackageID);
            entity.Property(e => e.Cost);
            entity.Property(e => e.PackageName)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            entity.Property(e => e.PackageDescription)
                    .HasMaxLength(256)
                    .IsUnicode(false); ;
        }
    }
}
