using AuthProjects.Infrastructures.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthProjects.Infrastructures
{
    public static class ServicesScoped
    {
        public static void AddRepositoriesScoped(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        }
    }
}