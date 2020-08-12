using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Policy.Models
{
    public class AdminPolicyRequirement : IAuthorizationRequirement 
    { 
        public readonly string _role;
        public AdminPolicyRequirement()
        {
            _role = "Admin";
        }
    }
}
