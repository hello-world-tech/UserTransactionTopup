using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTopupFeature.Api.Models
{
    public class Beneficiary
    {
        public int BeneficiaryId { get; set; }
        public string Nickname { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}