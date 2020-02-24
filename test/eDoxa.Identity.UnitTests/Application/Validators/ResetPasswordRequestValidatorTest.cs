// Filename: ResetPasswordRequestValidatorTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Application.Validators;
using eDoxa.Seedwork.Domain.Misc;

using FluentValidation.TestHelper;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed class ResetPasswordRequestValidatorTest
    {
        public static TheoryData<string> ValidUserIds =>
            new TheoryData<string>
            {
                new UserId(),
                Guid.NewGuid().ToString()
            };

        public static TheoryData<string> InvalidUserIds =>
            new TheoryData<string>
            {
                string.Empty
            };

        public static TheoryData<string> ValidPasswords =>
            new TheoryData<string>
            {
                "Testing123!",
                "th1s_Is-a-p4ssw3rd"
            };

        //public static TheoryData<string, string> InvalidPasswords =>
        //    new TheoryData<string, string>
        //    {
        //        {string.Empty, PasswordResetErrorDescriber.PasswordRequired()},
        //        {null, PasswordResetErrorDescriber.PasswordLength()}
        //    };

        [Theory]
        [MemberData(nameof(ValidUserIds))]
        public void Validate_WhenEmailIsValid_ShouldNotHaveValidationErrorFor(string userId)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.UserId, userId);
        }

        [Theory]
        [MemberData(nameof(InvalidUserIds))]
        public void Validate_WhenEmailIsInvalid_ShouldNotHaveValidationErrorFor(string userId)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            validator.ShouldHaveValidationErrorFor(request => request.UserId, userId);
        }

        [Theory]
        [MemberData(nameof(ValidPasswords))]
        public void Validate_WhenPasswordIsValid_ShouldNotHaveValidationErrorFor(string password)
        {
            // Arrange
            var validator = new ResetPasswordRequestValidator();

            // Act - Assert
            validator.ShouldNotHaveValidationErrorFor(request => request.Password, password);
        }

        //[Theory]
        //[MemberData(nameof(InvalidPasswords))]
        //public void Validate_WhenPasswordIsInvalid_ShouldNotHaveValidationErrorFor(string password, string errorMessage)
        //{
        //    // Arrange
        //    var validator = new ResetPasswordRequestValidator();

        //    // Act - Assert
        //    var failures = validator.ShouldHaveValidationErrorFor(request => request.Password, password);

        //    failures.Should().Contain(failure => failure.ErrorMessage == errorMessage);
        //}
    }
}
