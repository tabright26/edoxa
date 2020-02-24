// Filename: RequestBodyInitializer.cs
// Date Created: 2020-02-24
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.IO;

using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Http;

namespace eDoxa.Seedwork.Application.ApplicationInsights.Initializers
{
    public sealed class RequestBodyInitializer : ITelemetryInitializer
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestBodyInitializer(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Initialize(ITelemetry telemetry)
        {
            if (telemetry is RequestTelemetry requestTelemetry)
            {
                if ((_httpContextAccessor.HttpContext.Request.Method == HttpMethods.Post ||
                     _httpContextAccessor.HttpContext.Request.Method == HttpMethods.Put) &&
                    _httpContextAccessor.HttpContext.Request.Body.CanRead)
                {
                    const string requestBody = "Request body";

                    if (requestTelemetry.Properties.ContainsKey(requestBody))
                    {
                        return;
                    }

                    //Allows re-usage of the stream
                    _httpContextAccessor.HttpContext.Request.EnableBuffering();

                    var stream = new StreamReader(_httpContextAccessor.HttpContext.Request.Body);
                    var body = stream.ReadToEnd();

                    //Reset the stream so data is not lost
                    _httpContextAccessor.HttpContext.Request.Body.Position = 0;
                    requestTelemetry.Properties.Add(requestBody, body);
                }
            }
        }
    }
}
