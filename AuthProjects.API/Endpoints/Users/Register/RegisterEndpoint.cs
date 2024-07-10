using AuthProjects.Infrastructures;
using AuthProjects.Infrastructures.Domain;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace AuthProjects.API.Endpoints.Users.Register
{
    public class RegisterEndpoint : Endpoint<RegisterRequest>
    {
        #region Properties
        private readonly CoreDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        #endregion Properties

        #region  Constructors

        public RegisterEndpoint(CoreDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        #endregion  Constructors

        public override void Configure()
        {
            Post("/register");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            var user = new User
            {
                Username = req.Username,
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Role = req.Role,
            };

            user.Password = _passwordHasher.HashPassword(user, req.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync(ct);

            await SendOkAsync(ct);
        }

    }

}