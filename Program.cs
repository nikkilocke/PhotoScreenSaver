using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace PhotoScreenSaver
{
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
			if (string.IsNullOrWhiteSpace(PhotoScreenSaver.Properties.Settings.Default.Folder)) {
				PhotoScreenSaver.Properties.Settings.Default.Folder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
				PhotoScreenSaver.Properties.Settings.Default.Save();
			}
			MainForm.Log("PhotoScreenSaver v{0}", Version);
			MainForm.Log(Environment.CommandLine);
			MainForm.Log("Config={0}", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
			MainForm.Log("User={0}", System.Security.Principal.WindowsIdentity.GetCurrent().Name);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm.Folders = Directory.EnumerateDirectories(PhotoScreenSaver.Properties.Settings.Default.Folder).Where(d =>
				!File.Exists(Path.Combine(d, "ignore")) && Directory.EnumerateFiles(d, "*.jpg").FirstOrDefault() != null).ToArray();
			if (args.Length > 0) {
				string arg2 = args.Length > 1 ? args[1] : args[0].Length > 3 ? args[0].Substring(3) : "";
				switch (args[0].ToLower().Trim().Substring(0, 2)) {
					case "/p": //preview
						//show the screen saver preview
						Application.Run(new MainForm(new IntPtr(long.Parse(arg2))));
						return;
					case "/b":	// bounds
						var m = Regex.Match(args[1], @"(\d+)(?:,(\d+))(?:,(\d+))(?:,(\d+))");
						if (m.Success) {
							// Left, Top, Width, Height
							int[] rect = new int[] { 0, 0, 0, 0 };
							try {
								for (int i = 0; i < 4; i++) {
									rect[i] = int.Parse(m.Groups[i + 1].ToString());
								}
								MainForm screensaver = new MainForm(new Rectangle(rect[0], rect[1], rect[2], rect[3])) {
									IsPreviewMode = args[0][1] == 'B'
								};
								Application.Run(screensaver);
								return;
							} catch {
							}
						}
						break;
					case "/s": //show
						//run the screen saver
						ShowScreensaver();
						return;
					case "/c": //configure
						// new SettingsForm(new IntPtr(long.Parse(arg2))).Show();
						break;
				}
            }
			// Default action is to show settings dialog
			new SettingsForm().ShowDialog();
		}

        //will show the screen saver
        static void ShowScreensaver()
        {
			//loops through all the computer's screens (monitors)
            foreach (Screen screen in Screen.AllScreens)
            {
                //creates a form just for that screen and passes it the bounds of that screen
                MainForm screensaver = new MainForm(screen.Bounds);
                screensaver.Show();
            }
			Application.Run();
		}

		public static string Version {
			get {
				System.Reflection.Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
				if (assembly == null) assembly = System.Reflection.Assembly.GetExecutingAssembly();
				return assembly.GetName().Version.ToString(); 
			}
		}
    }
}
