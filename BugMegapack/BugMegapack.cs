using BugMegapack.Config;
using BugMegapack.Events;
using Exiled.API.Features;
using System;

namespace BugMegapack
{
    public class BugMegapack : Plugin<BaseConfig, Translation>
    {
        private Scp173Events _scp173Events;
        private PlayerEvents _playerEvents;
        private ServerEvents _serverEvents;

        public static BugMegapack Instance;

        public override string Name { get; } = nameof(BugMegapack);
        public override string Author { get; } = "Heisenberg3666";
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 2);

        public override void OnEnabled()
        {
            Instance = this;

            _scp173Events = new Scp173Events(Config);
            _playerEvents = new PlayerEvents(Config, Translation);
            _serverEvents = new ServerEvents(Config);

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            _scp173Events = null;
            _playerEvents = null;
            _serverEvents = null;

            Instance = null;

            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            _scp173Events.RegisterEvents();
            _playerEvents.RegisterEvents();
            _serverEvents.RegisterEvents();
        }

        public void UnregisterEvents()
        {
            _scp173Events.UnregisterEvents();
            _playerEvents.UnregisterEvents();
            _serverEvents.UnregisterEvents();
        }
    }
}
