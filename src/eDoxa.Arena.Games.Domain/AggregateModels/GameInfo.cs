// Filename: GameInfo.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Games.Domain.AggregateModels.GameCredentialAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Games.Domain.AggregateModels
{
    public sealed class GameInfo : ValueObject
    {
        public GameInfo(
            Game game,
            string imageName,
            string reactComponent,
            Dictionary<string, ServiceInfo> services
        ) : this(
            game.Name,
            game.DisplayName,
            imageName,
            reactComponent,
            false,
            services)
        {
        }

        private GameInfo(
            GameCredential credential,
            string imageName,
            string reactComponent,
            Dictionary<string, ServiceInfo> services
        ) : this(
            credential.Game,
            imageName,
            reactComponent,
            true,
            services)
        {
        }

        private GameInfo(
            Game game,
            string imageName,
            string reactComponent,
            bool linked,
            Dictionary<string, ServiceInfo> services
        ) : this(
            game.Name,
            game.DisplayName,
            imageName,
            reactComponent,
            linked,
            services)
        {
        }

        private GameInfo(
            string name,
            string displayName,
            string imageName,
            string reactComponent,
            bool linked,
            Dictionary<string, ServiceInfo> services
        )
        {
            Name = name;
            DisplayName = displayName;
            Linked = linked;
            ImageName = imageName;
            ReactComponent = reactComponent;
            Services = services;
        }

        public string Name { get; }

        public string DisplayName { get; }

        public string ImageName { get; }

        public string ReactComponent { get; }

        public bool Linked { get; }

        public IReadOnlyDictionary<string, ServiceInfo> Services { get; }

        public GameInfo TryGetGameCredential(GameCredential? credential)
        {
            return credential != null
                ? new GameInfo(
                    credential,
                    ImageName,
                    ReactComponent,
                    Services.ToDictionary(service => service.Key, service => service.Value))
                : this;
        }

        public override string ToString()
        {
            return Name;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return DisplayName;
            yield return ImageName;
            yield return ReactComponent;
            yield return Linked;
            yield return Services;
        }
    }
}
