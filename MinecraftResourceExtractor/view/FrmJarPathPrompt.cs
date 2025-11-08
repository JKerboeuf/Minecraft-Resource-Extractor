using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mre.view
{
	public partial class FrmJarPathPrompt : Form
	{
		public string SelectedPath { get; private set; }

		public FrmJarPathPrompt()
		{
			InitializeComponent();
		}

		private void btnBrowseJar_Click(object sender, EventArgs e)
		{
			using (var dialog = new OpenFileDialog())
			{
				dialog.Title = "Select jar.exe";
				dialog.Filter = "jar.exe (jar.exe)|jar.exe|Executable files (*.exe)|*.exe|All files (*.*)|*.*";
				dialog.CheckFileExists = true;
				dialog.FileName = "jar.exe";
				dialog.Multiselect = false;

				if (dialog.ShowDialog(this) == DialogResult.OK)
				{
					txtJarPath.Text = dialog.FileName;
					btnJarPathOk.Enabled = true;
				}
			}
		}

		private void btnJarPathOk_Click(object sender, EventArgs e)
		{
			var p = txtJarPath.Text;
			if (string.IsNullOrWhiteSpace(p) || !File.Exists(p))
			{
				MessageBox.Show(this, "The selected file doesn't exist.", "Invalid path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				DialogResult = DialogResult.None;
				return;
			}

			// Ensure user actually selected jar.exe (case-insensitive)
			if (!string.Equals(Path.GetFileName(p), "jar.exe", StringComparison.OrdinalIgnoreCase))
			{
				var res = MessageBox.Show(this, "The selected file is not named 'jar.exe'. Are you sure you want to use it?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (res == DialogResult.No)
				{
					DialogResult = DialogResult.None;
					return;
				}
			}

			SelectedPath = p;
		}
	}
}
