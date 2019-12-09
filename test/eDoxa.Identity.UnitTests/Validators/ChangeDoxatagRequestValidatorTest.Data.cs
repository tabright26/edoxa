// Filename: ChangeDoxatagRequestValidatorTest.Data.cs
// Date Created: 2019-11-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.ErrorDescribers;

using Xunit;

namespace eDoxa.Identity.UnitTests.Validators
{
    public sealed partial class ChangeDoxatagRequestValidatorTest
    {
        public static TheoryData<string> ValidNameData =>
            new TheoryData<string>
            {
                "DoxatagName",
                "Doxa_Tag_Name",
                "aaaaaaaaaaaaaaaa"
            };

        public static TheoryData<string, string> InvalidNameData =>
            new TheoryData<string, string>
            {
                {null, DoxatagErrorDescriber.Required()},
                {"", DoxatagErrorDescriber.Required()},
                {"D", DoxatagErrorDescriber.Length()},
                {"aaaaaaaaaaaaaaaaa", DoxatagErrorDescriber.Length()},
                {"@DoxatagName", DoxatagErrorDescriber.Invalid()},
                {"DoxatagName1", DoxatagErrorDescriber.Invalid()},
                {"_DoxatagName", DoxatagErrorDescriber.InvalidUnderscore()},
                {"DoxatagName_", DoxatagErrorDescriber.InvalidUnderscore()}
            };
    }
}
