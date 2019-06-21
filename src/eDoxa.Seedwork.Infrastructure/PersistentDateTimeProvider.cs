using System;

using eDoxa.Seedwork.Common;

namespace eDoxa.Seedwork.Infrastructure
{
    public sealed class PersistentDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime _dateTime;

        public PersistentDateTimeProvider(DateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public DateTime DateTime => _dateTime;
    }
}
