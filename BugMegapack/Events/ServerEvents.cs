using BugMegapack.API;
using BugMegapack.Config;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugMegapack.Events
{
    internal class ServerEvents : IEventHandler
    {
        private BaseConfig _config;

        public ServerEvents(BaseConfig config)
        {
            _config = config;
        }

        public void RegisterEvents()
        {
            if (_config.SprintBugMaxPlayers > 0)
                Exiled.Events.Handlers.Server.RoundStarted += OnRoundStart;
        }

        public void UnregisterEvents()
        {
            if (_config.SprintBugMaxPlayers > 0)
            {
                Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStart;
            }
        }

        private void OnRoundStart()
        {
            List<Player> players = Player.List.ToList();
            players.ShuffleList();

            foreach (Player player in players.Take(_config.SprintBugMaxPlayers))
                BugMegapackApi.StaminaBuggedPlayers.Add(player.Id);
        }
    }
}
