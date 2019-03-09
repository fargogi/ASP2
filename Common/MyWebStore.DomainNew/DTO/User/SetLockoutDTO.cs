using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.User
{
    public class SetLockoutDTO : IdentityModelDTO
    {
        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
