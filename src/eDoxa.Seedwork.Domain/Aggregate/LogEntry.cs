// Filename: LogEntry.cs
// Date Created: 2019-04-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Security;
using eDoxa.Versioning;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public class LogEntry : IAggregateRoot
    {
        private DateTime _date;
        private Guid _id;
        private Guid? _idempotencyKey;
        private string _localIpAddress;
        private string _method;
        private string _origin;
        private string _remoteIpAddress;
        private string _requestBody;
        private string _requestType;
        private string _responseBody;
        private string _responseType;
        private string _url;
        private string _version;

        protected LogEntry(HttpContext context, Guid idempotencyKey) : this()
        {
            this.SetVersion(context);
            _origin = context.Request?.Headers[HeaderNames.Origin];
            _method = context.Request?.Method;
            _url = context.Request?.Path.Value.ToLower();
            _localIpAddress = context.Connection?.LocalIpAddress?.MapToIPv4().ToString();
            _remoteIpAddress = context.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
            _idempotencyKey = idempotencyKey != Guid.Empty ? idempotencyKey : (Guid?) null;
        }

        private LogEntry()
        {
            _id = Guid.NewGuid();
            _date = DateTime.UtcNow;
            _requestType = null;
            _requestBody = null;
            _responseType = null;
            _responseBody = null;
        }

        public Guid Id => _id;

        public DateTime Date => _date;

        public string Version => _version;

        public string Origin => _origin;

        public string Method => _method;

        public string Url => _url;

        public string LocalIpAddress => _localIpAddress;

        public string RemoteIpAddress => _remoteIpAddress;

        public string RequestBody => _requestBody;

        public string RequestType => _requestType;

        public string ResponseBody => _responseBody;

        public string ResponseType => _responseType;

        public Guid? IdempotencyKey => _idempotencyKey;

        protected void SetRequest(object request)
        {
            var json = JsonConvert.SerializeObject(request);

            _requestBody = json != "{}" ? json : null;

            _requestType = request.GetType().FullName;
        }

        protected void SetResponse(object response)
        {
            var json = JsonConvert.SerializeObject(response);

            _responseBody = json != "{}" ? json : null;

            _responseType = response.GetType().FullName;
        }

        private void SetVersion(HttpContext context)
        {
            var defaultVersion = new DefaultApiVersion();

            var version = context.Request.Headers[CustomHeaderNames.Version];

            _version = version != StringValues.Empty ? version.ToString() : defaultVersion.ToString();
        }
    }
}