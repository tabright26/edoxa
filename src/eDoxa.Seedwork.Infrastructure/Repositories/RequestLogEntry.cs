// Filename: RequestLogEntry.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Infrastructure.Constants;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Infrastructure.Repositories
{
    public class RequestLogEntry : IAggregateRoot
    {
        //TODO: This must be implemented before eDoxa v.3 (Release 1)
        public RequestLogEntry([CanBeNull] HttpContext httpContext, /*object request, object response,*/ string idempotencyKey = null) : this()
        {
            if (httpContext != null)
            {
                Id = Guid.Parse(httpContext.Response.Headers[CustomHeaderNames.RequestId]);
                Time = DateTime.Parse(httpContext.Response.Headers[CustomHeaderNames.RequestDate]);
                Type = RequestLogEntryType.External;
                Version = httpContext.Response.Headers[CustomHeaderNames.EdoxaVersion];
                Method = httpContext.Request?.Method;
                Url = httpContext.Request?.Path.Value.ToLower();
                LocalIpAddress = httpContext.Connection?.LocalIpAddress?.MapToIPv4().ToString();
                RemoteIpAddress = httpContext.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
                Origin = httpContext.Request?.Headers[CustomHeaderNames.Origin];

                if (idempotencyKey != null)
                {
                    IdempotencyKey = Guid.Parse(idempotencyKey);
                }

                //TODO: This must be implemented before eDoxa v.3 (Release 1)
                //this.RequestType = request.GetType().FullName;
                //var resquestBody = JsonConvert.SerializeObject(request);
                //this.RequestBody = resquestBody != "{}" ? resquestBody : null;
                //this.ResponseType = response.GetType().FullName;
                //var responseBody = JsonConvert.SerializeObject(response);
                //this.ResponseBody = responseBody != "{}" ? responseBody : null;
            }
        }

        private RequestLogEntry()
        {
            Id = Guid.NewGuid();
            Time = DateTime.UtcNow;
            Type = RequestLogEntryType.Internal;
            Version = null;
            Method = null;
            Url = null;
            LocalIpAddress = null;
            RemoteIpAddress = null;
            Origin = null;
            IdempotencyKey = null;
        }

        public Guid Id { get; private set; }
        public DateTime Time { get; private set; }
        public RequestLogEntryType Type { get; private set; }
        public string Method { get; private set; }
        public string Url { get; private set; }
        public string LocalIpAddress { get; private set; }
        public string RemoteIpAddress { get; private set; }
        public string Version { get; private set; }
        public string Origin { get; private set; }

        public Guid? IdempotencyKey { get; private set; }

        //TODO: This must be implemented before eDoxa v.3 (Release 1)
        //public string RequestBody { get; private set; }
        //TODO: This must be implemented before eDoxa v.3 (Release 1)
        //public string RequestType { get; private set; }
        //TODO: This must be implemented before eDoxa v.3 (Release 1)
        //public string ResponseBody { get; private set; }
        //TODO: This must be implemented before eDoxa v.3 (Release 1)
        //public string ResponseType { get; private set; }
    }
}