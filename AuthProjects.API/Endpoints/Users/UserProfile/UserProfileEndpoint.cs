using System.Security.Claims;
using AuthProjects.Infrastructures;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.UserProfile
{
    public class UserProfileEndpoint : EndpointWithoutRequest<UserProfileResponse>
    {
        private readonly CoreDbContext _context;

        public UserProfileEndpoint(CoreDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/profile");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(userId);

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
                LastName = user.LastName
            };

            await SendAsync(response, cancellation: ct);
        }
    }
}