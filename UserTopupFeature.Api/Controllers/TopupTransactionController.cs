using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserTopupFeature.Api.Services;
using UserTopupFeature.Api.Models;
using System.Threading.Tasks;

namespace UserTopupFeature.Api.Controllers
{
    public class TopupTransactionController : ApiController
    {
        private readonly TopupService _topupService = null;

        public TopupTransactionController()
        {
            _topupService = new TopupService();
        }

        [HttpPost]
        [Route("api/user/topup")]
        public async Task<IHttpActionResult> TopupTransaction(int userId, int beneficiaryId, decimal topupAmount)
        {
            bool isSuccess = await _topupService.TopupTransaction(userId, beneficiaryId, topupAmount);

            return Ok(isSuccess);
        }
    }
}
