using AuthProjects.Core.Domains;
using AuthProjects.Core.Repositories;
using AuthProjects.Core.Services;
using AuthProjects.Infrastructures.Repositories.Users;
using AuthProjects.Infrastructures.Services;
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
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IEmailService, EmailService>();
        }
    }
}