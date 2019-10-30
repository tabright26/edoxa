// Filename: ValidationDetailsDelegatingHandler.cs
// Date Created: 2019-10-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;

namespace eDoxa.Seedwork.Application.DelegatingHandlers
{
    public sealed class ValidationDetailsDelegatingHandler : DelegatingHandler
    {
        //protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    try
        //    {
        //        return await base.SendAsync(request, cancellationToken);
        //    }
        //    catch (ValidationApiException exception)
        //    {
        //        // handle validation here by using validationException.Content, 
        //        // which is type of ProblemDetails according to RFC 7807
        //        return  exception.Content;
        //    }
        //    catch (ApiException exception)
        //    {
        //        return exception.RequestMessage;
        //    }
        //}
    }
}
