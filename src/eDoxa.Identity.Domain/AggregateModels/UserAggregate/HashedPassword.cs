// Filename: SecuredPassword.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class HashedPassword : Password
    {
        public HashedPassword(string hash) : base(hash)
        {
        }
    }
}
