using AuthProjects.API.Endpoints.Users.UserProfile;
using AuthProjects.API.Models.Users.UserProfile;

namespace AuthProjects.API.Models.Users.UserList
{
    public class UserListResponse
    {
        public List<UserProfileResponse> Data { get; set; }
    }
}