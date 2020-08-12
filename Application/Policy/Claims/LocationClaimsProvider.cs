using System.Linq;

namespace Application.Infrastructure
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Configuration;

    namespace Application.Policy.Claims
    {
        public class LocationClaimsProvider : IClaimsTransformation
        {
            private readonly string pattern;
            private readonly IConfiguration _configuration;
            public LocationClaimsProvider(IConfiguration configuration)
            {
                _configuration = configuration;
                pattern = "AspNet.Identity.SecurityStamp";
            }
            public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
            {
                if (principal != null)
                {
                    var stamp = principal.Claims.FirstOrDefault(x => x.Type.Equals(pattern))?.Value;
                    ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
                    if (stamp != null)
                    {
                        if (stamp == _configuration["ClaimStamps:Admin"])
                        {
                            identity.AddClaims(new Claim[] {

                         new Claim(ClaimTypes.Authentication, "Admin", ClaimValueTypes.String, "ClaimsControl"),
                         new Claim(ClaimTypes.Authentication, "User", ClaimValueTypes.String, "ClaimsControl"),

                         });
                        }
                        else if (stamp == _configuration["ClaimStamps:SuperAdmin"])
                        {
                            identity.AddClaims(new Claim[] {

                         new Claim(ClaimTypes.Authentication, "SuperAdmin", ClaimValueTypes.String, "ClaimsControl"),
                         new Claim(ClaimTypes.Authentication, "Admin", ClaimValueTypes.String, "ClaimsControl"),
                         new Claim(ClaimTypes.Authentication, "User", ClaimValueTypes.String, "ClaimsControl"),
                         });
                        }
                        else
                        {
                            identity.AddClaims(new Claim[] {
                         new Claim(ClaimTypes.Authentication, "User", ClaimValueTypes.String, "ClaimsControl"),
                        });
                        }
                    }
                }
                return Task.FromResult(principal);
            }
        }
    }

}
