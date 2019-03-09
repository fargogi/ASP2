using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.User
{
    public class PasswordHashDTO : IdentityModelDTO
    {
        public string Hash { get; set; }
    }
}
