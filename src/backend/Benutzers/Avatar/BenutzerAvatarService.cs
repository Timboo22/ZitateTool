using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SanoaAPI.Benutzer.Avatar.Services;
using SanoaAPI.Benutzer.Models;

namespace SanoaAPI.Benutzer.Avatar;

public class BenutzerAvatarService : IBenutzerAvatarService
{
    private readonly string _avatarBildPfad;
    
    public BenutzerAvatarService(IOptionsSnapshot<AvatarOptions> avatarBildPfad)
    {
        _avatarBildPfad = avatarBildPfad.Value.AvatarFolderPath;
    }
    
    public void SpeicherBildImOrdner(IFormFile file)
    {
        Directory.CreateDirectory(_avatarBildPfad);
        
        KopiereDateiZumStream(file);
    }

    private void KopiereDateiZumStream(IFormFile file)
    {
        using (var stream = new FileStream(Path.Combine(_avatarBildPfad, file.FileName), FileMode.Create))
        {
            file.CopyTo(stream);
        };
    }
} 