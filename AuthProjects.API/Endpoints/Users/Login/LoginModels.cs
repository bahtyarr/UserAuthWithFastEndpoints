using FastEndpoints;
using FluentValidation;

namespace AuthProjects.API.Endpoints.Users.Login
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }

    public class LoginRequestValidator : Validator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("user name is required!");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("password is required!");
        }
    }
}