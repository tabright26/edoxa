// Filename: PersonalInfoPostRequestValidator.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Areas.Identity.Requests;
using eDoxa.Seedwork.Application.Validations.Extensions;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class PersonalInfoPostRequestValidator : AbstractValidator<PersonalInfoPostRequest>
    {
        public PersonalInfoPostRequestValidator()
        {
            this.RuleFor(request => request.FirstName).NotNull().NotEmpty().Length(2, 16);
            this.RuleFor(request => request.LastName).NotNull().NotEmpty().Length(2, 16);
            //this.Enumeration(request => request.Gender);
            //this.RuleFor(request => request.BirthDate).NotNull().GreaterThanOrEqualTo(DateTime.UtcNow.Date.AddYears(-13));
        }
    }
}
