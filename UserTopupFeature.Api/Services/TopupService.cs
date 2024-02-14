using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;
using UserTopupFeature.Api.Models;

namespace UserTopupFeature.Api.Services
{
    public class TopupService
    {
        private readonly UserTopUpAppEntities _dbContext;
        

        public TopupService()
        {
            _dbContext = new UserTopUpAppEntities();
            
        }

        public List<TopUpOption> GetAllTopup()
        {
            var allTopUp = _dbContext.TopUpOptions.ToList();

            return allTopUp;
        }

        public async Task<User> GetUserById(int userId)
        {
            User user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/user/"+userId);
                if (response.IsSuccessStatusCode)
                {
                    var u = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(u))
                    {
                        user = JsonConvert.DeserializeObject<User>(u);
                    }


                }
                
            }

            return user;
        }

        public async Task<Beneficiary> GetBeneficiaryById(int beneficiaryId)
        {
            Beneficiary beneficiary = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/user/beneficiaries/"+ beneficiaryId);
                if (response.IsSuccessStatusCode)
                {
                    var u = await response.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(u))
                    {
                        beneficiary = JsonConvert.DeserializeObject<Beneficiary>(u);
                    }
                }
                
            }

            return beneficiary;
        }

        public async Task<decimal> GetUserBalance(int userId)
        {
            decimal balance = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44372/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("api/user/balance/"+userId);
                if (response.IsSuccessStatusCode)
                {
                    balance = Convert.ToDecimal(await response.Content.ReadAsStringAsync());

                }
                
            }

            return balance;
        }

        public async Task<bool> DoTransaction(int userId, decimal amount, bool isCredit)
        {
            bool isTransactionSuccess = false;
            string endPoint = string.Empty;

            using (var client = new HttpClient())
            {
                if (isCredit)
                    endPoint = "api/user/credit/?";
                else
                    endPoint = "api/user/debit/?";
                client.BaseAddress = new Uri("https://localhost:44372/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Post Method

                HttpResponseMessage response = await client.PostAsync(endPoint + "userId="+userId+"&amount="+amount, null);
                if (response.IsSuccessStatusCode)
                {
                    isTransactionSuccess = true;

                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }

            return isTransactionSuccess;
        }

        public async Task<bool> TopupTransaction(int userId, int beneficiaryId, decimal topupAmount)
        {
            bool isSuccess = false;
            decimal totalTopUpThisMonth = 0;
            decimal totalTopUpPerBeneficiaryThisMonth = 0;
            //get User
            User user = await GetUserById(userId);
            Beneficiary beneficiary = await GetBeneficiaryById(beneficiaryId);

            // Apply charge of Rs 1 for every top-up transaction
            topupAmount += 1;

            // Check if user is verified and adjust top-up limit accordingly
            decimal maxTopUpPerMonth = user.IsVerified ? 1000 : 500;

            // Check if total top-up amount for all beneficiaries exceeds Rs 3000 per month
            if (_dbContext.TopUpRecords.Count() > 0)
            {
                totalTopUpThisMonth = _dbContext.TopUpRecords
                    .Where(t => t.UserId == userId && t.TransactionDate.Month == DateTime.Now.Month)
                    .Sum(t => t.Amount);

                totalTopUpPerBeneficiaryThisMonth = _dbContext.TopUpRecords
                    .Where(t => t.UserId == userId && t.BeneficiaryId == beneficiaryId && t.TransactionDate.Month == DateTime.Now.Month)
                    .Sum(t => t.Amount);
            }

            if (totalTopUpPerBeneficiaryThisMonth + topupAmount > maxTopUpPerMonth || totalTopUpThisMonth > 3000)
            {
                return isSuccess = false;
            }

            //Debit user balance first
            bool isDebitSuccess = await DoTransaction(userId, topupAmount, false);

            if (isDebitSuccess)
            {
                // Add top-up transaction
                _dbContext.TopUpRecords.Add(new TopUpRecord { UserId = userId, BeneficiaryId = beneficiaryId, Amount = topupAmount, TransactionDate = DateTime.Now });

                // Save changes
                _dbContext.SaveChanges();

                isSuccess = true;
            }

            return isSuccess;
        }

    }
}