using _011Global.Shared.JobsServiceDBContext.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _011Global.Shared.SecretServiceConnection
{
    public static class SecretServiceAccess
    {
        /// <summary>
        /// This should access a Secret Manager service, it could be Hashicorp's Vault, Keycloak, AWS secret manager 
        /// or any other. This is just a hard-coded class, the real service should be deployed on an internal network
        /// with several access restriction.
        /// </summary>
        public static string SecretServiceUrl { get; set; }


        public static async Task<string> GetUSAEpayKeyPin( )
        {
            string key = "_1v60HeN4Wpe03jK2rcZ8Q6u309MRj48";
            Console.WriteLine($"Key for UsaEpayAPI retrieved from secret's service");
            return key;
        }

        public static async Task<string> GetUSAEpayKey( )
        {
            string pin = "sdk5G4";
            Console.WriteLine($"Pin for UsaEpayAPI retrieved from secret's service");
            return pin;
        }

        public static async Task<string> GetDatabaseUserName(string database)
        {
            string user = "";
            Console.WriteLine($"Username for database {database} retrieved from secret's service");
            return user;
        }
        public static async Task<string> GetDatabasePassword(string database)
        {
            string password = "";
            Console.WriteLine($"Password for database {database} retrieved from secret's service");
            return password;
        }
        public static async Task<string> GetAESEncryptionKey()
        {
            string key = "w9z8k+WIOtb/vVafrcjFFTdNLFul9bZRvkwulZzUP5Q=";
            Console.WriteLine("Encryption key retrieved from secret's server");
            return key;
        }

        public static async Task<string> GetAESCreditCardIV(int globalCreditCardId) 
        {
            string iv = "5TGbbhQxsiWXsi6W9fP3dQ==";
            Console.WriteLine($"Initialization vector for {globalCreditCardId} retrieved from secret's server");
            return iv;
        }

        public static async Task SaveAESCreditCardIV(string iv, int globalCreditCardId)
        {
            Console.WriteLine($"Initialization vector for {globalCreditCardId} saved in secret's server");
        }
    }
}
