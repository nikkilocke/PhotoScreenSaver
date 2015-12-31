﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (args.Length > 0) {
				switch (args[0].ToLower().Trim().Substring(0, 2)) {
					case "/p": //preview
						//show the screen saver preview
						Application.Run(new MainForm(new IntPtr(long.Parse(args[1])))); //args[1] is the handle to the preview window
						return;
					case "/c": //configure
						new SettingsForm().ShowDialog();
						return;
					case "/s": //show
					default: //an argument was passed, but it wasn't /s, /p, or /c, so we don't care wtf it was
						//show the screen saver anyway
						break;
				}
            }
            //run the screen saver
            ShowScreensaver();
        }

        //will show the screen saver
        static void ShowScreensaver()
        {
			MainForm.Folders = Directory.EnumerateDirectories(PhotoScreenSaver.Properties.Settings.Default.Folder).Where(d => 
				!File.Exists(Path.Combine(d, "ignore")) && Directory.EnumerateFiles(d, "*.jpg").FirstOrDefault() != null).ToArray();
			//loops through all the computer's screens (monitors)
            foreach (Screen screen in Screen.AllScreens)
            {
                //creates a form just for that screen and passes it the bounds of that screen
                MainForm screensaver = new MainForm(screen.Bounds);
                screensaver.Show();
            }
			Application.Run();
		}
    }
}
