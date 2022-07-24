using BugMegapack.API;
using BugMegapack.Config;
using Exiled.API.Enums;
using Exiled.Events.EventArgs;
using Exiled.Events.Handlers;

namespace BugMegapack.Events
{
    internal class PlayerEvents : IEventHandler
    {
        private BaseConfig _config;
        private Translation _translation;

        public PlayerEvents(BaseConfig config, Translation translation)
        {
            _config = config;
            _translation = translation;
        }

        public void RegisterEvents()
        {
            if (_config.PeanutAnticheat)
                Player.Landing += OnLanding;

            if (_config.SprintBugMaxPlayers > 0)
            {
                Player.PickingUpItem += OnPickingUpItem;
                Player.PickingUpArmor += OnPickingUpArmour;
                Player.PickingUpAmmo += OnPickingUpAmmo;
                Player.SearchingPickup += OnSearchingPickup;
                Player.ChangingMoveState += OnChangingMoveState;
            }
        }

        public void UnregisterEvents()
        {
            if (_config.PeanutAnticheat)
                Player.Landing -= OnLanding;

            if (_config.SprintBugMaxPlayers > 0)
            {
                Player.PickingUpItem -= OnPickingUpItem;
                Player.PickingUpArmor -= OnPickingUpArmour;
                Player.PickingUpAmmo -= OnPickingUpAmmo;
                Player.SearchingPickup -= OnSearchingPickup;
                Player.ChangingMoveState -= OnChangingMoveState;
            }
        }

        private void OnLanding(LandingEventArgs e)
        {
            if (e.Player.Role == RoleType.Scp173
                && e.Player.CurrentRoom.Type == RoomType.HczServers)
            {
                e.Player.Kill(_translation.PeanutAnticheatDeathReason);
            }
        }

        private void OnPickingUpItem(PickingUpItemEventArgs e) => e.IsAllowed = !BugMegapackApi.StaminaBuggedPlayers.Contains(e.Player.Id);
        private void OnPickingUpArmour(PickingUpArmorEventArgs e) => e.IsAllowed = !BugMegapackApi.StaminaBuggedPlayers.Contains(e.Player.Id);
        private void OnPickingUpAmmo(PickingUpAmmoEventArgs e) => e.IsAllowed = !BugMegapackApi.StaminaBuggedPlayers.Contains(e.Player.Id);
        private void OnSearchingPickup(SearchingPickupEventArgs e) => e.IsAllowed = !BugMegapackApi.StaminaBuggedPlayers.Contains(e.Player.Id);
        private void OnChangingMoveState(ChangingMoveStateEventArgs e)
        {
            if (e.NewState == PlayerMovementState.Sprinting
                && BugMegapackApi.StaminaBuggedPlayers.Contains(e.Player.Id))
            {
                e.Player.Stamina._isSprinting = false;
                e.Player.Stamina.RemainingStamina = 0f;
            }
        }
    }
}
