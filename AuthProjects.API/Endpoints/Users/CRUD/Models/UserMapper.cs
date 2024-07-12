using AuthProjects.Core.Domains;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.CRUD.Models
{
    public class UserMapper : Mapper<UserRequest, UserResponse, User>
    {
        public override User ToEntity(UserRequest r) => new()
        {
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            Username = r.Username,
            Password = r.Password,
            Role = r.Role,
        };

        public override UserResponse FromEntity(User e) => new()
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Username = e.Username,
            Role = e.Role,
        };
    }
}