using AuthProjects.API.Utility;
using AuthProjects.Infrastructures;
using AuthProjects.Core.Domain;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthProjects.API.Endpoints.Users.Login
{
    public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        #region Properties

        private readonly CoreDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        #endregion Properties

        public LoginEndpoint(CoreDbContext context, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();
        }

        public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == req.Username, ct);
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.Password, req.Password) != PasswordVerificationResult.Success)
            {
                await SendUnauthorizedAsync(ct);
                return;
            }

            var token = TokenGenerator.GenerateJwtToken(user, _configuration);
            var response = new LoginResponse { Token = token };

            await SendAsync(response, cancellation: ct);
        }
    }
}