using _011Global.Shared.JobsServiceDBContext.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.JobsServiceDBContext.Repos;

public class JobsServiceRepository : IJobsServiceRepository
{
    private readonly JobsServiceContext _context;
    public JobsServiceRepository(JobsServiceContext context)
    {
        _context = context;
    }

    public async Task<Dictionary<string, GlobalJob>> GetJobs(string hostName)
    {
        return await _context.GlobalJobs.Where(g => g.MachineNameList.Contains(hostName)).ToDictionaryAsync(gk => gk.TypeName, gv => gv);
    }
}
