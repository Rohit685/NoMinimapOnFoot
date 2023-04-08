using System;
using System.Dynamic;
using System.Windows.Forms;
using Rage;
using Rage.Native;

[assembly: Rage.Attributes.Plugin("NoMinimapOnFoot", Description = "Disables radar when on foot", Author = "Roheat")]
namespace NoMinimapOnFoot
{
    internal static class EntryPoint
    {
        internal static Ped Player => Game.LocalPlayer.Character;
        internal static void Main()
        {
            Game.DisplayNotification("hunting","huntingwindarrow_32","No Minimap On Foot", "~b~By Roheat","~g~Loaded Successfully!");
            Settings.Initialize();
            while (true)
            {
                GameFiber.Yield();
                if (IsPlayerInVehicle())
                {
                    NativeFunction.Natives.DISPLAY_RADAR(true);
                }
                if (!IsPlayerInVehicle() && !NativeFunction.Natives.IS_RADAR_HIDDEN<bool>())
                {
                    NativeFunction.Natives.DISPLAY_RADAR(false);
                }

                if (!IsPlayerInVehicle() && CheckModifierKey() && Game.IsKeyDownRightNow(Settings.ShowMap))
                {
                    ToggleMinimap();
                }
            }   
        }

        internal static bool IsPlayerInVehicle()
        {
            return Player.IsInAnyVehicle(false);
        }

        internal static void ToggleMinimap()
        {
            if (NativeFunction.Natives.IS_RADAR_HIDDEN<bool>())
            {
                NativeFunction.Natives.DISPLAY_RADAR(true);
                return;
            }
            else
            {
                NativeFunction.Natives.DISPLAY_RADAR(false);
            }
        }
        
        internal static bool CheckModifierKey() => Settings.ModifierKey == Keys.None ? true : Game.IsKeyDownRightNow(Settings.ModifierKey);
    }
}