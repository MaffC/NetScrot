using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NetScrot {
	public partial class dragDropToast : Form {
		protected override CreateParams CreateParams {
			get {
				CreateParams cp = base.CreateParams;
				// turn on WS_EX_TOOLWINDOW style bit
				cp.ExStyle |= 0x80;
				return cp;
			}
		}

		public dragDropToast( ) {
			InitializeComponent();
			this.DragDrop += new DragEventHandler(dragDropToast_DragDrop);
			this.DragEnter += new DragEventHandler(dragDropToast_DragEnter);
			this.DragLeave += new EventHandler(dragDropToast_DragLeave);
			this.MouseClick += new MouseEventHandler(dragDropToast_MouseClick);
			this.MouseEnter += new EventHandler(dragDropToast_MouseEnter);
			this.MouseLeave += new EventHandler(dragDropToast_MouseLeave);
			this.FormClosing += new FormClosingEventHandler(dragDropToast_FormClosing);
			this.Shown += new EventHandler(dragDropToast_Shown);
			opacityShift(0.0);
		}

		private void dragDropToast_MouseEnter(object sender, EventArgs e) {
			opacityShift(0.75);
		}

		private void dragDropToast_MouseLeave(object sender, EventArgs e) {
			opacityShift(0.55);
		}

		private void dragDropToast_Shown(object sender, EventArgs e) {
			opacityShift(0.55);
		}

		private void dragDropToast_FormClosing(object sender, FormClosingEventArgs e) {
			opacityShift(0.0);
		}

		private void dragDropToast_DragLeave(object sender, EventArgs e) {
			opacityShift(0.55);
		}

		private void dragDropToast_MouseClick(object sender, MouseEventArgs e) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void dragDropToast_DragEnter(object sender, DragEventArgs e) {
			opacityShift(0.85);
			if (e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetDataPresent(DataFormats.Text))
				e.Effect = DragDropEffects.Copy;
			else
				e.Effect = DragDropEffects.None;
		}

		private void opacityShift(double newOpacity) {
			if (newOpacity > this.Opacity) {
				while (this.Opacity < newOpacity) {
					System.Threading.Thread.Sleep(3);
					this.Opacity += 0.01;
				}
			} else if (newOpacity < this.Opacity) {
				while (this.Opacity > newOpacity) {
					System.Threading.Thread.Sleep(3);
					this.Opacity -= 0.01;
				}
			}
		}

		private void dragDropToast_DragDrop(object sender, DragEventArgs e) {
			string tmpURL;
			string[] tmpFileArray;
			if (e.Data.GetDataPresent(DataFormats.Text)) {
				netscrot.isDroppedItemURL = true;
				tmpURL = (string) e.Data.GetData(DataFormats.Text);
				netscrot.uri = tmpURL;
			} else {
				if (new FileInfo(((string[]) e.Data.GetData(DataFormats.FileDrop))[0]).Extension == "" && Directory.Exists(((string[]) e.Data.GetData(DataFormats.FileDrop))[0])) {
					this.DialogResult = System.Windows.Forms.DialogResult.Abort;
					return;
				}
				tmpFileArray = (string[]) e.Data.GetData(DataFormats.FileDrop, false);
				netscrot.isDroppedItemURL = false;
				//I refuse to handle multi-file uploads. As such only the first file gets uploaded.
				netscrot.uri = tmpFileArray[0];
			}
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}
