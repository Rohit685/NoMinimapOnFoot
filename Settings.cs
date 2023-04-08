using System.Windows.Forms;
using Rage;

namespace NoMinimapOnFoot
{
    internal class Settings
    {
        internal static Keys ShowMap = Keys.L;
        internal static Keys ModifierKey = Keys.LShiftKey;
        
        internal static InitializationFile iniFile;
        
        internal static void Initialize()
        {
            try
            {
                iniFile = new InitializationFile(@"Plugins/NoMinimapOnFoot.ini");
                iniFile.Create();
                ShowMap = iniFile.ReadEnum("Keybinds", "ShowMap", ShowMap);
                ModifierKey = iniFile.ReadEnum("Keybinds", "ModifierKey", ModifierKey);
            }
            catch(System.Exception e)
            {
                string error = e.ToString();
                Game.LogTrivial("NoMinimapOnFoot: ERROR IN 'Settings.cs, Initialize()': " + error);
                Game.DisplayNotification("NoMinimapOnFoot: Error Occured");
            }
        }
    }
}