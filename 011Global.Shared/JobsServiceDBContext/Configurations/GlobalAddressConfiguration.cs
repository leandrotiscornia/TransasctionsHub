using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;

namespace _011Global.Shared.JobsServiceDBContext.Configurations
{
    public class GlobalAddressConfiguration : IEntityTypeConfiguration<GlobalAddress>
    {
        public void Configure(EntityTypeBuilder<GlobalAddress> entity)
        {
            entity.HasKey(e => e.AddressID);
            entity.ToTable("Global_Addresses");

            entity.Property(e => e.AddressID);
            entity.Property(e => e.CountryISO2);
            entity.Property(e => e.StateISO2)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ZipCode)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
        }
    }
}
