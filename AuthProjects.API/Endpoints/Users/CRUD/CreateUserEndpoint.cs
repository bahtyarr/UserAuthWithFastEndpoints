using AuthProjects.API.Endpoints.Users.CRUD.Models;
using AuthProjects.Core.Domains;
using AuthProjects.Core.Repositories;
using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace AuthProjects.API.Endpoints.Users.CRUD
{
    public class CreateUserEndpoint : Endpoint<UserRequest, UserResponse, UserMapper>
    {
        #region Properties

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        #endregion Properties

        #region Constructors

        public CreateUserEndpoint(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        #endregion Constructors

        public override void Configure()
        {
            Post("/users");
            Policies("AdminOnly");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(UserRequest req, CancellationToken ct)
        {
            User user = Map.ToEntity(req);

            user.Password = _passwordHasher.HashPassword(user, req.Password);

            await _userRepository.AddAsync(user);
            await SendOkAsync(ct);
        }
    }
}