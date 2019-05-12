// Filename: VerifyBankAccountCommandHandler.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Commands.Abstractions.Handlers;

using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class VerifyBankAccountCommandHandler : ICommandHandler<VerifyBankAccountCommand, IActionResult>
    {
        public Task<IActionResult> Handle(VerifyBankAccountCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult<IActionResult>(new BadRequestResult());
        }
    }
}