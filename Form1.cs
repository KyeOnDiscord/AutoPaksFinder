using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
namespace AutoPaksFinderByKye
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string Paks { get; set; } //This Paks string will be set after FindPaks();
        private static readonly string[] drives = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        private static string GetEpicDir()
        {
            foreach (string letter in drives)//Searches all drives for LauncherInstalled.dat
            {
                if (File.Exists(letter + @":\ProgramData\Epic\UnrealEngineLauncher\LauncherInstalled.dat"))
                    return letter + @":\ProgramData\Epic\UnrealEngineLauncher\LauncherInstalled.dat";
            }
            return "Could not find LauncherInstalled.dat";
        }
        
        public static void FindPaks()
        {
            try
            {
                string line;
                StreamReader file = new StreamReader(GetEpicDir());
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("\"InstallLocation\"") && line.Contains("Fortnite"))
                    {
                        string path = line.Replace("			\"InstallLocation\": \"", "");
                        string path2 = path.Replace(@"\\", @"\");
                        string fnname = "Fortnite\"" + ",";
                        string path3 = path2.Replace(fnname, "Fortnite");
                        Paks = path3 + @"\FortniteGame\Content\Paks";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            FindPaks();//Sets the "Paks" string to your paks folder
            textBox1.Text = Paks;//Sets the textbox as Paks folder directory
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FindPaks(); //Sets the "Paks" string to your paks folder
                textBox1.Text = Paks; //Displays the paks directory
                Process.Start(Paks); //Opens Paks folder
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //If the LauncherInstalled.dat file is deleted it generates a new one by opening Epic Games Launcher
        private void button3_Click(object sender, EventArgs e)
        {
            if (GetEpicDir() == "Could not find LauncherInstalled.dat")
            {
                Process.Start("com.epicgames.launcher://");//Generates LauncherInstalled.dat
                Thread.Sleep(3000); //Gives times to generate the file
                foreach (var process in Process.GetProcessesByName("EpicGamesLauncher")) //Stops Epic Games Launcher
                    process.Kill();
                MessageBox.Show("Generated new LauncherInstalled.dat");
            }
            else
            {
                File.Delete(GetEpicDir());
                Process.Start("com.epicgames.launcher://");//Generates LauncherInstalled.dat
                Thread.Sleep(3000);//Gives times to generate the file
                foreach (var process in Process.GetProcessesByName("EpicGamesLauncher"))//Stops Epic Games Launcher
                    process.Kill();
                MessageBox.Show("Generated new LauncherInstalled.dat");
            }
        }
    }
}
