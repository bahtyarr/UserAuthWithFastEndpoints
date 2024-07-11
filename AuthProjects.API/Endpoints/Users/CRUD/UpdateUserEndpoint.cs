using AuthProjects.API.Models.Users;
using AuthProjects.Core.Domains;
using AuthProjects.Core.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace AuthProjects.API.Endpoints.Users.CRUD
{
    public class UpdateUserEndpoint : Endpoint<UserRequest>
    {
        #region Properties

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        #endregion Properties

        #region Constructors

        public UpdateUserEndpoint(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        #endregion Constructors

        public override void Configure()
        {
            Put("/users/{id}");
            Policies("AdminOnly");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(UserRequest req, CancellationToken ct)
        {
            var id = Route<int>("id");
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not null)
            {
                user.Username = req.Username;
                user.Password = _passwordHasher.HashPassword(user, req.Password);
                user.FirstName = req.FirstName;
                user.LastName = req.LastName;
                user.Email = req.Email;
                user.Role = req.Role;

                await _userRepository.UpdateAsync(user);
                await SendOkAsync(ct);
            }
            else
            {
                await SendNotFoundAsync();
            }
        }
    }
}