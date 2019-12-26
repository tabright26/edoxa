// Filename: ChangeDoxatagRequestValidatorTest.Data.cs
// Date Created: 2019-11-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Application.ErrorDescribers;

using Xunit;

namespace eDoxa.Identity.UnitTests.Application.Validators
{
    public sealed partial class ChangeDoxatagRequestValidatorTest
    {
        public static TheoryData<string> ValidNameData =>
            new TheoryData<string>
            {
                "DoxatagName",
                "Doxa_Tag_Name",
                "aaaaaaaaaaaaaaaa",
                "DoxatagName1",
                "DoxatagName_"
            };

        public static TheoryData<string, string> InvalidNameData =>
            new TheoryData<string, string>
            {
                {"", DoxatagErrorDescriber.Required()},
                {"D", DoxatagErrorDescriber.Length()},
                {"aaaaaaaaaaaaaaaaa", DoxatagErrorDescriber.Length()},
                {"@DoxatagName", DoxatagErrorDescriber.Invalid()},
                {"_DoxatagName", DoxatagErrorDescriber.InvalidUnderscore()}
            };
    }
}
