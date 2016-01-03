using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhotoScreenSaver {
	public partial class SettingsForm : Form {
		public SettingsForm() {
			InitializeComponent();
			txtFolder.Text = PhotoScreenSaver.Properties.Settings.Default.Folder;
			txtNumber.Text = PhotoScreenSaver.Properties.Settings.Default.PicsPerFolder;
			txtTime.Text = PhotoScreenSaver.Properties.Settings.Default.TimePerPic.ToString();
			txtMargins.Text = PhotoScreenSaver.Properties.Settings.Default.Margins;
			ckDebug.Checked = PhotoScreenSaver.Properties.Settings.Default.Debug;
			Text = "PhotoScreenSaver v" + Program.Version;
		}

		public SettingsForm(IntPtr PreviewHandle)
			: this() {
			//set the preview window as the parent of this window
			MainForm.SetParent(this.Handle, PreviewHandle);
		}

		private void btnBrowse_Click(object sender, EventArgs e) {
			folderBrowserDialog1.SelectedPath = txtFolder.Text;
			if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
				txtFolder.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void btnOK_Click(object sender, EventArgs e) {
			try {
				PhotoScreenSaver.Properties.Settings.Default.Folder = txtFolder.Text;
				PhotoScreenSaver.Properties.Settings.Default.PicsPerFolder = txtNumber.Text;
				PhotoScreenSaver.Properties.Settings.Default.TimePerPic = int.Parse(txtTime.Text);
				PhotoScreenSaver.Properties.Settings.Default.Margins = txtMargins.Text;
				PhotoScreenSaver.Properties.Settings.Default.Debug = ckDebug.Checked;
				PhotoScreenSaver.Properties.Settings.Default.Save();
			} catch {
			}
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e) {
			Close();
		}

	}
}
