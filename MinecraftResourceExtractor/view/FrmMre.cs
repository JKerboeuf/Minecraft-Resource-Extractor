using mre.controller;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace mre.view
{
	public partial class FrmMre : Form
	{
		private Controller controller { get; }

		public FrmMre()
		{
			InitializeComponent();
			controller = new Controller(this);
			if (controller.GetJavaPath() == null)
			{
				MessageBox.Show("ERROR\nYou need to install Java JDK first in order to use this tool", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Load += (s, e) => Close();
				return;
			}
			Log("Welcome to the Minecraft Resource Extractor !");
			StepsController.Step1(this);
			Log("Select if you would want to extract resources from an official Minecraft version or an individual jar file.");
		}

		public void Log(string msg, string color = "Black")
		{
			rtbHelp.SelectionColor = Color.FromName(color);
			rtbHelp.AppendText("- " + msg + '\n');
		}

		public void Status(string msg)
		{
			slbStatusLabel.Text = msg;
		}

		public void SetCheckAll(CheckedListBox elem, bool value)
		{
			for (int i = 0; i < elem.Items.Count; i++)
			{
				elem.SetItemChecked(i, value);
			}
		}

		public void FillCheckedBox(CheckedListBox elem, List<string> list)
		{
			foreach (var line in list)
				elem.Items.Add(line);
		}

		public void SwitchUiLock(int step)
		{
			Cursor = Cursor.Current == Cursors.Default ? Cursors.WaitCursor : Cursors.Default;
			if (step >= 4)
				grbStep4.Enabled = !grbStep4.Enabled;
			if (step >= 3)
				grbStep3.Enabled = !grbStep3.Enabled;
			if (step >= 2)
				grbStep1.Enabled = !grbStep1.Enabled;
			if (step >= 1)
				grbStep2.Enabled = !grbStep2.Enabled;
		}

		private void SwitchExtractionType()
		{
			if (rdbExMinecraft.Checked)
			{
				rdbExJar.Checked = false;
				btnLocateJar.Text = "Locate...";
			}
			else
			{
				rdbExMinecraft.Checked = false;
				btnLocateJar.Text = "Browse...";
			}
			StepsController.Step1(this);
		}

		private void RdbExMinecraft_CheckedChanged(object sender, EventArgs e)
		{
			SwitchExtractionType();
		}

		private void BtnLocateJar_Click(object sender, EventArgs e)
		{
			if (rdbExMinecraft.Checked)
			{
				controller.LocateMinecraft();
			}
			else
			{
				controller.LocateJarFile();
			}
		}

		private void BtnConfirm2_Click(object sender, EventArgs e)
		{
			if (cmbVersions.Text != string.Empty)
			{
				controller.FindVersionJar(cmbVersions.Text);
			}
		}

		private void BtnConfirm3_Click(object sender, EventArgs e)
		{
			if (chkExtGroups.GetItemChecked(0))
			{
				controller.CheckVersionJar();
			}
			else if (chkExtGroups.GetItemChecked(1))
			{
				controller.GetAssets();
				Status("Job completed !");
				Log("Thank you for using the Minecraft Resource Extractor made by Julien Kerboeuf !", "DarkGreen");
				Process.Start("explorer.exe", controller.settings.MreDirPath + "\\mre-output");
			}
		}

		private void BtnConfirm4_Click(object sender, EventArgs e)
		{
			if (rdbExJar.Checked || chkExtGroups.GetItemChecked(0))
			{
				Log("Sarting jar extraction, this can take a while, please be patient...");
				for (int i = 0; i < chkExtFolders.Items.Count; i++)
				{
					if (chkExtFolders.GetItemChecked(i))
					{
						controller.ExtractJarFolder(chkExtFolders.Items[i].ToString());
					}
				}
				Log("Successfully extracted content from jar file ! Your files are located in the \"mre-output\" folder.", "DarkGreen");
			}
			if (!rdbExJar.Checked && chkExtGroups.GetItemChecked(1))
			{
				controller.GetAssets();
			}
		}

		private void LnkAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			FrmAbout frmAbout = new FrmAbout();
			frmAbout.ShowDialog();
		}

		private void CmbVersions_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (grbStep3.Enabled)
				StepsController.Step2(this);
		}

		private void ChkExtGroups_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (grbStep4.Enabled)
				StepsController.Step3(this);
		}

		private void FrmMre_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (Directory.Exists(controller.settings.MreDirPath + "\\mre-tmp"))
				Directory.Delete(controller.settings.MreDirPath + "\\mre-tmp", true);
		}
	}
}