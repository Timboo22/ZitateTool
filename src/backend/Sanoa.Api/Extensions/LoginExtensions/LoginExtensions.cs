using SanoaAPI.Benutzers.Services.Contracts;
using SanoaAPI.DTOs;

namespace SanoaAPI.Extensions.LoginExtensions;

public static class LoginExtensions
{
    public static void LoginEndpoint(this WebApplication app)
    {
        app.MapPost("auth/login", (loginParms loginParms, ILoginService loginService) =>
        {
            var token = loginService.CreateJwtToken(loginParms);

            if (string.IsNullOrEmpty(token))
                return Results.BadRequest("Benutzer gibt es nicht oder Passwort ist Falsch");

            return Results.Ok(new { Token = token });
        });
    }
}