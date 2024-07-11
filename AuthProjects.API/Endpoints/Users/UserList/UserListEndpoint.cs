using AuthProjects.API.Models.Users.UserList;
using AuthProjects.API.Models.Users.UserProfile;
using AuthProjects.Core.Repositories;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.UserList
{
    public class UserListEndpoint : EndpointWithoutRequest<UserListResponse>
    {
        private readonly IUserRepository _userRepository;

        public UserListEndpoint(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override void Configure()
        {
            Get("/users");
            Policies("AdminOnly");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var users = await _userRepository.GetAllAsync();

            var response = new UserListResponse { Data = new List<UserProfileResponse>() };

            if (users.Any())
            {
                response.Data.AddRange(users.Select(user => new UserProfileResponse
                {
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role,
                }));
            }

            await SendAsync(response, cancellation: ct);
        }
    }
}