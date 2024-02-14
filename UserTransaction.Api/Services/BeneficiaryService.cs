using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTransaction.Api.Services
{
    public class BeneficiaryService
    {
        private readonly UserTransactionEntities _dbContext;

        public BeneficiaryService()
        {
            _dbContext = new UserTransactionEntities(); // Replace YourEntities with your DbContext
        }

        public bool AddBeneficiary(int userId, string nickname)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return false;
            }

            if (user.Beneficiaries.Count >= 5)
            {
                return false; // Reached maximum limit of beneficiaries
            }

            if (nickname.Length > 20)
            {
                return false; // Nickname exceeds maximum length
            }

            user.Beneficiaries.Add(new Beneficiary { Nickname = nickname });
            _dbContext.SaveChanges();

            return true;
        }

        public List<Beneficiary> GetBeneficiaries(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            var beneficiaries = _dbContext.Beneficiaries.Where(u => u.UserId == userId).ToList();
            return beneficiaries;
        }
    }
}