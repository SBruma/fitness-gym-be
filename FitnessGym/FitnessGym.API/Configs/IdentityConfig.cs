using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using System.Security.Claims;

namespace FitnessGym.API.Configs
{
    public static class IdentityConfig
    {
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "fittnes-gym-app",
                ClientSecrets = { new Secret("MYSUPERIMPORTANTSECRETKEY12".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { "api" },
                AccessTokenLifetime = 3600,
                AllowOfflineAccess = true,
            }
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            new ApiScope("api", "api"),
        };

        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("api-resource", "Your API Resource")
            {
                Scopes = { "api" }
            }
        };

        public static List<TestUser> Users => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1",
                Username = "alice",
                Password = "_Test123",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    new Claim(JwtClaimTypes.Email, "alice@example.com"),
                }
            }
        };
    }
}
