// Filename: CommandService.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Commands.Abstractions;
using eDoxa.Commands.Infrastructure;
using eDoxa.Commands.Infrastructure.Repositories;
using eDoxa.Security;
using eDoxa.Seedwork.Infrastructure.Exceptions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace eDoxa.Commands.Services
{
    public class CommandService : ICommandService
    {
        private readonly ICommandRepository _commandRepository;

        public CommandService(ICommandRepository commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task LogEntryAsync(ICommand<IActionResult> command, IActionResult result, HttpContext context)
        {
            var idempotencyKey = context.Request.Headers[CustomHeaderNames.IdempotencyKey];

            var key = idempotencyKey != StringValues.Empty ? Guid.Parse(idempotencyKey) : Guid.Empty;

            if (_commandRepository.IdempotencyKeyExists(key))
            {
                throw new IdempotencyException(key);
            }

            var logEntry = new CommandLogEntry(command, result, context, key);

            _commandRepository.Create(logEntry);

            await _commandRepository.UnitOfWork.CommitAsync();
        }
    }
}