// Filename: IGatewayHttpClient.cs
// Date Created: 2019-10-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;

using Refit;

namespace eDoxa.Arena.Games.Api.Areas.Credentials.RefitClient
{
    public interface IGamesRefitClient
    {
        [Post("/games/{game}/api/authenticate")]
        Task<PlayerId> AuthenticateAsync([AliasAs("game")] string game);
    }
}
