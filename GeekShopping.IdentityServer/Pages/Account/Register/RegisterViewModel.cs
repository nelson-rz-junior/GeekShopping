using System.ComponentModel.DataAnnotations;
using static GeekShopping.Pages.Login.ViewModel;

namespace GeekShopping.IdentityServer.Pages.Account.Register;

public class RegisterViewModel
{
    [Required(ErrorMessage = "The username is required")]
    public string Username { get; set; }

    [Required(ErrorMessage = "The email is required")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The first name is required")]
    [Display(Name = "First name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "The last name is required")]
    [Display(Name = "Last name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "The phone number is required")]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "The password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "The role name is required")]
    [Display(Name = "Role name")]
    public string RoleName { get; set; }

    public string? ReturnUrl { get; set; }

    public string Button { get; set; }

    public bool AllowRememberLogin { get; set; }

    public bool EnableLocalLogin { get; set; }

    public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();

    public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !string.IsNullOrWhiteSpace(x.DisplayName));

    public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;

    public string? ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
}
