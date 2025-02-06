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
    public class GlobalTransactionConfiguration : IEntityTypeConfiguration<GlobalTransaction>
    {
        public void Configure(EntityTypeBuilder<GlobalTransaction> entity)
        {
            entity.HasKey(e => e.TransactionID);
            entity.ToTable("Global_Transactions");

            entity.Property(e => e.TransactionID);
            entity.Property(e => e.CustomerID);
            entity.Property(e => e.Amount);
            entity.Property(e => e.TransactionStatusID);
            entity.Property(e => e.PaymentGWTransID)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResponseCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SubErrorDesc1)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.SubErrorDesc2)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.SubErrorDesc3)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreditCardID);
        }
    }
}
