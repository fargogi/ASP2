using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebStore.DomainNew.DTO.User
{
    public class AddLoginDTO : IdentityModelDTO
    {
        public UserLoginInfo UserLoginInfo { get; set; }
    }
}
