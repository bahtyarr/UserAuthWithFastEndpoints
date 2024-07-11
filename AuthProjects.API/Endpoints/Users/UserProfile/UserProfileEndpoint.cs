using System.Security.Claims;
using AuthProjects.API.Models.Users.UserProfile;
using AuthProjects.Core.Repositories;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.UserProfile
{
    public class UserProfileEndpoint : EndpointWithoutRequest<UserProfileResponse>
    {
        #region Properties

        private readonly IUserRepository _userRepository;

        #endregion Properties

        public UserProfileEndpoint(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override void Configure()
        {
            Get("/profile");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var response = new UserProfileResponse
            {
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}