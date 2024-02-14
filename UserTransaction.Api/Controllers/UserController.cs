using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using UserTransaction.Api.Services;

namespace UserTransaction.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly UserTransactionEntities _dbContext;
        private readonly BeneficiaryService _beneficiaryService;

        public UserController()
        {
            _dbContext = new UserTransactionEntities(); // Replace YourEntities with the name of your DbContext class
            _beneficiaryService = new BeneficiaryService();
        }

        [HttpPost]
        [Route("api/user/credit")]
        public IHttpActionResult CreditAmount(int userId, decimal amount)
        {
            var user = _dbContext.Users.Include("Transactions").FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            user.Balance += amount;
            _dbContext.SaveChanges();

            return Ok($"Amount {amount} credited successfully. Current balance: {user.Balance}");
        }

        [HttpPost]
        [Route("api/user/debit")]
        public IHttpActionResult DebitAmount(int userId, decimal amount)
        {
            var user = _dbContext.Users.Include("Transactions").FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Balance < amount)
            {
                return BadRequest("Insufficient balance.");
            }

            user.Balance -= amount;
            _dbContext.SaveChanges();

            _dbContext.Transactions.Add(new Transaction { Amount = amount, IsCredit = false, TransactionDate = DateTime.Now });
            _dbContext.SaveChanges();

            return Ok($"Amount {amount} debited successfully. Current balance: {user.Balance}");
        }

        [HttpGet]
        [Route("api/user/balance/{userId}")]
        public IHttpActionResult GetBalance(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            return Ok(user.Balance);
        }

        [HttpPost]
        [Route("api/user/updatebalance")]
        public IHttpActionResult UpdateBalance(int userId, decimal newBalance)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return BadRequest("Unable to update balance.");
            }

            user.Balance = newBalance;
            _dbContext.SaveChanges();

            return Ok($"Balance updated successfully to {newBalance}.");
        }

        [HttpGet]
        [Route("api/user/beneficiaries/all/{userId}")]
        public IHttpActionResult GetAllBeneficiaries(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return BadRequest("Beneficiary for the User does not found.");
            }
            var allBeneficiaries = _beneficiaryService.GetBeneficiaries(userId);
            //user.Beneficiaries = allBeneficiaries;
            return Ok(allBeneficiaries);
        }

        [HttpGet]
        [Route("api/user/beneficiaries/{beneficiaryId}")]
        public IHttpActionResult GetBeneficiary(int beneficiaryId)
        {
            var beneficiary = _dbContext.Beneficiaries.FirstOrDefault(u => u.BeneficiaryId == beneficiaryId);
            if (beneficiary == null)
            {
                return BadRequest("Beneficiary not found.");
            }
            
            return Ok(beneficiary);
        }

        [HttpPost]
        [Route("api/user/benefiacry/add")]
        public IHttpActionResult AddNewBeneficiary(int userId, string nickName)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null || string.IsNullOrEmpty(nickName))
            {
                return BadRequest("Unable to add beneficiary.");
            }

            bool isAdded = _beneficiaryService.AddBeneficiary(userId, nickName);

            return Ok(isAdded);

        }

        [HttpGet]
        [Route("api/user/{userId}")]
        public async Task<IHttpActionResult> GetUserById(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(user);
        }
    }
}
