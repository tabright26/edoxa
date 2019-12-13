using System;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Newtonsoft.Json;

using Xunit;

namespace eDoxa.Cashier.UnitTests
{
    public class Class1
    {
        [Fact]
        public void Test()
        {
            var dto = new TransactionDto
            {
                Id = new TransactionId(),
                Type = TransactionTypeDto.Charge,
                Status = TransactionStatusDto.Pending,
                Description = "Test",
                Timestamp = DateTime.UtcNow.ToTimestamp(),
                Currency = CurrencyDto.Money,
                Amount = 5000
            };

            var serialize = JsonConvert.SerializeObject(dto);

            var deserializet = JsonConvert.DeserializeObject<TransactionDto>(serialize);

            dto.Should().BeEquivalentTo(deserializet);
        }
    }
}
