using System;
using System.Windows.Forms;

namespace mre.view
{
	public partial class FrmAbout : Form
	{
		public FrmAbout()
		{
			InitializeComponent();
		}

		private void BtnDonate_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://paypal.me/JKerboeuf");
		}

		private void BtnWebsite_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("https://github.com/JKerboeuf/Minecraft-Resource-Extractor");
		}

		private void BtnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}