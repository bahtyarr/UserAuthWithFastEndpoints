using FastEndpoints;
using FluentValidation;

namespace AuthProjects.API.Endpoints.Users.Login
{
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