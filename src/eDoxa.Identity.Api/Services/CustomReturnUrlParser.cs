﻿// Filename: ReturnUrlParser.cs
// Date Created: 2020-01-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure;

using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;

using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace eDoxa.Identity.Api.Services
{
    public class CustomReturnUrlParser : IReturnUrlParser
    {
        private readonly IAuthorizeRequestValidator _validator;
        private readonly IUserSession _userSession;
        private readonly ILogger _logger;
        private readonly IOptions<IdentityAppSettings> _options;

        public CustomReturnUrlParser(IAuthorizeRequestValidator validator, IUserSession userSession, ILogger<CustomReturnUrlParser> logger, IOptionsSnapshot<IdentityAppSettings> options)
        {
            _validator = validator;
            _userSession = userSession;
            _logger = logger;
            _options = options;
        }

        public IdentityAppSettings Options => _options.Value;

        public async Task<AuthorizationRequest?> ParseAsync(string returnUrl)
        {
            if (this.IsValidReturnUrl(returnUrl))
            {
                var parameters = returnUrl.ReadQueryStringAsNameValueCollection();
                var user = await _userSession.GetUserAsync();
                var result = await _validator.ValidateAsync(parameters, user);

                if (!result.IsError)
                {
                    _logger.LogTrace("AuthorizationRequest being returned");

                    return result.ValidatedRequest.ToAuthorizationRequest();
                }
            }

            _logger.LogTrace("No AuthorizationRequest being returned");

            return null;
        }

        public bool IsValidReturnUrl(string returnUrl)
        {
            // had to add returnUrl.StartsWith("http://localhost:5000")
            // because when UI and API are not on the same host, the URL is not local
            // the condition here should be changed to either use configuration or just match domain
            if (returnUrl.IsLocalUrl() || returnUrl.StartsWith(Options.Authority.ExternalUrl))
            {
                var index = returnUrl.IndexOf('?');

                if (index >= 0)
                {
                    returnUrl = returnUrl.Substring(0, index);
                }

                if (returnUrl.EndsWith(ProtocolRoutePaths.Authorize, StringComparison.Ordinal) ||
                    returnUrl.EndsWith(ProtocolRoutePaths.AuthorizeCallback, StringComparison.Ordinal))
                {
                    _logger.LogTrace("returnUrl is valid");

                    return true;
                }
            }

            _logger.LogTrace("returnUrl is not valid");

            return false;
        }

        public static class ProtocolRoutePaths
        {
            public const string Authorize = "connect/authorize";
            public const string AuthorizeCallback = Authorize + "/callback";
        }
    }

    internal static class Extensions
    {
        //[DebuggerStepThrough]
        public static NameValueCollection ReadQueryStringAsNameValueCollection(this string url)
        {
            if (url != null)
            {
                var idx = url.IndexOf('?');

                if (idx >= 0)
                {
                    url = url.Substring(idx + 1);
                }

                var query = QueryHelpers.ParseNullableQuery(url);

                if (query != null)
                {
                    return query.AsNameValueCollection();
                }
            }

            return new NameValueCollection();
        }

        [DebuggerStepThrough]
        public static bool IsLocalUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            // Allows "/" or "/foo" but not "//" or "/\".
            if (url[0] == '/')
            {
                // url is exactly "/"
                if (url.Length == 1)
                {
                    return true;
                }

                // url doesn't start with "//" or "/\"
                if (url[1] != '/' && url[1] != '\\')
                {
                    return true;
                }

                return false;
            }

            // Allows "~/" or "~/foo" but not "~//" or "~/\".
            if (url[0] == '~' && url.Length > 1 && url[1] == '/')
            {
                // url is exactly "~/"
                if (url.Length == 2)
                {
                    return true;
                }

                // url doesn't start with "~//" or "~/\"
                if (url[2] != '/' && url[2] != '\\')
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        [DebuggerStepThrough]
        internal static AuthorizationRequest ToAuthorizationRequest(this ValidatedAuthorizeRequest request)
        {
            var authRequest = new AuthorizationRequest
            {
                ClientId = request.ClientId,
                RedirectUri = request.RedirectUri,
                DisplayMode = request.DisplayMode,
                UiLocales = request.UiLocales,
                IdP = request.GetIdP(),
                Tenant = request.GetTenant(),
                LoginHint = request.LoginHint,
                PromptMode = request.PromptMode,
                AcrValues = request.GetAcrValues(),
                ScopesRequested = request.RequestedScopes
            };

            authRequest.Parameters.Add(request.Raw);

            return authRequest;
        }

        [DebuggerStepThrough]
        public static NameValueCollection AsNameValueCollection(this IDictionary<string, StringValues> collection)
        {
            var nv = new NameValueCollection();

            foreach (var field in collection)
            {
                nv.Add(field.Key, field.Value.First());
            }

            return nv;
        }
    }
}
