using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityApiSupport.Infrastructure
{
    public static class LocationClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            List<Claim> claims = new List<Claim>();
            if (user.Name.ToLower() == "ljeanjoseph")
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "HT 001509"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "HT"));
            }
            else
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "FL 33709"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "FL"));
            }
            return claims;
        }

        private static Claim CreateClaim(string type, string value) =>
            new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
    }
}