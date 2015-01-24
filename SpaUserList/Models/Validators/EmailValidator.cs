using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace SpaUserList.Models.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(email => email.EmailAddress)
                .Matches(@"^[a-z0-9!#$%&'*+\/=?^_`{|}~.-]+@[a-z0-9]([a-z0-9-]*[a-z0-9])?(\.[a-z0-9]([a-z0-9-]*[a-z0-9])?)*$")
                .WithMessage("Not an email !!");
        }
    }
}