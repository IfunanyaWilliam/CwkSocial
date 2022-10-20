using System;
using Cwk.Domain.Aggregates.UserProfileAggregate;
using FluentValidation;

namespace Cwk.Domain.Validators.UserProfileValidators
{
    public class BasicInfoValidator : AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(info => info.FirstName)
                .NotNull().WithMessage("First name is required. It is currently null")
                .MinimumLength(3).WithMessage("First Name must be at least 3 characters long")
                .MaximumLength(50).WithMessage("First Name can contain at most 50 characters long");

            RuleFor(info => info.LastName)
                .NotNull().WithMessage("Last name is required. It is currently null")
                .MinimumLength(3).WithMessage("Last Name must be at least 3 characters long")
                .MaximumLength(50).WithMessage("Last Name can contain at most 50 characters long");

            RuleFor(info => info.EmailAddress)
                .NotNull().WithMessage("Email address is required")
                .EmailAddress().WithMessage("The string provided is not a valid email address format");

            RuleFor(info => info.DateOfBirth)
                .InclusiveBetween(new DateTime(DateTime.Now.AddYears(-90).Ticks),
                            new DateTime(DateTime.Now.AddYears(-18).Ticks))
                                .WithMessage("Age should be between 18 and 90 years");
        }
    }
}

