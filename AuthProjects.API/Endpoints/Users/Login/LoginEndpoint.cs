using AuthProjects.API.Utility;
using AuthProjects.Core.Domains;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using AuthProjects.Core.Repositories;

namespace AuthProjects.API.Endpoints.Users.Login
{
    public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
    {
        #region Properties

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;

        #endregion Properties

        public LoginEndpoint(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
        {
            _userRepository = userRepository;
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
            var user = await _userRepository.ReadAsync(item => item.Username == req.Username);
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