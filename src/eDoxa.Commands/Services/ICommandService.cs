// Filename: ICommandService.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Commands.Services
{
    public interface ICommandService
    {
        Task LogEntryAsync(HttpContext context);
    }
}