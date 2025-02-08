using _011Global.Shared.JobsServiceDBContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using Azure.Identity;

namespace _011Global.Shared.JobsServiceDBContext.Repos;

public class JobsServiceRepository : IJobsServiceRepository
{
    private readonly JobsServiceContext _context;
    public JobsServiceRepository(JobsServiceContext context)
    {
        _context = context;
    }

    public async Task<List<GlobalCustomer>> GetSubscribedCustomers()
    {
        try
        {
            return await _context.GlobalCustomers
                .Include(gc => gc.GlobalTransactions)
                .Include(gc => gc.GlobalCreditCards)
                .Include(gc => gc.BillingAddress)
                .Include(gc => gc.ShippingAddress)
                .Include(gc => gc.GlobalPackage)
                .Where(gc => gc.Subscribed)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }

    public async Task<Dictionary<string, GlobalJob>> GetJobs(string hostName)
    {
        try
        {
            return await _context.GlobalJobs
            .Where(g => g.MachineNameList
            .Contains(hostName))
            .ToDictionaryAsync(gk => gk.TypeName, gv => gv);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }

    }
    
    public async Task EnrolCustomer(GlobalCustomer customer)
    {
        try
        {
            
            if (!customer.BillingAddress.Equals(customer.ShippingAddress))
            {

                _context.GlobalAddresses.AddRange(customer.BillingAddress, customer.ShippingAddress);
                await _context.SaveChangesAsync();
            }
            else
            {
                
                _context.GlobalAddresses.Add(customer.ShippingAddress);
                customer.BillingAddress = customer.ShippingAddress;
                await _context.SaveChangesAsync();
            }

            

            customer.GlobalPackage = _context.GlobalPackages.FirstOrDefault(gp => gp.ServicePackageID == customer.ServicePackageID);
            _context.GlobalCustomers.Add(customer);
            _context.GlobalCreditCards.AddRange(customer.GlobalCreditCards);
            await _context.SaveChangesAsync();
            customer.PreferredCreditCardId = customer.GlobalCreditCards[0].CreditCardID;
            await _context.SaveChangesAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }

    public async Task UnsubscribeCustomer(int customerId)
    {
        try
        {
            var result = _context.GlobalCustomers.Where(gc => gc.CustomerID == customerId).FirstOrDefault();
            if (result != null)
            {
                result.Subscribed = false;
                _context.Entry(result).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }
    public async Task SaveTransaction(GlobalTransaction transaction)
    {
        try
        {
            _context.GlobalTransaction.Add(transaction);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
        }
    }

    public async Task<List<GlobalCreditCard>> GetUnsecuredCreditCards()
    {
        try
        {
            return await _context.GlobalCreditCards.Where(cc => cc.Encrypted == false).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task EncryptCreditCards(List<GlobalCreditCard> creditCards)
    {
        try
        {
            foreach(var creditCard in creditCards)
            {
                creditCard.Encrypted = true;
                _context.GlobalCreditCards.Add(creditCard);
                _context.Entry(creditCard).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task StopJob(string jobName)
    {
        try
        {
            var job =_context.GlobalJobs.Where(gj => gj.Name == jobName).FirstOrDefault();
            job.LastStopDate = DateTime.Now;
            job.IsRunning = false;
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw e;
        }
    }
    public async Task StartJob(string jobName)
    {
        try
        {
            var job = _context.GlobalJobs.Where(gj => gj.Name == jobName).FirstOrDefault();
            job.LastStartDate = DateTime.Now;
            job.IsRunning = true;
            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {

            throw e;
        }
    }
}
 