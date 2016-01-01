using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace PhotoScreenSaver
{
    public partial class MainForm : Form
    {        
        
        #region Preview API's

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion

        public bool IsPreviewMode = false;

		List<string> undoStack = new List<string>();
		int current;
		static public string[] Folders;
		AutoResetEvent signal = new AutoResetEvent(false);
		Rectangle bounds;

        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        //This constructor is passed the bounds this form is to show in
        //It is used when in normal mode
        public MainForm(Rectangle Bounds)
        {
            InitializeComponent();
			lblFolder.Parent = lblName.Parent = lblDate.Parent = pictureBox1;
			Log("Bounds {0}", Bounds);
			bounds = Bounds;
			Log("Margin {0}", PhotoScreenSaver.Properties.Settings.Default.Margins);
			var m = Regex.Match(PhotoScreenSaver.Properties.Settings.Default.Margins, @"(\d+)(?:,(\d+))(?:,(\d+))(?:,(\d+))");
			if (m.Success) {
				// Top, Bottom, Left, Right
				int[] margins = new int[] { 0, 0, 0, 0 };
				try {
					for (int i = 0; i < 4; i++) {
						margins[i] = int.Parse(m.Groups[i + 1].ToString());
					}
				} catch {
				}
				moveAndShrink(lblFolder, margins[2], margins[0], margins[2] + margins[3]);
				moveAndShrink(lblName, margins[2], -margins[1], margins[2]);
				moveAndShrink(lblDate, -margins[3], -margins[1], margins[3]);
			}
        }

		void moveAndShrink(Control c, int x, int y, int w) {
			c.Location = new Point(c.Location.X + x, c.Location.Y + y);
			c.Size = new Size(c.Width - w, c.Height);
		}

        //This constructor is the handle to the select screensaver dialog preview window
        //It is used when in preview mode (/p)
        public MainForm(IntPtr PreviewHandle)
        {
            InitializeComponent();

            //set the preview window as the parent of this window
            SetParent(this.Handle, PreviewHandle);

            //make this a child window, so when the select screensaver dialog closes, this will also close
            SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

            //set our window's size to the size of our window's new parent
            Rectangle ParentRect;
            GetClientRect(PreviewHandle, out ParentRect);
            this.Size = ParentRect.Size;

            //set our location at (0, 0)
            this.Location = new Point(0, 0);

            IsPreviewMode = true;
        }

        #endregion

		public void Despatch(Action a) {
			if (InvokeRequired) {
				Invoke(a);
			} else
				a();
		}

		int TimePerPic {
			get { return PhotoScreenSaver.Properties.Settings.Default.TimePerPic * 1000; }
		}

		int PicsPerFolder {
			get {
				var m = Regex.Match(PhotoScreenSaver.Properties.Settings.Default.PicsPerFolder, @"(\d+)(?:-(\d+))?");
				if (!m.Success)
					return 2;
				int min = int.Parse(m.Groups[1].Value);
				if(string.IsNullOrEmpty(m.Groups[2].Value))
					return min;
				int max =  int.Parse(m.Groups[2].Value);
				if (max == min)
					return min;
				if (max < min) {
					int tmp = max;
					max = min;
					min = tmp;
				}
				return new Random().Next(min, max);
			}
		}

		int [] PicsToShow(int max) {
			int p = Math.Min(PicsPerFolder, max);
			int [] result = Enumerable.Repeat(-1, p).ToArray();
			for(int i = 0; i < p; i++) {
				int r;
				do {
					r = new Random().Next(max);
				} while(result.Contains(r));
				result[i] = r;
			}
			return result;
		}

		void ShowPictures() {
			for (; ; ) {
				if (Folders.Length == 0) {
					Despatch(delegate() {
						lblFolder.Text = PhotoScreenSaver.Properties.Settings.Default.Folder;
						lblDate.Text = "";
						lblName.Text = "No pictures found";
					});
					return;
				}
				if (current <= undoStack.Count) {
					string folder = Folders[new Random().Next(Folders.Length)];
					string [] pictures = Directory.EnumerateFiles(folder, "*.jpg").ToArray();
					foreach (int p in PicsToShow(pictures.Length)) {
						undoStack.Add(pictures[p]);
					}
					while (undoStack.Count > 1000) {
						undoStack.RemoveAt(0);
						current--;
					}
				}
				ShowPicture();
				current++;
				signal.WaitOne(TimePerPic);
				signal.Reset();
			}
		}

		void ShowPicture() {
			string name = undoStack[current];
			Despatch(delegate() {
				lblFolder.Text = Path.GetFileName(Path.GetDirectoryName(name));
				lblDate.Text = new FileInfo(name).LastWriteTime.Date.ToString("Y");
				lblName.Text = Path.GetFileNameWithoutExtension(name);
				if (pictureBox1.Image != null)
					pictureBox1.Image.Dispose();
				pictureBox1.Image = Image.FromFile(name);
			});
		}

		void Log(string s) {
			if (PhotoScreenSaver.Properties.Settings.Default.Debug) {
				lock (PhotoScreenSaver.Properties.Settings.Default) {
					using (StreamWriter sw = new StreamWriter("PhotoScreenSaver.Log", true)) {
						sw.WriteLine(s);
					}
				}
			}
		}

		void Log(string format, params object[] args) {
			try {
				Log(string.Format(format, args));
			} catch {
			}
		}

		#region GUI

		private void MainForm_Shown(object sender, EventArgs e) {
			new Task(ShowPictures).Start();
        }

        #endregion

        #region User Input

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
			switch (e.KeyValue) {
				case (int)Keys.Left:
					if (current < 2)
						return;
					current -= 2;
					break;
				case (int)Keys.Right:
					break;
				case (int)Keys.Escape:
					Application.Exit();
					return;
				default:
					if (!IsPreviewMode) //disable exit functions for preview
						Application.Exit();
					return;
			}
			signal.Set();
		}

        private void MainForm_Click(object sender, EventArgs e)
        {
            if (!IsPreviewMode) //disable exit functions for preview
                Application.Exit();
        }

        //start off OriginalLoction with an X and Y of int.MaxValue, because
        //it is impossible for the cursor to be at that position. That way, we
        //know if this variable has been set yet.
        Point OriginalLocation = new Point(int.MaxValue, int.MaxValue);

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPreviewMode) { //disable exit functions for preview
                //see if originalLocation has been set
                if (OriginalLocation.X == int.MaxValue & OriginalLocation.Y == int.MaxValue) {
                    OriginalLocation = e.Location;
                }
                //see if the mouse has moved more than 20 pixels in any direction. If it has, close the application.
                if (Math.Abs(e.X - OriginalLocation.X) > 20 | Math.Abs(e.Y - OriginalLocation.Y) > 20) {
                    Application.Exit();
                }
            }
        }

        #endregion

		private void MainForm_Load(object sender, EventArgs e) {
			Bounds = bounds;
			//hide the cursor
			Cursor.Hide();
		}
    }
}
