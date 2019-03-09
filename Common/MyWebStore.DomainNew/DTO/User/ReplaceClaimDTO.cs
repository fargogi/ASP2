using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MyWebStore.DomainNew.DTO.User
{
    public class ReplaceClaimDTO
    {
        public Claim OldClaim { get; set; }
        public Claim NewClaim { get; set; }
    }
}
