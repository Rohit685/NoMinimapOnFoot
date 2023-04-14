using System;
using System.Dynamic;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Rage;
using Rage.Native;

[assembly: Rage.Attributes.Plugin("NoMinimapOnFoot", Description = "Disables radar when on foot", Author = "Roheat")]
namespace NoMinimapOnFoot
{
    internal static class EntryPoint
    {
        internal static Ped Player => Game.LocalPlayer.Character;
        internal static MinimapDisabledDisplayType chosenType;
        internal static bool IsMinimapEnabled = true;
        internal static bool parseSuccessful = false;

        internal enum MinimapDisabledDisplayType
        {
            SCRAMBLE,
            HIDE
        }
        internal static void Main()
        {
            Game.DisplayNotification("hunting","huntingwindarrow_32","No Minimap On Foot", "~b~By Roheat","~g~Loaded Successfully!");
            Settings.Initialize();
            parseSuccessful =Enum.TryParse(Settings.typeOfDisabling, true, out chosenType);
            CheckParse();
            while (true)
            {
                GameFiber.Yield();
                if (IsPlayerInVehicle() && !IsMinimapEnabled)
                {
                    EnableMinimap();
                }
                if (!IsPlayerInVehicle() && IsMinimapEnabled)
                {
                    DisableMinimap();
                }

                if (!IsPlayerInVehicle() && CheckModifierKey() && Game.IsKeyDownRightNow(Settings.ShowMap))
                {
                    ToggleMinimap();
                }
            }   
        }

        internal static bool IsPlayerInVehicle() => Player.IsInAnyVehicle(false);
        
        internal static bool CheckModifierKey() => Settings.ModifierKey == Keys.None ? true : Game.IsKeyDownRightNow(Settings.ModifierKey);

        internal static void CheckParse()
        {
            if (!parseSuccessful)
            {
                chosenType = MinimapDisabledDisplayType.HIDE;
            }
        }
        
        internal static void EnableMinimap()
        {
            NativeFunction.Natives.DISPLAY_RADAR(true);
            NativeFunction.Natives.UNLOCK_MINIMAP_POSITION();
            IsMinimapEnabled = true;
        }

        internal static void DisableMinimap()
        {
            switch (chosenType)
            {
                case MinimapDisabledDisplayType.HIDE:
                    NativeFunction.Natives.DISPLAY_RADAR(false);
                    break;
                case MinimapDisabledDisplayType.SCRAMBLE:
                    NativeFunction.Natives.LOCK_MINIMAP_POSITION(Player.Position.X + 1000f, Player.Position.Y + 1000f);
                    break;
            }
            
            IsMinimapEnabled = false;
        }

        internal static void ToggleMinimap()
        {
            if (IsMinimapEnabled)
            {
                DisableMinimap();
            }
            else
            {
                EnableMinimap();
            }
        }

        internal static void OnUnload(bool Exit)
        {
            EnableMinimap();
            Game.LogTrivial("NoMinimapOnFoot Unloaded.");
        }
    }
}
