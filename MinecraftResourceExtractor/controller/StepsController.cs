using mre.view;

namespace mre.controller
{
	public static class StepsController
	{
		public static void Step1(FrmMre form)
		{
			form.grbStep2.Enabled = false;
			form.grbStep3.Enabled = false;
			form.grbStep4.Enabled = false;
			form.txtPath.Text = string.Empty;
			form.cmbVersions.Items.Clear();
			form.chkExtFolders.Items.Clear();
			form.pgbProgress.Value = 0;
			form.btnLocateJar.Focus();
			form.SetCheckAll(form.chkExtGroups, true);
			form.Status("Step 1 > Choose what to extract");
		}

		public static void Step2(FrmMre form)
		{
			form.grbStep2.Enabled = true;
			form.grbStep3.Enabled = false;
			form.grbStep4.Enabled = false;
			form.cmbVersions.Focus();
			form.chkExtFolders.Items.Clear();
			form.pgbProgress.Value = 0;
			form.Status("Step 2 > Choose a version to extract");
		}

		public static void Step3(FrmMre form)
		{
			form.grbStep3.Enabled = true;
			form.grbStep4.Enabled = false;
			form.chkExtFolders.Items.Clear();
			form.btnConfirm3.Focus();
			form.pgbProgress.Value = 0;
			form.Status("Step 3 > Choose what to extract");
		}

		public static void Step4(FrmMre form)
		{
			form.grbStep4.Enabled = true;
			form.btnConfirm4.Focus();
			form.pgbProgress.Value = 0;
			form.Status("Step 4 > Choose folders to extract from jar");
		}
	}
}