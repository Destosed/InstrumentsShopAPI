using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace InstrumentsShopAPI.Models
{
    public class CustomUserIdentity: IdentityUser
    {
        public int Age { get; set; }
    }
}