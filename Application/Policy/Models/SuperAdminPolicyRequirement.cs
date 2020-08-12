using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Policy.Models
{
    public class SuperAdminPolicyRequirement : IAuthorizationRequirement
    {
        public readonly string _role;
        public readonly string _email;
        public SuperAdminPolicyRequirement()
        {
            _role = "SuperAdmin";
        }
    }
}
