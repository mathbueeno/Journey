using FluentValidation;
using Journey.Communication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Journey.Application.UseCases.Trips.Activities.Register
{
    public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        public RegisterActivityValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .WithMessage("Name must not be");
        }
    }
}
