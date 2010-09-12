using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace NetScrot {
	static class Program {
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static netscrot nscrot = new netscrot();

		private static bool kprtscrn = false;
		private static bool klctrl = false;
		private static bool klshift = false;
#if USE_ALT_METHOD
        private static bool kalt = false;
#endif

		/// <summary>The GetForegroundWindow function returns a handle to the foreground window.</summary>
#if USE_ALT_METHOD
        [DllImport("user32.dll")]static extern IntPtr GetForegroundWindow( );
        [DllImport("gdi32.dll")]static extern bool GetWindowOrgEx(IntPtr hdc, out Point lpPoint);
#endif

		[STAThread]
		static void Main( ) {
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			nscrot.hook.HookedKeys.Add(Keys.PrintScreen);
			nscrot.hook.HookedKeys.Add(Keys.LControlKey);
			nscrot.hook.HookedKeys.Add(Keys.LShiftKey);
			nscrot.hook.KeyDown += new KeyEventHandler(hook_KeyDown);
			nscrot.hook.KeyUp += new KeyEventHandler(hook_KeyUp);
			nscrot.init();
			Application.Run();
		}
		
		public static void runUpload( ) {
			if (Properties.Settings.Default.firstrun)
				return;
			NameValueCollection postdata = new NameValueCollection();
			postdata.Add("u", Properties.Settings.Default.username);
			postdata.Add("p", Properties.Settings.Default.password);
			if (!Clipboard.ContainsImage() && !Clipboard.ContainsText()) {
				nscrot.balloonText("No image or URL in clipboard!", 2);
				return;
			}
			if (!Clipboard.ContainsText()) {
				Image scrt = Clipboard.GetImage();
				System.Threading.Thread upld = new System.Threading.Thread(( ) =>
				{
					nscrot.upload(postdata, scrt);
				});
				upld.SetApartmentState(System.Threading.ApartmentState.STA);
				upld.Start();
			} else {
				string lURL = Clipboard.GetText();
				System.Threading.Thread shrt = new System.Threading.Thread(( ) =>
				{
					nscrot.shorten(lURL);
				});
				shrt.SetApartmentState(System.Threading.ApartmentState.STA);
				shrt.Start();
			}
		}
		
		private static void hook_KeyUp(object sender, KeyEventArgs e) {
			kprtscrn = false;
			klctrl = false;
			klshift = false;
		}

		private static void hook_KeyDown(object sender, KeyEventArgs e) {
			if (Properties.Settings.Default.firstrun)
				return;
			if (e.KeyCode == Keys.PrintScreen)
				kprtscrn = true;
			if (e.KeyCode == Keys.LShiftKey)
				klshift = true;
			if (e.KeyCode == Keys.LControlKey)
				klctrl = true;
			if (kprtscrn && klctrl && klshift)
				runUpload();
		}
	}
}
