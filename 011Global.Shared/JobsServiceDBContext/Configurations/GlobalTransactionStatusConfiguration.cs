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
    public class GlobalTransactionStatusConfiguration : IEntityTypeConfiguration<GlobalTransactionsStatus>
    {
        public void Configure(EntityTypeBuilder<GlobalTransactionsStatus> entity)
        {
            entity.HasKey(e => e.TransactionStatusID);
            entity.ToTable("Global_TransactionsStatuses");

            entity.Property(e => e.TransactionStatusID);
            entity.Property(e => e.TransactionStatus)
                .HasMaxLength(256)
                .IsUnicode(false);
        }
    }
}
