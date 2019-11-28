// Filename: InformationsPostRequestValidatorTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.ErrorDescribers;
using eDoxa.Identity.Api.Validators;
using eDoxa.Identity.Requests;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Validators
{
    public sealed class CreateProfileRequestValidatorTest
    {
        public static TheoryData<string> ValidFirstNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R"
            };

        public static TheoryData<string> ValidLastNames =>
            new TheoryData<string>
            {
                "Gabriel",
                "Gabriel-Roy",
                "Gabriel-Roy-R"
            };

        public static TheoryData<string, string> InvalidFirstNames =>
            new TheoryData<string, string>
            {
                {null, InformationsErrorDescriber.FirstNameRequired()},
                {"", InformationsErrorDescriber.FirstNameRequired()},
                {"G", InformationsErrorDescriber.FirstNameLength()},
                {"Gabriel-Roy-Gab-R", InformationsErrorDescriber.FirstNameLength()},
                {"Gab123", InformationsErrorDescriber.FirstNameInvalid()},
                {"Gabriel-Ro_Roy", InformationsErrorDescriber.FirstNameInvalid()},
                {"gabriel-Roy", InformationsErrorDescriber.FirstNameUppercase()},
                {"Gabriel-roy", InformationsErrorDescriber.FirstNameUppercase()}
            };

        public static TheoryData<string, string> InvalidLastNames =>
            new TheoryData<string, string>
            {
                {null, InformationsErrorDescriber.LastNameRequired()},
                {"", InformationsErrorDescriber.LastNameRequired()},
                {"G", InformationsErrorDescriber.LastNameLength()},
                {"Gabriel-Roy-Gab-R", InformationsErrorDescriber.LastNameLength()},
                {"Gab123", InformationsErrorDescriber.LastNameInvalid()},
                {"Gabriel-Ro_Roy", InformationsErrorDescriber.LastNameInvalid()},
                {"gabriel-Roy", InformationsErrorDescriber.LastNameUppercase()},
                {"Gabriel-roy", InformationsErrorDescriber.LastNameUppercase()}
            };

        public static TheoryData<Gender> ValidGenders =>
            new TheoryData<Gender>
            {
                Gender.Male,
                Gender.Female,
                Gender.Other
            };

        public static TheoryData<Gender, string> InvalidGenders =>
            new TheoryData<Gender, string>
            {
                {null, InformationsErrorDescriber.GenderRequired()}
            };

        public static TheoryData<DobRequest> ValidDob =>
            new TheoryData<DobRequest>
            {
                new DobRequest(1995, 8, 4)
            };

        //public static TheoryData<DobRequest, string> InvalidDob
        //{
        //    get
        //    {
        //        var dob = new DateTime();

        //        return new TheoryData<DobRequest, string>
        //        {
        //            {new DobRequest(dob.Year, dob.Month, dob.Day), InformationsErrorDescriber.BirthDateInvalid()}
        //        };
        //    }
        //}

        [Theory]
        [MemberData(nameof(ValidFirstNames))]
        public void Validate_WhenFirstNameIsValid_ShouldNotHaveValidationErrorFor(string firstName)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.FirstName, firstName);
        }

        [Theory]
        [MemberData(nameof(InvalidFirstNames))]
        public void Validate_WhenFirstNameIsInvalid_ShouldHaveValidationErrorFor(string firstName, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.FirstName, firstName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidLastNames))]
        public void Validate_WhenLastNameIsValid_ShouldNotHaveValidationErrorFor(string lastName)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.LastName, lastName);
        }

        [Theory]
        [MemberData(nameof(InvalidLastNames))]
        public void Validate_WhenLastNameIsInvalid_ShouldHaveValidationErrorFor(string lastName, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.LastName, lastName);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidGenders))]
        public void Validate_WhenGenderIsValid_ShouldNotHaveValidationErrorFor(Gender gender)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Gender, gender.Name);
        }

        [Theory(Skip = "TODO")]
        [MemberData(nameof(InvalidGenders))]
        public void Validate_WhenGenderIsInvalid_ShouldHaveValidationErrorFor(Gender gender, string errorMessage)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            var failures = validator.ShouldHaveValidationErrorFor(request => request.Gender, gender.Name);
            failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        }

        [Theory]
        [MemberData(nameof(ValidDob))]
        public void Validate_WhenDobIsValid_ShouldNotHaveValidationErrorFor(DobRequest dob)
        {
            // Arrange
            var validator = new CreateProfileRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Dob, dob);
        }

        //[Theory]
        //[MemberData(nameof(InvalidDob))]
        //public void Validate_WhenDobIsInvalid_ShouldHaveValidationErrorFor(DobRequest dob, string errorMessage)
        //{
        //    // Arrange
        //    var validator = new InformationsPostRequestValidator();

        //    // Act
        //    var failures = validator.ShouldHaveValidationErrorFor(request => request.Dob, dob);

        //    // Assert
        //    failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        //}
    }
}
