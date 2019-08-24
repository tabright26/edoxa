// Filename: PersonalInfoPutRequestValidator.cs
// Date Created: 2019-08-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Areas.Identity.Requests;

using FluentValidation;

namespace eDoxa.Identity.Api.Areas.Identity.Validators
{
    public class PersonalInfoPutRequestValidator : AbstractValidator<PersonalInfoPutRequest>
    {
        public PersonalInfoPutRequestValidator()
        {
            this.RuleFor(request => request.FirstName).NotNull().NotEmpty().Length(2, 16);
        }
    }
}
