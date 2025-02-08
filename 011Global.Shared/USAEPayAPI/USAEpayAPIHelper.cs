using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _011Global.Shared.JobsServiceDBContext.Entities;
using _011Global.Shared.USAEPayAPI.Interfaces;
using _011Global.Shared.USAEPayAPI.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json;
using _011Global.Shared.SecretServiceConnection;

namespace _011Global.Shared.USAEPayAPI
{

    public class USAEpayAPIHelper : IUSAEpayAPIHelper
    {
        private string url;
        public USAEpayAPIHelper(IConfiguration configuration)
        {
            
            url = configuration.GetSection("UsaEpayAPIUrl").Value;
        }
        public async Task<HttpResponseMessage> PostTransactionAsync(TransactionRequestDTO transactionRequestDTO)
        {
            string authorizationKey = await SecretServiceAccess.GetUSAEpayKey();
            string authorizationPin = await SecretServiceAccess.GetUSAEpayKeyPin();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.ConnectionClose = true;
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", generateHash(authorizationKey, authorizationPin));
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                string json = JsonSerializer.Serialize(transactionRequestDTO);
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return await client.SendAsync(requestMessage);
            }
        }


        
        private string generateHash(string authorizationKey, string authorizationPin)
        {
            string preHash = string.Empty;
            string seed = generateRandomString(5, 30);
            
            preHash = authorizationKey + seed + authorizationPin;
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(preHash));
                foreach (byte b in bytes)
                    stringBuilder.Append(b.ToString("x2"));
            }
            string apiHash = "s2/" + seed + "/" + stringBuilder.ToString();
            byte[] hashedBytes = Encoding.UTF8.GetBytes(authorizationKey + ":" + apiHash);
            string finalHash = Convert.ToBase64String(hashedBytes);
            return finalHash;

        }

        private string generateRandomString(int sizeFrom, int sizeTo)
        {
            var charPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int stringLength = RandomNumberGenerator.GetInt32(sizeFrom, sizeTo);
            var chars = new char[stringLength];
            for (int i = 0; i < stringLength; i++)
                chars[i] = charPool[RandomNumberGenerator.GetInt32(charPool.Length)];
            return new string(chars);
        }
    } 
}
