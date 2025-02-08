using _011Global.JobsService.JobInterfaces;
using _011Global.Shared.JobsServiceDBContext.Entities;
using _011Global.Shared;
using _011Global.Shared.JobsServiceDBContext.Interfaces;
using Microsoft.Extensions.Configuration;
using _011Global.Shared.USAEPayAPI.Interfaces;
using _011Global.Shared.USAEPayAPI.DTOs;
using System.Net.Http.Json;
using Azure;
using System.Text.Json;
using _011Global.Shared.Utilities;
using _011Global.Shared.SecretServiceConnection;
using System.Text.Json.Serialization;

namespace _011Global.JobsService.JobImplementations;

public class MonthlyChargeJob : Job, IJob
{
    protected override int IterationWaitTime { get { return 5000; } }
    private static IUSAEpayAPIHelper _USAEpayAPIHelper;
    private readonly IServiceProvider _serviceProvider;
    public MonthlyChargeJob(CancellationTokenBase cancellationTokenBase, ILogger<MonthlyChargeJob> logger,
        IUSAEpayAPIHelper USAEpayAPIHelper, IServiceProvider serviceProvider)
        : base(cancellationTokenBase, logger, serviceProvider)
    {

        _serviceProvider = serviceProvider;
        _USAEpayAPIHelper = USAEpayAPIHelper;
    }



    protected override async Task WorkLoad(CancellationToken cancellationToken)
    {
        try
        {
            using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var _jobRepo = scope.ServiceProvider.GetRequiredService<IJobsServiceRepository>();
                List<GlobalCustomer> globalCustomers = await _jobRepo.GetSubscribedCustomers();

                foreach (GlobalCustomer customer in globalCustomers)
                { 
                    DateTime? date = customer.GlobalTransactions.
                        Max(gt => gt.CreationDate);
                    if (!date.HasValue || date.Value.AddDays(30) <= DateTime.Now)
                        await Charge(customer);
                }
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
            throw;
        }
        logger.LogInformation($"{Name} my last run time was: {DateTime.Now}");

    }
    protected async Task Charge(GlobalCustomer customer)
    {
        try
        {
            GlobalCreditCard customerCreditCard = 
                        customer.GlobalCreditCards.Where(c => c.CreditCardID == customer.PreferredCreditCardId).First();
            string key = await SecretServiceAccess.GetAESEncryptionKey();
            string iv = await SecretServiceAccess.GetAESCreditCardIV(customerCreditCard.CreditCardID);
            string decryptedCreditCardNumber = 
                EncryptionUtility.AES256Decrypt(customerCreditCard.CreditCardNumber, key, iv);
            var jsone = JsonSerializer.Serialize(customer, new JsonSerializerOptions
            {
                ReferenceHandler =ReferenceHandler.Preserve
            } );
            Console.WriteLine(jsone);

            TransactionRequestDTO transactionRequestDTO = new TransactionRequestDTO
            {
                amount = customer.GlobalPackage.Cost,
                software = "TransactionsHub 1.0",
                creditcard = new CreditCardDTO
                {
                    cardholdername = $"{customer.CustomerFirstName} {customer.CustomerLastName}",
                    number = customerCreditCard.CreditCardNumber,
                    expiration = $"{customerCreditCard.ExpirationMonth}{customerCreditCard.ExpirationYear}",
                    avs_street = customer.BillingAddress.Address,
                    avs_postalcode = customer.BillingAddress.ZipCode
                }
            };
            var response = await _USAEpayAPIHelper.PostTransactionAsync(transactionRequestDTO);
            var json = await response.Content.ReadAsStringAsync();
            TransactionResponseDTO transactionResponseDTO = new TransactionResponseDTO();
            if (json != "")
                transactionResponseDTO = JsonSerializer.Deserialize<TransactionResponseDTO>(json);


            GlobalTransaction globalTransaction = new GlobalTransaction
            {
                CustomerID = customer.CustomerID,
                CreditCardID = customerCreditCard.CreditCardID,
                Amount = customer.GlobalPackage.Cost,
                TransactionStatusID = (byte)(int)Enum.Parse(typeof(TransactionStatusCode), transactionResponseDTO.resultcode),
                PaymentGWTransID = transactionResponseDTO.refnum,
                SubErrorDesc1 = "",
                SubErrorDesc2 = "",
                SubErrorDesc3 = "",
                CreationDate = DateTime.Now,
                ResponseCode = response.StatusCode.ToString(),
            };
            using var scope = _serviceProvider.CreateAsyncScope();
            var _jobRepo = scope.ServiceProvider.GetRequiredService<IJobsServiceRepository>();
            await _jobRepo.SaveTransaction(globalTransaction);
        }
        catch (Exception e)
        {
            logger.LogCritical(e.Message);
            throw e;
        }
    }
}