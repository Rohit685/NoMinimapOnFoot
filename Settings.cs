using System.Windows.Forms;
using Rage;
using static NoMinimapOnFoot.EntryPoint;

namespace NoMinimapOnFoot
{
    internal class Settings
    {
        internal static Keys ShowMap = Keys.L;
        internal static Keys ModifierKey = Keys.LShiftKey;
        internal static string typeOfDisabling = MinimapDisabledDisplayType.HIDE.ToString();
        internal static InitializationFile iniFile;
        
        internal static void Initialize()
        {
            try
            {
                iniFile = new InitializationFile(@"Plugins/NoMinimapOnFoot.ini");
                iniFile.Create();
                ShowMap = iniFile.ReadEnum("Keybinds", "ShowMap", ShowMap);
                ModifierKey = iniFile.ReadEnum("Keybinds", "ModifierKey", ModifierKey);
                typeOfDisabling = iniFile.ReadString("Customization", "typeOfDisabling",typeOfDisabling.ToString());
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