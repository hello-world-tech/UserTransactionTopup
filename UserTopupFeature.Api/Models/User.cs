using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTopupFeature.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public decimal Balance { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}