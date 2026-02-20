namespace SanoaAPI.Benutzers.Models.Settings;

public class TokenSettings
{
    public string PasswordForValidUser { get; set; } = string.Empty;
    public string ValidIssuer { get; set; } = string.Empty;
    public string IssuerSigningKey { get; set; } = string.Empty;
}