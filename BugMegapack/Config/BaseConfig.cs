using Exiled.API.Interfaces;
using System.ComponentModel;

namespace BugMegapack.Config
{
    public class BaseConfig : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("This is the bug that was introduced when SCP: SL version 11.2.1 released on servers with Exiled 5.2.2 (intergration bug). " +
            "Whenever SCP-173 places a tantrum, the server crashes.")]
        public bool TantrumCrashing { get; set; } = true;

        [Description("This is the bug that kills SCP-173 when they fall in the server room.")]
        public bool PeanutAnticheat { get; set; } = true;

        [Description("This is the maximum amount of players that can be affected by the sprint bug. Set to 0 to disable.")]
        public int SprintBugMaxPlayers { get; set; } = 10;
    }
}
