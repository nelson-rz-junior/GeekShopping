using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configurations;

public static class IdentityConfigurations
{
    public const string ADMIN = "Admin";
    public const string CLIENT = "Client";

    private static readonly string GeekShoppingScope = "geek_shopping";
    private static readonly string Secret = "my-super-secret-key";
    private static readonly string MachineClientId = "client";
    private static readonly string WebAppId = "web";
    private static readonly string RedirectUri = "https://localhost:1001/signin-oidc";
    private static readonly string RedirectAfterLogin = "https://localhost:1001/signout-callback-oidc";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(GeekShoppingScope, "GeekShopping Server"),
            new ApiScope("read", "Read data"),
            new ApiScope("write", "Write data"),
            new ApiScope("delete", "Delete data")
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // Machine to machine client
            new Client
            {
                ClientId = MachineClientId,
                ClientSecrets = { new Secret(Secret.Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "write", "profile" }
            },
            // Interactive ASP.NET Core Web App
            new Client
            {
                ClientId = WebAppId,
                ClientSecrets = { new Secret(Secret.Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { RedirectUri },
                PostLogoutRedirectUris = { RedirectAfterLogin },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    GeekShoppingScope
                }
            }
        };
}
