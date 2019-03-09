using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.Entities
{
    public class User : IdentityUser
    {
        public const string UserRole = "User";
        public const string AdminRole = "Administrator";
        public const string AdminUser = "Administrator";
    }
}
