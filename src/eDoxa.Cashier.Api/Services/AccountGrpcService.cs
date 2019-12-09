// Filename: AccountService.cs
// Date Created: 2019-12-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Grpc.Protos;

using Grpc.Core;

namespace eDoxa.Cashier.Api.Services
{
    public sealed class AccountGrpcService : AccountService.AccountServiceBase
    {
        public override async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionRequest request, ServerCallContext context)
        {
            return await base.CreateTransaction(request, context);
        }
    }
}
