using System.Net;
using System.Reflection;
using Rage;

namespace NoMinimapOnFoot
{
    internal static class VersionChecker
    {
        internal static string CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString(3);

        internal static void CheckForUpdates()
        {
            var webClient = new WebClient();
            var pluginUpToDate = false;
            var webSuccess = false;
            try
            {
                var receivedVersion = webClient
                    .DownloadString(
                        "https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=43469&textOnly=1")
                    .Trim();
                Game.LogTrivial(
                    $"NoMinimapOnFoot: Online Version: {receivedVersion} | Local NoMinimapOnFoot Version: {CurrentVersion}");
                pluginUpToDate = receivedVersion == CurrentVersion;
                webSuccess = true;
            }
            catch (WebException)
            {
                Game.DisplayNotification(
                    "NoMinimapOnFoot By Roheat\nPlease make sure you are connected to proper WIFI Network.");
            }
            finally
            {
                Game.DisplayNotification(
                    $"NoMinimapOnFoot By Roheat\nVersion is {(webSuccess ? pluginUpToDate ? "~g~Up To Date" : "~r~Out Of Date" : "~o~Version Check Failed")}");
                if (!pluginUpToDate)
                {
                    Game.LogTrivial(
                        "NoMinimapOnFoot: [VERSION OUTDATED] Please update to latest version here: https://www.lcpdfr.com/downloads/gta5mods/scripts/43469-no-minimap-on-foot/");
                }
            }
        }
    }
}