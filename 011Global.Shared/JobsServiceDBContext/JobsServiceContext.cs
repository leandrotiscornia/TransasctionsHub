using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _011Global.Shared.JobsServiceDBContext.Entities;
using _011Global.Shared.JobsServiceDBContext.Configurations;


namespace _011Global.Shared.JobsServiceDBContext;

public partial class JobsServiceContext : DbContext
{
    public JobsServiceContext(DbContextOptions<JobsServiceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GlobalJob> GlobalJobs { get; set; }
    public virtual DbSet<GlobalCustomer> GlobalCustomers { get; set; }
    public virtual DbSet<GlobalCreditCard> GlobalCreditCards { get; set; }
    public virtual DbSet<GlobalAddress> GlobalAddresses { get; set; }
    public virtual DbSet<GlobalTransaction> GlobalTransaction { get; set; }
    public virtual DbSet<GlobalTransactionsStatus> GlobalTransactionStatuses { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GlobalJobConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalCustomerConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalCreditCardConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalAddressConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalTransactionConfiguration());
        modelBuilder.ApplyConfiguration(new GlobalTransactionStatusConfiguration());


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
