using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
namespace AutoPaksFinderByKye
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Your Paks are located at: " + GetPaksLocation());
            Console.ReadLine();
        }
        public static string GetPaksLocation()
        {
            string LauncherJson = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
            if (File.Exists(LauncherJson))
            {
                try
                {
                    EpicGamesLauncher.Root launcherdata = JsonConvert.DeserializeObject<EpicGamesLauncher.Root>(File.ReadAllText(LauncherJson));
                    string baseInstallLocation = launcherdata.InstallationList.First(x => x.AppName == "Fortnite").InstallLocation;
                    return baseInstallLocation + @"\FortniteGame\Content\Paks";

                }
                catch (Exception ex)
                {
                    throw new Exception($"Paks folder could not be found: {ex.Message}");
                }
            }
            return null;
        }
    }

    class EpicGamesLauncher
    {
        public class InstallationList
        {
            public string InstallLocation { get; set; }
            public string NamespaceId { get; set; }
            public string ItemId { get; set; }
            public string ArtifactId { get; set; }
            public string AppVersion { get; set; }
            public string AppName { get; set; }
        }
        public class Root
        {
            public InstallationList[] InstallationList { get; set; }
        }
    }
}
