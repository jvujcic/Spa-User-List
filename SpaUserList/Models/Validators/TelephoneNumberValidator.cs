using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace SpaUserList.Models.Validators
{
    public class TelephoneNumberValidator : AbstractValidator<TelephoneNumber>
    {
        public TelephoneNumberValidator()
        {
            RuleFor(tel => tel.Number)
                .Matches(@"^\+?[\d]{3,15}$")
                .WithMessage("Telephone number in wrong format !!");
        }
    }
}