// Filename: IGatewayHttpClient.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Application.Responses;

using Refit;

namespace eDoxa.Arena.Games.Api.HttpClients
{
    public interface IGatewayHttpClient
    {
        [Post("/games/{game}/api/credential")]
        Task<CredentialResponse> GetCredentialAsync([AliasAs("game")] string game);
    }
}
