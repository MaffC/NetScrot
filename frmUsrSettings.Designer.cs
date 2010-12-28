namespace NetScrot {
	partial class frmUsrSettings {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent( ) {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUsrSettings));
			this.grpUser = new System.Windows.Forms.GroupBox();
			this.spltBaseDomain = new System.Windows.Forms.SplitContainer();
			this.lblBaseDomain = new System.Windows.Forms.Label();
			this.txtBaseDomain = new System.Windows.Forms.TextBox();
			this.spltUsrPass = new System.Windows.Forms.SplitContainer();
			this.spltUsr = new System.Windows.Forms.SplitContainer();
			this.lblUsername = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.spltPassword = new System.Windows.Forms.SplitContainer();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.lblExplain = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.grpUser.SuspendLayout();
			this.spltBaseDomain.Panel1.SuspendLayout();
			this.spltBaseDomain.Panel2.SuspendLayout();
			this.spltBaseDomain.SuspendLayout();
			this.spltUsrPass.Panel1.SuspendLayout();
			this.spltUsrPass.Panel2.SuspendLayout();
			this.spltUsrPass.SuspendLayout();
			this.spltUsr.Panel1.SuspendLayout();
			this.spltUsr.Panel2.SuspendLayout();
			this.spltUsr.SuspendLayout();
			this.spltPassword.Panel1.SuspendLayout();
			this.spltPassword.Panel2.SuspendLayout();
			this.spltPassword.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpUser
			// 
			this.grpUser.Controls.Add(this.spltBaseDomain);
			this.grpUser.Controls.Add(this.spltUsrPass);
			this.grpUser.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpUser.Location = new System.Drawing.Point(0, 0);
			this.grpUser.Name = "grpUser";
			this.grpUser.Size = new System.Drawing.Size(292, 87);
			this.grpUser.TabIndex = 0;
			this.grpUser.TabStop = false;
			this.grpUser.Text = "Login Settings";
			// 
			// spltBaseDomain
			// 
			this.spltBaseDomain.IsSplitterFixed = true;
			this.spltBaseDomain.Location = new System.Drawing.Point(3, 64);
			this.spltBaseDomain.Name = "spltBaseDomain";
			// 
			// spltBaseDomain.Panel1
			// 
			this.spltBaseDomain.Panel1.Controls.Add(this.lblBaseDomain);
			// 
			// spltBaseDomain.Panel2
			// 
			this.spltBaseDomain.Panel2.Controls.Add(this.txtBaseDomain);
			this.spltBaseDomain.Size = new System.Drawing.Size(286, 20);
			this.spltBaseDomain.SplitterDistance = 60;
			this.spltBaseDomain.TabIndex = 1;
			this.spltBaseDomain.TabStop = false;
			// 
			// lblBaseDomain
			// 
			this.lblBaseDomain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblBaseDomain.Location = new System.Drawing.Point(0, 0);
			this.lblBaseDomain.Name = "lblBaseDomain";
			this.lblBaseDomain.Size = new System.Drawing.Size(60, 20);
			this.lblBaseDomain.TabIndex = 0;
			this.lblBaseDomain.Text = "Base URL";
			this.lblBaseDomain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtBaseDomain
			// 
			this.txtBaseDomain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtBaseDomain.Location = new System.Drawing.Point(0, 0);
			this.txtBaseDomain.Name = "txtBaseDomain";
			this.txtBaseDomain.Size = new System.Drawing.Size(222, 20);
			this.txtBaseDomain.TabIndex = 0;
			// 
			// spltUsrPass
			// 
			this.spltUsrPass.Dock = System.Windows.Forms.DockStyle.Top;
			this.spltUsrPass.IsSplitterFixed = true;
			this.spltUsrPass.Location = new System.Drawing.Point(3, 16);
			this.spltUsrPass.Name = "spltUsrPass";
			this.spltUsrPass.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// spltUsrPass.Panel1
			// 
			this.spltUsrPass.Panel1.Controls.Add(this.spltUsr);
			this.spltUsrPass.Panel1MinSize = 20;
			// 
			// spltUsrPass.Panel2
			// 
			this.spltUsrPass.Panel2.Controls.Add(this.spltPassword);
			this.spltUsrPass.Panel2MinSize = 20;
			this.spltUsrPass.Size = new System.Drawing.Size(286, 44);
			this.spltUsrPass.SplitterDistance = 20;
			this.spltUsrPass.TabIndex = 0;
			this.spltUsrPass.TabStop = false;
			// 
			// spltUsr
			// 
			this.spltUsr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spltUsr.IsSplitterFixed = true;
			this.spltUsr.Location = new System.Drawing.Point(0, 0);
			this.spltUsr.Name = "spltUsr";
			// 
			// spltUsr.Panel1
			// 
			this.spltUsr.Panel1.Controls.Add(this.lblUsername);
			// 
			// spltUsr.Panel2
			// 
			this.spltUsr.Panel2.Controls.Add(this.txtUsername);
			this.spltUsr.Size = new System.Drawing.Size(286, 20);
			this.spltUsr.SplitterDistance = 60;
			this.spltUsr.TabIndex = 0;
			this.spltUsr.TabStop = false;
			// 
			// lblUsername
			// 
			this.lblUsername.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblUsername.Location = new System.Drawing.Point(0, 0);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(60, 20);
			this.lblUsername.TabIndex = 0;
			this.lblUsername.Text = "Username";
			this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtUsername
			// 
			this.txtUsername.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtUsername.Location = new System.Drawing.Point(0, 0);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(222, 20);
			this.txtUsername.TabIndex = 0;
			// 
			// spltPassword
			// 
			this.spltPassword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spltPassword.IsSplitterFixed = true;
			this.spltPassword.Location = new System.Drawing.Point(0, 0);
			this.spltPassword.Name = "spltPassword";
			// 
			// spltPassword.Panel1
			// 
			this.spltPassword.Panel1.Controls.Add(this.lblPassword);
			// 
			// spltPassword.Panel2
			// 
			this.spltPassword.Panel2.Controls.Add(this.txtPass);
			this.spltPassword.Size = new System.Drawing.Size(286, 20);
			this.spltPassword.SplitterDistance = 60;
			this.spltPassword.TabIndex = 0;
			this.spltPassword.TabStop = false;
			// 
			// lblPassword
			// 
			this.lblPassword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblPassword.Location = new System.Drawing.Point(0, 0);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(60, 20);
			this.lblPassword.TabIndex = 0;
			this.lblPassword.Text = "Password";
			this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtPass
			// 
			this.txtPass.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtPass.Location = new System.Drawing.Point(0, 0);
			this.txtPass.Name = "txtPass";
			this.txtPass.Size = new System.Drawing.Size(222, 20);
			this.txtPass.TabIndex = 0;
			this.txtPass.UseSystemPasswordChar = true;
			// 
			// lblExplain
			// 
			this.lblExplain.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (0)));
			this.lblExplain.ForeColor = System.Drawing.Color.FromArgb(((int) (((byte) (64)))), ((int) (((byte) (64)))), ((int) (((byte) (64)))));
			this.lblExplain.Location = new System.Drawing.Point(12, 90);
			this.lblExplain.Name = "lblExplain";
			this.lblExplain.Size = new System.Drawing.Size(268, 61);
			this.lblExplain.TabIndex = 1;
			this.lblExplain.Text = resources.GetString("lblExplain.Text");
			this.lblExplain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(174, 154);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(106, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.TabStop = false;
			this.btnSave.Text = "Save Settings";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// frmUsrSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 185);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.lblExplain);
			this.Controls.Add(this.grpUser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmUsrSettings";
			this.Text = "NetScrot Settings";
			this.grpUser.ResumeLayout(false);
			this.spltBaseDomain.Panel1.ResumeLayout(false);
			this.spltBaseDomain.Panel2.ResumeLayout(false);
			this.spltBaseDomain.Panel2.PerformLayout();
			this.spltBaseDomain.ResumeLayout(false);
			this.spltUsrPass.Panel1.ResumeLayout(false);
			this.spltUsrPass.Panel2.ResumeLayout(false);
			this.spltUsrPass.ResumeLayout(false);
			this.spltUsr.Panel1.ResumeLayout(false);
			this.spltUsr.Panel2.ResumeLayout(false);
			this.spltUsr.Panel2.PerformLayout();
			this.spltUsr.ResumeLayout(false);
			this.spltPassword.Panel1.ResumeLayout(false);
			this.spltPassword.Panel2.ResumeLayout(false);
			this.spltPassword.Panel2.PerformLayout();
			this.spltPassword.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox grpUser;
		private System.Windows.Forms.SplitContainer spltUsrPass;
		private System.Windows.Forms.SplitContainer spltUsr;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.SplitContainer spltPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtPass;
		private System.Windows.Forms.Label lblExplain;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.SplitContainer spltBaseDomain;
		private System.Windows.Forms.Label lblBaseDomain;
		private System.Windows.Forms.TextBox txtBaseDomain;
	}
}

