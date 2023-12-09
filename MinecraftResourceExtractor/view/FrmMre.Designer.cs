namespace mre.view
{
	partial class FrmMre
	{
		/// <summary>
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Nettoyage des resources utilisées.
		/// </summary>
		/// <param name="disposing">true si les resources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur Windows Form

		/// <summary>
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMre));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.pgbProgress = new System.Windows.Forms.ToolStripProgressBar();
			this.slbStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnLocateJar = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.grbStep1 = new System.Windows.Forms.GroupBox();
			this.rdbExMinecraft = new System.Windows.Forms.RadioButton();
			this.rdbExJar = new System.Windows.Forms.RadioButton();
			this.lnkAbout = new System.Windows.Forms.LinkLabel();
			this.grbStep2 = new System.Windows.Forms.GroupBox();
			this.btnConfirm2 = new System.Windows.Forms.Button();
			this.cmbVersions = new System.Windows.Forms.ComboBox();
			this.grbStep3 = new System.Windows.Forms.GroupBox();
			this.btnConfirm3 = new System.Windows.Forms.Button();
			this.chkExtGroups = new System.Windows.Forms.CheckedListBox();
			this.grbHelp = new System.Windows.Forms.GroupBox();
			this.rtbHelp = new System.Windows.Forms.RichTextBox();
			this.grbStep4 = new System.Windows.Forms.GroupBox();
			this.btnConfirm4 = new System.Windows.Forms.Button();
			this.chkExtFolders = new System.Windows.Forms.CheckedListBox();
			this.statusStrip1.SuspendLayout();
			this.grbStep1.SuspendLayout();
			this.grbStep2.SuspendLayout();
			this.grbStep3.SuspendLayout();
			this.grbHelp.SuspendLayout();
			this.grbStep4.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pgbProgress,
            this.slbStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 369);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(634, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// pgbProgress
			// 
			this.pgbProgress.Name = "pgbProgress";
			this.pgbProgress.Size = new System.Drawing.Size(100, 16);
			this.pgbProgress.Step = 1;
			this.pgbProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// slbStatusLabel
			// 
			this.slbStatusLabel.Name = "slbStatusLabel";
			this.slbStatusLabel.Size = new System.Drawing.Size(118, 17);
			this.slbStatusLabel.Text = "toolStripStatusLabel1";
			// 
			// btnLocateJar
			// 
			this.btnLocateJar.Location = new System.Drawing.Point(241, 16);
			this.btnLocateJar.Name = "btnLocateJar";
			this.btnLocateJar.Size = new System.Drawing.Size(115, 23);
			this.btnLocateJar.TabIndex = 1;
			this.btnLocateJar.Text = "Locate...";
			this.btnLocateJar.UseVisualStyleBackColor = true;
			this.btnLocateJar.Click += new System.EventHandler(this.BtnLocateJar_Click);
			// 
			// txtPath
			// 
			this.txtPath.Enabled = false;
			this.txtPath.Location = new System.Drawing.Point(6, 45);
			this.txtPath.Name = "txtPath";
			this.txtPath.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.txtPath.Size = new System.Drawing.Size(350, 20);
			this.txtPath.TabIndex = 2;
			// 
			// grbStep1
			// 
			this.grbStep1.Controls.Add(this.rdbExMinecraft);
			this.grbStep1.Controls.Add(this.rdbExJar);
			this.grbStep1.Controls.Add(this.txtPath);
			this.grbStep1.Controls.Add(this.btnLocateJar);
			this.grbStep1.Location = new System.Drawing.Point(12, 12);
			this.grbStep1.Name = "grbStep1";
			this.grbStep1.Size = new System.Drawing.Size(362, 76);
			this.grbStep1.TabIndex = 3;
			this.grbStep1.TabStop = false;
			this.grbStep1.Text = "Step 1 : locating Minecraft or jar";
			// 
			// rdbExMinecraft
			// 
			this.rdbExMinecraft.AutoSize = true;
			this.rdbExMinecraft.Checked = true;
			this.rdbExMinecraft.Location = new System.Drawing.Point(6, 19);
			this.rdbExMinecraft.Name = "rdbExMinecraft";
			this.rdbExMinecraft.Size = new System.Drawing.Size(105, 17);
			this.rdbExMinecraft.TabIndex = 4;
			this.rdbExMinecraft.TabStop = true;
			this.rdbExMinecraft.Text = "Extract Minecraft";
			this.rdbExMinecraft.UseVisualStyleBackColor = true;
			this.rdbExMinecraft.CheckedChanged += new System.EventHandler(this.RdbExMinecraft_CheckedChanged);
			// 
			// rdbExJar
			// 
			this.rdbExJar.AutoSize = true;
			this.rdbExJar.Location = new System.Drawing.Point(117, 19);
			this.rdbExJar.Name = "rdbExJar";
			this.rdbExJar.Size = new System.Drawing.Size(88, 17);
			this.rdbExJar.TabIndex = 5;
			this.rdbExJar.Text = "Extract jar file";
			this.rdbExJar.UseVisualStyleBackColor = true;
			// 
			// lnkAbout
			// 
			this.lnkAbout.AutoSize = true;
			this.lnkAbout.Location = new System.Drawing.Point(589, 377);
			this.lnkAbout.Name = "lnkAbout";
			this.lnkAbout.Size = new System.Drawing.Size(44, 13);
			this.lnkAbout.TabIndex = 6;
			this.lnkAbout.TabStop = true;
			this.lnkAbout.Text = "About...";
			this.lnkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkAbout_LinkClicked);
			// 
			// grbStep2
			// 
			this.grbStep2.Controls.Add(this.btnConfirm2);
			this.grbStep2.Controls.Add(this.cmbVersions);
			this.grbStep2.Enabled = false;
			this.grbStep2.Location = new System.Drawing.Point(12, 94);
			this.grbStep2.Name = "grbStep2";
			this.grbStep2.Size = new System.Drawing.Size(362, 51);
			this.grbStep2.TabIndex = 7;
			this.grbStep2.TabStop = false;
			this.grbStep2.Text = "Step 2 : version to extract";
			// 
			// btnConfirm2
			// 
			this.btnConfirm2.Location = new System.Drawing.Point(281, 17);
			this.btnConfirm2.Name = "btnConfirm2";
			this.btnConfirm2.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm2.TabIndex = 1;
			this.btnConfirm2.Text = "Confirm";
			this.btnConfirm2.UseVisualStyleBackColor = true;
			this.btnConfirm2.Click += new System.EventHandler(this.BtnConfirm2_Click);
			// 
			// cmbVersions
			// 
			this.cmbVersions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbVersions.FormattingEnabled = true;
			this.cmbVersions.Location = new System.Drawing.Point(6, 19);
			this.cmbVersions.Name = "cmbVersions";
			this.cmbVersions.Size = new System.Drawing.Size(269, 21);
			this.cmbVersions.TabIndex = 0;
			this.cmbVersions.SelectedIndexChanged += new System.EventHandler(this.CmbVersions_SelectedIndexChanged);
			// 
			// grbStep3
			// 
			this.grbStep3.Controls.Add(this.btnConfirm3);
			this.grbStep3.Controls.Add(this.chkExtGroups);
			this.grbStep3.Enabled = false;
			this.grbStep3.Location = new System.Drawing.Point(12, 151);
			this.grbStep3.Name = "grbStep3";
			this.grbStep3.Size = new System.Drawing.Size(362, 62);
			this.grbStep3.TabIndex = 8;
			this.grbStep3.TabStop = false;
			this.grbStep3.Text = "Step 3 : group(s) to extract";
			// 
			// btnConfirm3
			// 
			this.btnConfirm3.Location = new System.Drawing.Point(281, 30);
			this.btnConfirm3.Name = "btnConfirm3";
			this.btnConfirm3.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm3.TabIndex = 3;
			this.btnConfirm3.Text = "Confirm";
			this.btnConfirm3.UseVisualStyleBackColor = true;
			this.btnConfirm3.Click += new System.EventHandler(this.BtnConfirm3_Click);
			// 
			// chkExtGroups
			// 
			this.chkExtGroups.CheckOnClick = true;
			this.chkExtGroups.FormattingEnabled = true;
			this.chkExtGroups.Items.AddRange(new object[] {
            "minecraft.jar files",
            "assets files"});
			this.chkExtGroups.Location = new System.Drawing.Point(6, 19);
			this.chkExtGroups.Name = "chkExtGroups";
			this.chkExtGroups.Size = new System.Drawing.Size(269, 34);
			this.chkExtGroups.TabIndex = 0;
			this.chkExtGroups.SelectedIndexChanged += new System.EventHandler(this.ChkExtGroups_SelectedIndexChanged);
			// 
			// grbHelp
			// 
			this.grbHelp.Controls.Add(this.rtbHelp);
			this.grbHelp.Location = new System.Drawing.Point(380, 12);
			this.grbHelp.Name = "grbHelp";
			this.grbHelp.Size = new System.Drawing.Size(246, 358);
			this.grbHelp.TabIndex = 9;
			this.grbHelp.TabStop = false;
			this.grbHelp.Text = "Help";
			// 
			// rtbHelp
			// 
			this.rtbHelp.Location = new System.Drawing.Point(6, 19);
			this.rtbHelp.Name = "rtbHelp";
			this.rtbHelp.ReadOnly = true;
			this.rtbHelp.Size = new System.Drawing.Size(234, 333);
			this.rtbHelp.TabIndex = 0;
			this.rtbHelp.Text = "";
			// 
			// grbStep4
			// 
			this.grbStep4.Controls.Add(this.btnConfirm4);
			this.grbStep4.Controls.Add(this.chkExtFolders);
			this.grbStep4.Enabled = false;
			this.grbStep4.Location = new System.Drawing.Point(12, 219);
			this.grbStep4.Name = "grbStep4";
			this.grbStep4.Size = new System.Drawing.Size(362, 151);
			this.grbStep4.TabIndex = 9;
			this.grbStep4.TabStop = false;
			this.grbStep4.Text = "Step 4 : folder(s) from jar to extract";
			// 
			// btnConfirm4
			// 
			this.btnConfirm4.Location = new System.Drawing.Point(281, 122);
			this.btnConfirm4.Name = "btnConfirm4";
			this.btnConfirm4.Size = new System.Drawing.Size(75, 23);
			this.btnConfirm4.TabIndex = 3;
			this.btnConfirm4.Text = "Confirm";
			this.btnConfirm4.UseVisualStyleBackColor = true;
			this.btnConfirm4.Click += new System.EventHandler(this.BtnConfirm4_Click);
			// 
			// chkExtFolders
			// 
			this.chkExtFolders.CheckOnClick = true;
			this.chkExtFolders.Cursor = System.Windows.Forms.Cursors.Default;
			this.chkExtFolders.FormattingEnabled = true;
			this.chkExtFolders.Location = new System.Drawing.Point(6, 19);
			this.chkExtFolders.Name = "chkExtFolders";
			this.chkExtFolders.Size = new System.Drawing.Size(269, 124);
			this.chkExtFolders.TabIndex = 0;
			// 
			// FrmMre
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(638, 395);
			this.Controls.Add(this.grbStep4);
			this.Controls.Add(this.grbHelp);
			this.Controls.Add(this.lnkAbout);
			this.Controls.Add(this.grbStep3);
			this.Controls.Add(this.grbStep2);
			this.Controls.Add(this.grbStep1);
			this.Controls.Add(this.statusStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "FrmMre";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Minecraft Resource Extractor";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMre_FormClosed);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.grbStep1.ResumeLayout(false);
			this.grbStep1.PerformLayout();
			this.grbStep2.ResumeLayout(false);
			this.grbStep3.ResumeLayout(false);
			this.grbHelp.ResumeLayout(false);
			this.grbStep4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.LinkLabel lnkAbout;
		private System.Windows.Forms.GroupBox grbHelp;
		public System.Windows.Forms.GroupBox grbStep1;
		public System.Windows.Forms.RadioButton rdbExMinecraft;
		public System.Windows.Forms.TextBox txtPath;
		public System.Windows.Forms.RadioButton rdbExJar;
		public System.Windows.Forms.GroupBox grbStep2;
		public System.Windows.Forms.ComboBox cmbVersions;
		public System.Windows.Forms.GroupBox grbStep3;
		public System.Windows.Forms.GroupBox grbStep4;
		public System.Windows.Forms.ToolStripProgressBar pgbProgress;
		public System.Windows.Forms.ToolStripStatusLabel slbStatusLabel;
		public System.Windows.Forms.CheckedListBox chkExtGroups;
		public System.Windows.Forms.CheckedListBox chkExtFolders;
		public System.Windows.Forms.RichTextBox rtbHelp;
		public System.Windows.Forms.Button btnConfirm2;
		public System.Windows.Forms.Button btnConfirm3;
		public System.Windows.Forms.Button btnConfirm4;
		public System.Windows.Forms.Button btnLocateJar;
	}
}

