using _011Global.Shared.JobsServiceDBContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Configurations
{
    public class GlobalCreditCardConfiguration : IEntityTypeConfiguration<GlobalCreditCard>
    {
        public void Configure(EntityTypeBuilder<GlobalCreditCard> entity)
        {
            entity.HasKey(e => e.CreditCardID);
            entity.ToTable("Global_CreditCards");
           

            entity.Property(e => e.CreditCardID);
            entity.Property(e => e.CustomerID);
            entity.Property(e => e.CreditCardNumber)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            entity.Property(e => e.LastFourNumbers)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            entity.Property(e => e.CardHolder)
                    .HasMaxLength(350)
                    .IsUnicode(false);
            entity.Property(e => e.ExpirationMonth)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            entity.Property(e => e.ExpirationYear)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            entity.Property(e => e.Encrypted);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
        }
    }
}
