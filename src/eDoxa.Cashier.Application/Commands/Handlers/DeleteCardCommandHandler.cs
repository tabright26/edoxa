// Filename: DeleteCardCommandHandler.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Commands.Abstractions.Handlers;
using eDoxa.Security;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Mvc;

using Stripe;

namespace eDoxa.Cashier.Application.Commands.Handlers
{
    internal sealed class DeleteCardCommandHandler : ICommandHandler<DeleteCardCommand, IActionResult>
    {
        private readonly CardService _cardService;
        private readonly IUserInfoService _userInfoService;

        public DeleteCardCommandHandler(IUserInfoService userInfoService, CardService cardService)
        {
            _userInfoService = userInfoService;
            _cardService = cardService;
        }

        [ItemNotNull]
        public async Task<IActionResult> Handle([NotNull] DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var customerId = _userInfoService.CustomerId.SingleOrDefault();

            if (customerId == null)
            {
                return new NotFoundObjectResult("Stripe CustomerId not found.");
            }

            await _cardService.DeleteAsync(customerId, command.CardId.ToString(), cancellationToken: cancellationToken);

            return new OkResult();
        }
    }
}