using Microsoft.AspNetCore.Http;

namespace SanoaAPI.Benutzer.Avatar.Services;

public interface IBenutzerAvatarService
{
    void SpeicherBildImOrdner(IFormFile file);
}