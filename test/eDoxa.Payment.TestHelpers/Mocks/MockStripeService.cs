// Filename: MockStripeService.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Domain.Services;

using Moq;

namespace eDoxa.Payment.TestHelpers.Mocks
{
    public sealed class MockStripeService : Mock<IStripeReferenceService>
    {
        //private readonly CustomerFaker _customerFaker = new CustomerFaker();
        //private readonly AccountFaker _accountFaker = new AccountFaker();

        public MockStripeService()
        {
            //this.Setup(
            //        mock => mock.CreateAccountAsync(
            //            It.IsAny<Guid>(),
            //            It.IsAny<string>(),
            //            It.IsAny<string>(),
            //            It.IsAny<string>(),
            //            It.IsAny<int>(),
            //            It.IsAny<int>(),
            //            It.IsAny<int>(),
            //            It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(_accountFaker.FakeAccount().Id);

            //this.Setup(
            //        mock => mock.CreateCustomerAsync(
            //            It.IsAny<Guid>(),
            //            It.IsAny<string>(),
            //            It.IsAny<string>(),
            //            It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(_customerFaker.FakeCustomer().Id);

            //this.Setup(
            //        mock => mock.CreateInvoiceAsync(
            //            It.IsAny<Guid>(),
            //            It.IsAny<string>(),
            //            It.IsAny<string>(),
            //            It.IsAny<long>(),
            //            It.IsAny<CancellationToken>()))
            //    .Returns(Task.CompletedTask);

            //this.Setup(
            //        mock => mock.CreateTransferAsync(
            //            It.IsAny<Guid>(),
            //            It.IsAny<string>(),
            //            It.IsAny<string>(),
            //            It.IsAny<long>(),
            //            It.IsAny<CancellationToken>()))
            //    .Returns(Task.CompletedTask);
        }
    }
}
