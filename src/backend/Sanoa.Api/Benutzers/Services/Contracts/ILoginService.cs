using SanoaAPI.DTOs;

namespace SanoaAPI.Benutzers.Services.Contracts;

public interface ILoginService
{
    string CreateJwtToken(loginParms parms);
}