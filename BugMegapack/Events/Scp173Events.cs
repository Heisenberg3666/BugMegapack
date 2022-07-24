using BugMegapack.Config;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;

namespace BugMegapack.Events
{
    internal class Scp173Events : IEventHandler
    {
        private BaseConfig _config;

        public Scp173Events(BaseConfig config)
        {
            _config = config;
        }

        public void RegisterEvents()
        {
            if (_config.TantrumCrashing)
                Scp173.PlacingTantrum += OnPlacingTantrum;
        }

        public void UnregisterEvents()
        {
            if (_config.TantrumCrashing)
                Scp173.PlacingTantrum -= OnPlacingTantrum;
        }

        private void OnPlacingTantrum(PlacingTantrumEventArgs e)
        {
            if (e.Player.Role == RoleType.Scp173)
                Exiled.API.Features.Server.Restart();
        }
    }
}
