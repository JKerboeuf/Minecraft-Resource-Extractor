namespace mre.view
{
	partial class FrmAbout
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
			this.btnDonate = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnWebsite = new System.Windows.Forms.Button();
			this.lblTitre = new System.Windows.Forms.Label();
			this.picLogo = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblCreator = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// btnDonate
			// 
			this.btnDonate.Location = new System.Drawing.Point(127, 89);
			this.btnDonate.Name = "btnDonate";
			this.btnDonate.Size = new System.Drawing.Size(75, 23);
			this.btnDonate.TabIndex = 0;
			this.btnDonate.Text = "Donate";
			this.btnDonate.UseVisualStyleBackColor = true;
			this.btnDonate.Click += new System.EventHandler(this.BtnDonate_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(289, 89);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
			// 
			// btnWebsite
			// 
			this.btnWebsite.Location = new System.Drawing.Point(208, 89);
			this.btnWebsite.Name = "btnWebsite";
			this.btnWebsite.Size = new System.Drawing.Size(75, 23);
			this.btnWebsite.TabIndex = 2;
			this.btnWebsite.Text = "Web Page";
			this.btnWebsite.UseVisualStyleBackColor = true;
			this.btnWebsite.Click += new System.EventHandler(this.BtnWebsite_Click);
			// 
			// lblTitre
			// 
			this.lblTitre.AutoSize = true;
			this.lblTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblTitre.Location = new System.Drawing.Point(118, 12);
			this.lblTitre.Name = "lblTitre";
			this.lblTitre.Size = new System.Drawing.Size(253, 20);
			this.lblTitre.TabIndex = 4;
			this.lblTitre.Text = "Minecraft Resource Extractor";
			// 
			// picLogo
			// 
			this.picLogo.Image = global::mre.Properties.Resources.mre;
			this.picLogo.InitialImage = global::mre.Properties.Resources.mre;
			this.picLogo.Location = new System.Drawing.Point(12, 12);
			this.picLogo.Name = "picLogo";
			this.picLogo.Size = new System.Drawing.Size(100, 100);
			this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.picLogo.TabIndex = 5;
			this.picLogo.TabStop = false;
			// 
			// lblVersion
			// 
			this.lblVersion.AutoSize = true;
			this.lblVersion.Location = new System.Drawing.Point(119, 41);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(60, 13);
			this.lblVersion.TabIndex = 6;
			this.lblVersion.Text = "Version 2.0";
			// 
			// lblCreator
			// 
			this.lblCreator.AutoSize = true;
			this.lblCreator.Location = new System.Drawing.Point(119, 63);
			this.lblCreator.Name = "lblCreator";
			this.lblCreator.Size = new System.Drawing.Size(134, 13);
			this.lblCreator.TabIndex = 7;
			this.lblCreator.Text = "Created by Julien Kerboeuf";
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(376, 123);
			this.Controls.Add(this.picLogo);
			this.Controls.Add(this.lblCreator);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblTitre);
			this.Controls.Add(this.btnWebsite);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDonate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(392, 162);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(392, 162);
			this.Name = "FrmAbout";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnDonate;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnWebsite;
		private System.Windows.Forms.Label lblTitre;
		private System.Windows.Forms.PictureBox picLogo;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCreator;
	}
}