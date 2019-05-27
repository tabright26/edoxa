using System;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Domain
{
    public sealed class ExternalAccount : TypedObject<ExternalAccount, string>
    {
        public ExternalAccount(string externalAccount)
        {
            if (string.IsNullOrWhiteSpace(externalAccount) || !externalAccount.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalAccount));
            }

            Value = externalAccount;
        }

        public ExternalAccount(Guid externalAccount)
        {
            if (externalAccount == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalAccount));
            }

            Value = externalAccount.ToString();
        }

        public static implicit operator ExternalAccount(string externalAccount)
        {
            return new ExternalAccount(externalAccount);
        }

        public static implicit operator ExternalAccount(Guid externalAccount)
        {
            return new ExternalAccount(externalAccount);
        }
    }
}
