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
                .EmailAddress()
                .WithMessage("Not an email !!");
        }
    }
}