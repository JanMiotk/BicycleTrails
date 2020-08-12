using Application.Policy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Policy.Handlers
{
    public class SuperAdminPolicyHandler : AuthorizationHandler<SuperAdminPolicyRequirement>
    {
        const string stampPattern = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";
        const string emailPattern = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";

        protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        SuperAdminPolicyRequirement requirement)
        {
            var role = context.User.Claims.Where(x => x.Type.Equals(stampPattern)).ToList();
            var specificRole = role.FirstOrDefault(x => x.Value == requirement._role);

            if (role != null && specificRole?.Value == requirement._role )
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

        }
    }

}
