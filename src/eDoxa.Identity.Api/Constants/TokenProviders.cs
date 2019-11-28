// Filename: CustomTokenProviders.cs
// Date Created: 2019-07-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Constants
{
    public static class TokenProviders
    {
        public const string Authenticator = nameof(Authenticator);
        public const string ChangeEmail = nameof(ChangeEmail);
        public const string ChangePhoneNumber = nameof(ChangePhoneNumber);
        public const string EmailConfirmation = nameof(EmailConfirmation);
        public const string PasswordReset = nameof(PasswordReset);
    }
}
