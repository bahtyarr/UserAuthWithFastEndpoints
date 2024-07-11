using AuthProjects.Core.Constants;
using FastEndpoints;
using FluentValidation;

namespace AuthProjects.API.Models.Users.Register
{
    public class RegisterValidator : Validator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("user name is required!")
            .MinimumLength(5)
            .WithMessage("user name is too short!");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("pasword is required!")
            .MinimumLength(5)
            .WithMessage("password is too short!"); ;

            RuleFor(x => x.Email).NotEmpty().EmailAddress();

            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("first name is required!")
            .MinimumLength(5)
            .WithMessage("first is too short!");

            RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("last name is required!");

            RuleFor(x => x.Role)
            .NotEmpty()
            .WithMessage("roles is required!")
            .Must(role => RoleConstant.roles.Contains(role))
            .WithMessage("roles is not matches with any list");
        }

    }
}