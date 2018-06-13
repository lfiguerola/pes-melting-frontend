using FluentValidation;
using FluentValidation.Attributes;
using MeltingApp.Models;

namespace MeltingApp.Validators
{
    [Validator(typeof(UserValidation))]
    class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(x => x.username).NotNull().Length(3, 20).Matches("^[0-9a-zA-Z]+$");
            RuleFor(x => x.password).NotNull().Length(8, 64);
            RuleFor(x => x.email).NotNull().EmailAddress().WithMessage("Invalid mail format");
        }
    }
}
