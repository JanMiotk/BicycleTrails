using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Policy.Models
{
    public class UserPolicyRequirement : IAuthorizationRequirement 
    {
        public readonly string _role;
        public UserPolicyRequirement() 
        {
            _role = "User";
        }
    }
}
