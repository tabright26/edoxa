// Filename: GameManagerExtensions.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Areas.Identity.ViewModels;
using eDoxa.Identity.Api.Infrastructure.Models;

namespace eDoxa.Identity.Api.Areas.Identity.Extensions
{
    public static class GameManagerExtensions
    {
        // TODO: Mapper profile.
        public static async Task<IList<GameViewModel>> GetGameViewModelsAsync(this CustomUserManager userManager, User user)
        {
            var games = await userManager.GetGamesAsync(user);

            return Game.GetEnumerations()
                .Where(game => game.IsDisplayed)
                .Select(
                    game => new GameViewModel
                    {
                        Name = game.Name,
                        IsLinked = games.SingleOrDefault(userGame => Game.FromValue(userGame.Value).Name == game.Name) != null,
                        IsSupported = game.IsSupported
                    }
                )
                .OrderBy(gameViewModel => gameViewModel.Name)
                .ThenBy(gameViewModel => gameViewModel.IsSupported)
                .ToList();
        }
    }
}
