using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainEntities.Entities
{
    public class User : IdentityUser
    {
        public const string UserRole = "User";
        public const string AdminRole = "Administrator";
        public const string AdminUser = "Administrator";
    }
}
