﻿// Filename: ErrorDTO.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application.Models
{
    [JsonObject]
    public class Error
    {
        public Error(Exception exception)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            Message = exception.Message;
        }

        public Error()
        {
            StatusCode = StatusCodes.Status500InternalServerError;
            Message = "Internal Server Error";
        }

        [JsonProperty("statusCode")] public int StatusCode { get; }

        [JsonProperty("message")] public string Message { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}