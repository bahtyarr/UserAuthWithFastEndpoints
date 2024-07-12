using AuthProjects.API.Endpoints.Users.UserProfile;

namespace AuthProjects.API.Endpoints.Users.UserList
{
    public class UserListResponse
    {
        public List<UserProfileResponse> Data { get; set; }
    }
}