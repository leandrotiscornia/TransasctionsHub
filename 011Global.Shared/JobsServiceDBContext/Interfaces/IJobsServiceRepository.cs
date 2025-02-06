using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;

namespace _011Global.Shared.JobsServiceDBContext.Interfaces;

public interface IJobsServiceRepository
{
    public Task<Dictionary<string, GlobalJob>> GetJobs(string hostName);

    public Task<List<GlobalCustomer>> GetCustomers();

}

