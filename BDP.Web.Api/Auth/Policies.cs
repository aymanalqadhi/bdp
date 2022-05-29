namespace BDP.Web.Api.Auth;

public static class Policies
{
    public const string IsCustomer = nameof(IsCustomer);
    public const string IsProvider = nameof(IsProvider);
    public const string IsAdmin = nameof(IsAdmin);
    public const string IsRoot = nameof(IsRoot);
}
