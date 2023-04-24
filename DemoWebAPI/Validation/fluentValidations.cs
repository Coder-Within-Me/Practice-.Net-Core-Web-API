using DemoWebAPI.Models;
using FluentValidation;

namespace DemoWebAPI.Validation
{
    public class fluentValidations : AbstractValidator<TestModel>
    {
        public fluentValidations() 
        {
            RuleFor(x => x.Name).NotEmpty();        
        }
    }

    public class UsersfluentValidations : AbstractValidator<Authentication>
    {
        public UsersfluentValidations()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
