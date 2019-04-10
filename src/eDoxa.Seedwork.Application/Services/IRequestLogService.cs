// Filename: IRequestLogService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Services
{
    public interface IRequestLogService
    {
        //TODO: This must be implemented before eDoxa v.3 (Release 1)
        Task CreateAsync(HttpContext httpContext /*, object request, object response*/);
    }
}