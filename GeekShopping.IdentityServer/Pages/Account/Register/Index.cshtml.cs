using Duende.IdentityServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Duende.IdentityServer.Events;
using GeekShopping.IdentityServer.Models;
using GeekShopping.Pages;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Duende.IdentityServer.Models;
using static GeekShopping.Pages.Login.ViewModel;

namespace GeekShopping.IdentityServer.Pages.Account.Register;

public class IndexModel : PageModel
{
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IAuthenticationSchemeProvider _schemeProvider;
    private readonly IEventService _events;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IndexModel(
        IAuthenticationSchemeProvider schemeProvider, 
        IIdentityServerInteractionService interaction, 
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<ApplicationUser> signInManager,
        IEventService events)
    {
        _schemeProvider = schemeProvider;
        _interaction = interaction;
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _events = events;
    }

    [BindProperty(SupportsGet = true)]
    public RegisterViewModel Register { get; set; }

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        Register = await BuildRegisterViewModelAsync(returnUrl);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        var context = await _interaction.GetAuthorizationContextAsync(Register.ReturnUrl);

        // the user clicked the "cancel" button
        if (Register.Button == "cancel")
        {
            if (context != null)
            {
                // if the user cancels, send a result back into IdentityServer as if they 
                // denied the consent (even if this client does not require consent).
                // this will send back an access denied OIDC error response to the client.
                await _interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage(Register.ReturnUrl);
                }

                return Redirect(Register.ReturnUrl);
            }
            else
            {
                // since we don't have a valid context, then we just go back to the home page
                return Redirect("~/");
            }
        }

        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = Register.Username,
                Email = Register.Email,
                EmailConfirmed = true,
                FirstName = Register.FirstName,
                LastName = Register.LastName,
                PhoneNumber= Register.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, Register.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(Register.RoleName))
                {
                    var userRole = new IdentityRole
                    {
                        Name = Register.RoleName,
                        NormalizedName = Register.RoleName
                    };

                    await _roleManager.CreateAsync(userRole);
                }

                await _userManager.AddToRoleAsync(user, Register.RoleName);

                await _userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, $"{Register.FirstName} {Register.LastName}"),
                    new Claim(JwtClaimTypes.Email, Register.Email),
                    new Claim(JwtClaimTypes.FamilyName, Register.LastName),
                    new Claim(JwtClaimTypes.GivenName, Register.FirstName),
                    new Claim(JwtClaimTypes.WebSite, $"http://{ Register.Username }.com"),
                    new Claim(JwtClaimTypes.Role, Register.RoleName)
                });

                var loginresult = await _signInManager.PasswordSignInAsync(Register.Username, Register.Password, false, lockoutOnFailure: true);
                if (loginresult.Succeeded)
                {
                    var checkuser = await _userManager.FindByNameAsync(Register.Username);

                    await _events.RaiseAsync(new UserLoginSuccessEvent(checkuser.UserName, checkuser.Id, checkuser.UserName, clientId: context?.Client.ClientId));

                    if (context != null)
                    {
                        if (context.IsNativeClient())
                        {
                            // The client is native, so this change in how to
                            // return the response is for better UX for the end user.
                            return this.LoadingPage(Register.ReturnUrl);
                        }

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return Redirect(Register.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(Register.ReturnUrl))
                    {
                        return Redirect(Register.ReturnUrl);
                    }
                    else if (string.IsNullOrEmpty(Register.ReturnUrl))
                    {
                        return Redirect("~/");
                    }
                    else
                    {
                        // user might have clicked on a malicious link - should be logged
                        throw new Exception("invalid return URL");
                    }
                }
            }
        }

        ViewData["roles"] = new SelectList(new List<string> { "Admin", "Client" });

        // If we got this far, something failed, redisplay form
        return Page();
    }

    private async Task<RegisterViewModel> BuildRegisterViewModelAsync(string returnUrl)
    {
        var context = await _interaction.GetAuthorizationContextAsync(returnUrl);

        ViewData["roles"] = new SelectList(new List<string> { "Admin", "Client" });

        if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
        {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

            // this is meant to short circuit the UI and only trigger the one external IdP
            Register = new RegisterViewModel
            {
                EnableLocalLogin = local,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint
            };

            if (!local)
            {
                Register.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
            }

            return Register;
        }

        var schemes = await _schemeProvider.GetAllSchemesAsync();

        var providers = schemes
            .Where(x => x.DisplayName != null)
            .Select(x => new ExternalProvider
            {
                DisplayName = x.DisplayName ?? x.Name,
                AuthenticationScheme = x.Name
            }).ToList();

        var allowLocal = true;
        var client = context?.Client;
        if (client != null)
        {
            allowLocal = client.EnableLocalLogin;
            if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
            {
                providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
            }
        }

        return new RegisterViewModel
        {
            AllowRememberLogin = AccountOptions.AllowRememberLogin,
            EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
            ReturnUrl = returnUrl,
            Username = context?.LoginHint,
            ExternalProviders = providers.ToArray()
        };
    }
}
