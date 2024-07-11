using AuthProjects.API.Models.Users;
using AuthProjects.Core.Repositories;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.CRUD
{
    public class ReadUserEndpoint : EndpointWithoutRequest<UserResponse>
    {
        #region Properties

        private readonly IUserRepository _userRepository;

        #endregion Properties

        #region Constructors

        public ReadUserEndpoint(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        public override void Configure()
        {
            Get("/users/{id}");
            Policies("AdminOnly");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not null)
            {
                await SendAsync(new UserResponse
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                });
            }
            else
            {
                await SendNotFoundAsync();
            }
        }
    }
}