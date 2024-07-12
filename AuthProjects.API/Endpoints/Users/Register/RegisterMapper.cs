using AuthProjects.API.Endpoints.Users.CRUD.Models;
using AuthProjects.Core.Domains;
using FastEndpoints;

namespace AuthProjects.API.Endpoints.Users.Register
{
    public class RegisterMapper : Mapper<RegisterRequest, UserResponse, User>
    {
        public override User ToEntity(RegisterRequest r) => new()
        {
            FirstName = r.FirstName,
            LastName = r.LastName,
            Email = r.Email,
            Username = r.Username,
            Password = r.Password,
            Role = r.Role,
        };
    }
}