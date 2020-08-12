using Application.Policy.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Policy.Handlers
{
    public class AdminPolicyHandler : AuthorizationHandler<AdminPolicyRequirement>
    {
        const string pattern = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authentication";
        protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AdminPolicyRequirement requirement)
        {
            var role = context.User.Claims.Where(x =>  x.Type.Equals(pattern)).ToList();
            var specificRole = role.FirstOrDefault(x => x.Value == requirement._role);

            if (role != null && specificRole?.Value == requirement._role)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
