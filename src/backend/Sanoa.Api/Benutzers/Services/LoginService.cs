using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Models.Settings;
using SanoaAPI.Benutzers.Services.Contracts;
using SanoaAPI.DTOs;
using static System.String;

namespace SanoaAPI.Benutzers.Services;

public class LoginService : ILoginService
{
       private readonly IDataService _dataService;
       private readonly IOptionsSnapshot<TokenSettings> _tokenSettings;

       public LoginService(IDataService dataService,  IOptionsSnapshot<TokenSettings> tokenSettings)
       {
              _dataService = dataService;
              _tokenSettings = tokenSettings;
       }

       public string CreateJwtToken(loginParms loginParms)
       {
              var gefundenerNutzer = _dataService
                     .FirstOrDefault<UserAuth>(u => u.username == loginParms.username);

              if (gefundenerNutzer == null || loginParms.password != _tokenSettings.Value.PasswordForValidUser)
                     return Empty;
              
              var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Value.IssuerSigningKey));
              var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
              var token = new JwtSecurityToken(
                     issuer: _tokenSettings.Value.ValidIssuer,
                     audience: "token",
                     claims: new List<Claim>(),
                     expires: DateTime.Now.AddMinutes(30),
                     signingCredentials: creds);
              
              return new JwtSecurityTokenHandler().WriteToken(token);
       }
}