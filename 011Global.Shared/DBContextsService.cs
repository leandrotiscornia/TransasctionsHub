using _011Global.Shared.JobsServiceDBContext;
using _011Global.Shared.JobsServiceDBContext.Interfaces;
using _011Global.Shared.JobsServiceDBContext.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared
{
    public static class DBContextsService
    {
        public static IServiceCollection RegisterDBContexts(this IServiceCollection _services, string? connectionString)
        {
            _services.AddDbContext<JobsServiceContext>(options => options.UseSqlServer(connectionString,
            sqlServerOptions => sqlServerOptions.CommandTimeout(120).EnableRetryOnFailure()));

            _services.AddScoped<IJobsServiceRepository, JobsServiceRepository>();

            return _services; 
        }
    }
}
