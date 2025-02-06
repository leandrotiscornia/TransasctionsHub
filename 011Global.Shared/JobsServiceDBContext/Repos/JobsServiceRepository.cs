using _011Global.Shared.JobsServiceDBContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;

namespace _011Global.Shared.JobsServiceDBContext.Repos;

public class JobsServiceRepository : IJobsServiceRepository
{
    private readonly JobsServiceContext _context;
    public JobsServiceRepository(JobsServiceContext context)
    {
        _context = context;
    }

    public async Task<List<GlobalCustomer>> GetCustomers()
    {
        try
        {
            return await _context.GlobalCustomers.ToListAsync();
        }
        catch (Exception e)
        {

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

            throw e;
        }
        
    }

    public async Task SaveTransactionDetails(GlobalTransaction transaction)
    {
        try
        {
            await _context.GlobalTransaction.AddAsync(transaction);
        }
        catch (Exception e)
        {
            
            throw e;
        }
    }
}
