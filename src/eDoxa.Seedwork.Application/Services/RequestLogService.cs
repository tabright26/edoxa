// Filename: RequestLogService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Exceptions;
using eDoxa.Seedwork.Infrastructure.Constants;
using eDoxa.Seedwork.Infrastructure.Repositories;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.Services
{
    public class RequestLogService : IRequestLogService
    {
        private readonly IRequestLogRepository _requestLogRepository;

        public RequestLogService(IRequestLogRepository requestLogRepository)
        {
            _requestLogRepository = requestLogRepository;
        }

        public async Task CreateAsync([CanBeNull] HttpContext httpContext /*, object request, object response*/)
        {
            var idempotencyKey = httpContext?.Request?.Headers[CustomHeaderNames.IdempotencyKey].FirstOrDefault();

            var requestLog = _requestLogRepository.IdempotencyKeyExists(idempotencyKey) ?
                throw new IdempotentRequestException(idempotencyKey) :

                //TODO: This must be implemented before eDoxa v.3 (Release 1)
                new RequestLogEntry(httpContext, /*request, response,*/ idempotencyKey);

            _requestLogRepository.Create(requestLog);

            await _requestLogRepository.UnitOfWork.CommitAsync();
        }
    }
}