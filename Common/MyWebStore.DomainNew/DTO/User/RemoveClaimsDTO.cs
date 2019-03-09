using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace MyWebStore.DomainNew.DTO.User
{
    public class RemoveClaimsDTO : IdentityModelDTO
    {
        public IEnumerable<Claim> Claims { get; set; }
    }
}
