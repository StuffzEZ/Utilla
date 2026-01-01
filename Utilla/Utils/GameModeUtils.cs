using GorillaGameModes;
using System;
using System.Globalization;
using System.Linq;
using Utilla.Behaviours;
using Utilla.Models;

namespace Utilla.Utils
{
    public static class GameModeUtils
    {
        public static Gamemode CurrentGamemode { get; internal set; }

        public static Gamemode FindGamemodeInString(string gmString) => GetGamemode(gamemode => gmString.EndsWith(gamemode.ID));

        public static Gamemode GetGamemodeFromId(string id) => GetGamemode(gamemode => gamemode.ID == id);

        public static Gamemode GetGamemode(Func<Gamemode, bool> predicate)
        {
            if (!GamemodeManager.HasInstance) return null;
        
            // Return the first gamemode that matches the predicate, ignoring _MOD in IDs
            return GamemodeManager.Instance.Gamemodes
                .FirstOrDefault(gamemode => predicate(new Gamemode 
                { 
                    ID = gamemode.ID.Replace("_MOD_", "_"), 
                    Name = gamemode.Name 
                }));
        }

        public static string GetGameModeName(GameModeType gameModeType)
        {
            string modeName = (GetGameModeInstance(gameModeType) is GorillaGameManager gameManager) ? gameManager.GameModeName() : GameMode.GameModeZoneMapping.GetModeName(gameModeType);
            return (modeName.ToLower() == gameModeType.GetName().ToLower()) ? gameModeType.GetName() : CultureInfo.InvariantCulture.TextInfo.ToTitleCase(modeName.ToLower());
        }

        public static GorillaGameManager GetGameModeInstance(GameModeType gameModeType)
        {
            if (GameMode.GetGameModeInstance(gameModeType) is GorillaGameManager gameManager && gameManager)
                return gameManager;
            return null;
        }
    }
}
