using AuthProjects.Core.Domains;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using AuthProjects.Core.Repositories;
using AuthProjects.Core.Services;
using AuthProjects.Core.Constants;
using AuthProjects.API.Endpoints.Users.CRUD.Models;

namespace AuthProjects.API.Endpoints.Users.Register
{
    public class RegisterEndpoint : Endpoint<RegisterRequest, UserResponse, RegisterMapper>
    {
        #region Properties

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IEmailService _emailService;

        #endregion Properties

        #region  Constructors

        public RegisterEndpoint(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _emailService = emailService;
        }

        #endregion  Constructors

        public override void Configure()
        {
            Post("/register");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            var readUser = await _userRepository.GetUserByEmail(req.Email);
            if (readUser is not null)
            {
                AddError($"user with email {req.Email} is already exists!");
                await SendErrorsAsync(cancellation: ct);
                return;
            }

            User user = Map.ToEntity(req);
            user.Password = _passwordHasher.HashPassword(user, req.Password);

            await _userRepository.AddAsync(user);

            var emailSubject = "Registration Completed!";
            var emailFileName = EmailFileNameConstant.RegisterEmail;

            var placeholders = new Dictionary<string, string>
            {
                { "TITLE", emailSubject},
                { "EMAIL", user.Email },
                { "ROLE", user.Role }
            };

            await _emailService.SendEmailAsync(user.Email, emailSubject, emailFileName, placeholders);

            await SendOkAsync(ct);
        }

    }

}