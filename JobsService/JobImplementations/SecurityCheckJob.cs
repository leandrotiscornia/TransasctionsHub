using _011Global.JobsService.JobInterfaces;
using _011Global.Shared;
using _011Global.Shared.JobsServiceDBContext.Entities;
using _011Global.Shared.JobsServiceDBContext.Interfaces;
using _011Global.Shared.SecretServiceConnection;
using _011Global.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _011Global.JobsService.JobImplementations
{
    public class SecurityCheckJob : Job, IJob
    {
        private readonly IServiceProvider _serviceProvider;
        protected override int IterationWaitTime { get { return 30000; } }
        public SecurityCheckJob(CancellationTokenBase cancellationTokenBase, ILogger<MonthlyChargeJob> logger, 
            IServiceProvider serviceProvider) : base(cancellationTokenBase, logger, serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }


        protected override async Task WorkLoad(CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _serviceProvider.CreateAsyncScope();
                var _jobRepo = scope.ServiceProvider.GetRequiredService<IJobsServiceRepository>();
                List<GlobalCreditCard> globalCreditCards = await _jobRepo.GetUnsecuredCreditCards();
                List<GlobalCreditCard> cardsToEncrypt = new List<GlobalCreditCard>();
                string key = await SecretServiceAccess.GetAESEncryptionKey();
                string iv;
                foreach (var creditCard in globalCreditCards)
                {
                    if(!creditCard.Encrypted)
                    {
                        iv = await SecretServiceAccess.GetAESCreditCardIV(creditCard.CreditCardID);
                        creditCard.CreditCardNumber =
                            EncryptionUtility.AES256Encrypt(creditCard.CreditCardNumber, key, iv);
                        cardsToEncrypt.Add(creditCard);
                    }
                }
                logger.LogInformation($"{cardsToEncrypt.Count} credit cards need to be encrypted");
                await _jobRepo.EncryptCreditCards(cardsToEncrypt);

            }
            catch (Exception e)
            {
                logger.LogCritical(e.Message);
                throw;
            }
        }
    }
}
