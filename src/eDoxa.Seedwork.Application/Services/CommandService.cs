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

using eDoxa.Seedwork.Application.Exceptions;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Constants;
using eDoxa.Seedwork.Infrastructure.Repositories;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Services
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

            var logEntry = new LogEntry(context, idempotencyKey);

            _commandRepository.Create(logEntry);

            await _commandRepository.UnitOfWork.CommitAsync();
        }
    }
}