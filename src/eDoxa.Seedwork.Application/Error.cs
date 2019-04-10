// Filename: Error.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.Application
{
    public class Error
    {
        public int HResult { get; set; }
        public string HelpLink { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string StackTrace { get; set; }
        public string TargetSite { get; set; }
        public IDictionary Data { get; set; }

        internal void FromException(Exception exception)
        {
            HResult = exception.HResult;
            HelpLink = exception.HelpLink;
            Message = exception.Message;
            Source = exception.Source;
            StackTrace = exception.StackTrace;
            TargetSite = exception.TargetSite?.ToString();
            Data = exception.Data;
        }

        internal string Serialize()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    error = this
                }
            );
        }
    }
}