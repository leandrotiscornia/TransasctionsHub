using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace _011Global.Shared.JobsServiceDBContext.Configurations
{
    public class GlobalJobConfiguration : IEntityTypeConfiguration<GlobalJob>
    {
        public void Configure(EntityTypeBuilder<GlobalJob> entity)
        {
                entity.HasKey(e => e.Id).HasName("PK__Global_J__3214EC27AF7B5666");

                entity.ToTable("Global_Jobs");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Assembly)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.LastStartDate).HasColumnType("datetime");
                entity.Property(e => e.LastStopDate).HasColumnType("datetime");
                entity.Property(e => e.MachineNameList)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.TypeName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
        }
    }
}
