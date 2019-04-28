// Filename: LogEntry.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Constants;

using Microsoft.AspNetCore.Http;

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

        protected LogEntry(HttpContext httpContext, string idempotencyKey = null) : this()
        {
            _id = Guid.Parse(httpContext.Response.Headers[CustomHeaderNames.RequestId]);
            _date = DateTime.Parse(httpContext.Response.Headers[CustomHeaderNames.RequestDate]);
            _version = httpContext.Response?.Headers[CustomHeaderNames.Version];
            _origin = httpContext.Request?.Headers[CustomHeaderNames.Origin];
            _method = httpContext.Request?.Method;
            _url = httpContext.Request?.Path.Value.ToLower();
            _localIpAddress = httpContext.Connection?.LocalIpAddress?.MapToIPv4().ToString();
            _remoteIpAddress = httpContext.Connection?.RemoteIpAddress?.MapToIPv4().ToString();
            _idempotencyKey = idempotencyKey != null ? Guid.Parse(idempotencyKey) : (Guid?) null;
        }

        private LogEntry()
        {
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
    }
}