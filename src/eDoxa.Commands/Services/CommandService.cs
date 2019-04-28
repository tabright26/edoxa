// Filename: CommandService.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Commands.Exceptions;
using eDoxa.Commands.Infrastructure;
using eDoxa.Commands.Infrastructure.Repositories;
using eDoxa.Seedwork.Domain.Constants;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Commands.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;

        public CommandService(ICommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task LogEntryAsync(HttpContext context)
        {
            var idempotencyKey = context.Request?.Headers[CustomHeaderNames.IdempotencyKey];

            if (_commandRepository.IdempotencyKeyExists(idempotencyKey))
            {
                throw new IdempotencyException(idempotencyKey);
            }

            var logEntry = new CommandLogEntry(context, idempotencyKey);

            _commandRepository.Create(logEntry);

            await _commandRepository.UnitOfWork.CommitAsync();
        }
    }
}