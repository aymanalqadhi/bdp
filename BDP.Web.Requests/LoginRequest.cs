namespace BDP.Web.Requests;

public class LoginRequest
{
    /// <summary>
    /// Gets or sets the username of the login request
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// Gets or sets the password of the login request (plain-text)
    /// </summary>
    public string Password { get; set; } = null!;
}