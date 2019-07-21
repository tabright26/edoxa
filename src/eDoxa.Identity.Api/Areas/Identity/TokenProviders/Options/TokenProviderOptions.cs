// Filename: TokenProviderOptions.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Identity.Api.Areas.Identity.TokenProviders.Options
{
    public class TokenProviderOptions
    {
        public AuthenticatorTokenProviderOptions Authenticator { get; set; } = new AuthenticatorTokenProviderOptions();

        public ChangeEmailTokenProviderOptions ChangeEmail { get; set; } = new ChangeEmailTokenProviderOptions();

        public ChangeEmailTokenProviderOptions ChangePhoneNumber { get; set; } = new ChangeEmailTokenProviderOptions();

        public EmailConfirmationTokenProviderOptions EmailConfirmation { get; set; } = new EmailConfirmationTokenProviderOptions();

        public PasswordResetTokenProviderOptions PasswordReset { get; set; } = new PasswordResetTokenProviderOptions();
    }
}
