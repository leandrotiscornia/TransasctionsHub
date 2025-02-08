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

    public Task<List<GlobalCustomer>> GetSubscribedCustomers();

    public Task EnrolCustomer(GlobalCustomer customer);

    public Task UnsubscribeCustomer(int  customerId);

    public Task SaveTransaction(GlobalTransaction transaction);

    public Task<List<GlobalCreditCard>> GetUnsecuredCreditCards();

    public Task EncryptCreditCards(List<GlobalCreditCard> creditCards);
    public Task StartJob(string jobName);
    public Task StopJob(string jobName);
}

