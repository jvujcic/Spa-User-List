using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace SpaUserList.Models.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .WithMessage("User name is empty")
                .NotNull();
            RuleFor(user => user.Surname)
                .NotEmpty()
                .WithMessage("User surname is empty")
                .NotNull();
            RuleFor(user => user.Emails)
                .Must((user, emails) => { return IsEmailUsed(emails, user.UserId); })
                .WithMessage("Email already used");
        }

        private bool IsEmailUsed(HashSet<Email> emails, int id)
        {
            using(var db = new UserListDbContext())
            {
                foreach(Email email in emails)
                {
                    Email emailInDb = db.Emails.SingleOrDefault(e => e.EmailAddress == email.EmailAddress);
                    if(emailInDb != null && (id == -1 || emailInDb.UserId != id))
                        return false;
                }
            }
            return true;
        }
    }
}