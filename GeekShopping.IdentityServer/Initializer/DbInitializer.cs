using GeekShopping.IdentityServer.Configurations;
using GeekShopping.IdentityServer.Models;
using GeekShopping.IdentityServer.Models.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public DbInitializer(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task InitializeAsync()
    {
        if (await _roleManager.FindByNameAsync(IdentityConfigurations.ADMIN) == null)
        {
            await _roleManager.CreateAsync(new IdentityRole(IdentityConfigurations.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(IdentityConfigurations.CLIENT));

            // Create admin user
            var admin = new ApplicationUser
            {
                UserName = _configuration["DbInitializer:AdminUserName"],
                Email = _configuration["DbInitializer:AdminUserName"],
                FirstName = "Administrator",
                LastName = "User",
                PhoneNumber = "+55 11 98765-4321",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(admin, _configuration["DbInitializer:Password"]);

            // Add user to role
            await _userManager.AddToRoleAsync(admin, IdentityConfigurations.ADMIN);

            // Add claims
            _ = await _userManager.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfigurations.ADMIN)
            });

            // Create client user
            var client = new ApplicationUser
            {
                UserName = _configuration["DbInitializer:ClientUserName"],
                Email = _configuration["DbInitializer:ClientUserName"],
                FirstName = "Client",
                LastName = "User",
                PhoneNumber = "+55 99 12345-6789",
                EmailConfirmed = true
            };

            await _userManager.CreateAsync(client, _configuration["DbInitializer:Password"]);

            // Add user to role
            await _userManager.AddToRoleAsync(client, IdentityConfigurations.CLIENT);

            // Add claims
            _ = await _userManager.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfigurations.CLIENT)
            });

        }
    }
}
