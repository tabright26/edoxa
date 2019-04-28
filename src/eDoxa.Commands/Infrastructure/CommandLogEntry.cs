// Filename: CommandLogEntry.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Commands.Abstractions;
using eDoxa.Seedwork.Domain.Aggregate;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eDoxa.Commands.Infrastructure
{
    public sealed class CommandLogEntry : LogEntry
    {
        public CommandLogEntry(
            ICommand<IActionResult> command,
            IActionResult result,
            HttpContext httpContext,
            string idempotencyKey = null) : base(
            httpContext,
            idempotencyKey
        )
        {
            this.SetRequest(command);
            this.SetResponse(((ObjectResult) result).Value);
        }
    }
}