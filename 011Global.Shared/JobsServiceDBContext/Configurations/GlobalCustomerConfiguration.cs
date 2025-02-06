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
    public class GlobalCustomerConfiguration : IEntityTypeConfiguration<GlobalCustomer>
    {
        public void Configure(EntityTypeBuilder<GlobalCustomer> entity)
        {
            entity.HasKey(e => e.CustomerID);
            entity.ToTable("Global_Customers");

            entity.Property(e => e.CustomerID);
            entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            entity.Property(e => e.CustomerLastName)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            entity.Property(e => e.ShippingAddressID);
            entity.Property(e => e.BillingAddressID);
            entity.Property(e => e.MonthlyFee);
            entity.Property(e => e.CreationDate);

        }
       
    }
}
