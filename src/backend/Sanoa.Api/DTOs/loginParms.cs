namespace SanoaAPI.DTOs;

public record loginParms()
{
    public required string username { get; init; } 
    public required string password { get; init; }
}