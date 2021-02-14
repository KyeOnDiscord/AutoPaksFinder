# AutoPaksFinder
Gets your Fortnite Paks Folder Automatically using the LauncherInstalled.dat file


Basically boils down to this:

Make sure to add System.Web.Extensions as a reference

```csharp
using System;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.IO;

namespace AutoPaksFinderByKye
{
    static class Program
    {
        static void Main()
        {
            MessageBox.Show("Your Paks are located at: " + PakFiles + @"\FortniteGame\Content\Paks");
        }
        public class InstallationList
        {
            public string InstallLocation { get; set; }
            public string AppName { get; set; }
            public string AppVersion { get; set; }
        }

        public class Root
        {
            public List<InstallationList> InstallationList { get; set; }
        }
        public static string PakFiles
        {
            get
            {
                string datfile = "";
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
                if (File.Exists(path))
                    datfile = path;
                else
                    datfile = "";



                if (datfile != "")
                {
                    try
                    {
                        foreach (var d in new JavaScriptSerializer().Deserialize<Root>(File.ReadAllText(datfile)).InstallationList)
                        {
                            if (d.AppName == "Fortnite")
                                return d.InstallLocation;
                        }
                        return "";
                    }
                    catch
                    {
                        MessageBox.Show("Could not find your pak files! Please select them manually!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return "";
                    }
                }
                else
                {
                    MessageBox.Show("Could not find your pak files! Please select them manually!", "Pro Swapper", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "";
                }
            }
            
        }
    }
}
```
