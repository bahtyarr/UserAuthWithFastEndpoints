using AuthProjects.API.Endpoints.Users.CRUD.Models;
using AuthProjects.Core.Repositories;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.CRUD
{
    public class DeleteUserEndpoint : EndpointWithoutRequest<UserRequest>
    {
        #region Properties

        private readonly IUserRepository _userRepository;

        #endregion Properties

        #region Constructors

        public DeleteUserEndpoint(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion Constructors

        public override void Configure()
        {
            Delete("/users/{id}");
            Policies("AdminOnly");
            AuthSchemes("Bearer");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var id = Route<int>("id");
            var user = await _userRepository.GetByIdAsync(id);
            if (user is not null)
            {
                await _userRepository.DeleteAsync(user);
                await SendNoContentAsync();
            }
            else
            {
                await SendNotFoundAsync();
            }
        }
    }
}