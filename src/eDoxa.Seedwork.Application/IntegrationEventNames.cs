// Filename: IntegrationEventNames.cs
// Date Created: 2019-08-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Application
{
    public static class IntegrationEventNames
    {
        // Integration events.
        public const string EmailSent = "email.sent";

        // Cashier integration events.
        public const string UserAccountDeposit = "user.account.deposit";
        public const string UserAccountWithdrawal = "user.account.withdrawal";
        public const string UserTransactionSucceded = "user.transaction.succeded";
        public const string UserTransactionFailed = "user.transaction.failed";

        // Identity integration events.
        public const string RoleCreated = "role.created";
        public const string RoleDeleted = "role.deleted";
        public const string RoleClaimAdded = "role.claim.added";
        public const string RoleClaimRemoved = "role.claim.removed";
        public const string UserCreated = "user.created";
        public const string UserClaimsAdded = "user.claims.added";
        public const string UserClaimsRemoved = "user.claims.removed";
        public const string UserClaimsReplaced = "user.claims.replaced";
        public const string UserRoleAdded = "user.role.added";
        public const string UserRoleRemoved = "user.role.removed";
    }
}
