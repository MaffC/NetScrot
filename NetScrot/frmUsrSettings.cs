using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NetScrot {
	public partial class frmUsrSettings : Form {
		public frmUsrSettings( ) {
			InitializeComponent();
			this.KeyUp += new KeyEventHandler(keyup);
			this.FormClosing += new FormClosingEventHandler(closeify);
			this.Load += new EventHandler(loadify);
			this.Icon = Properties.Resources.idle;
		}

		public void loadify(object sender, EventArgs e) {
			txtUsername.Text = Properties.Settings.Default.username;
			txtPass.Text = "Password";
		}

		public void closeify(object sender, FormClosingEventArgs e) {
			if (Properties.Settings.Default.username.Length == 0 || Properties.Settings.Default.password.Length == 0 || Properties.Settings.Default.firstrun) {
				e.Cancel = true;
				MessageBox.Show("Please enter your login information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			} else {
				this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				this.Dispose();
			}
		}

		public void keyup(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter && (!e.Alt && !e.Control && !e.Shift))
				returnToSender();
		}

		private void btnSave_Click(object sender, EventArgs e) {
			returnToSender();
		}

		private void returnToSender( ) {
			if (txtUsername.Text.Length == 0) {
				MessageBox.Show("Please enter your username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (txtPass.Text == "Password" && Properties.Settings.Default.password.Length == 0) {
				MessageBox.Show("Please enter your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (txtUsername.Text != Properties.Settings.Default.username)
				Properties.Settings.Default.username = txtUsername.Text;
			if (txtPass.Text != "Password" && txtPass.Text != Properties.Settings.Default.password)
				Properties.Settings.Default.password = txtPass.Text;
			if (Properties.Settings.Default.firstrun)
				Properties.Settings.Default.firstrun = false;
			Properties.Settings.Default.Save();
			Properties.Settings.Default.Reload();
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Dispose();
		}
	}
}
