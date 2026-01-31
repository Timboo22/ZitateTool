using Microsoft.EntityFrameworkCore;

namespace SanoaAPI.Extensions;

public static class WebapplicationExtensions {
    public static WebApplication RunOutstandingMigrations(this WebApplication webApplication) {
        using var scope = webApplication.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<ContextDb>().Database.Migrate();
        return webApplication;
    }
}