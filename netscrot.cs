using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

//I am well aware of the issue regarding the responsiveness of this software, but I really have no idea what's causing it.

//To whoever likes knowing how their upload is going, beyond just "0, 50%, 100%", I'm sorry but I can't understand how the hell to implement more accurate
//progress tracking using the WebClient class.

namespace NetScrot {
	class netscrot {
		public Utilities.globalKeyboardHook hook = new Utilities.globalKeyboardHook();
		public NotifyIcon tray = new NotifyIcon();
		private Microsoft.Win32.RegistryKey regSet = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
		private long maxupload = 12582912;
		public static bool isDroppedItemURL = false;
		public static string uri = "";
		private bool isCollectionPointOpen = false;

		public void uploadScreenshot(NameValueCollection pdata, System.Drawing.Image scrnshot) {
			Properties.Settings.Default.Reload();
			string sURL = Properties.Settings.Default.basedomain + "sApi";
			scrnshot.Save(Environment.CurrentDirectory + "\\temp.png", System.Drawing.Imaging.ImageFormat.Png);
			scrnshot.Dispose();
			WebClient r = new WebClient();
			r.Headers["User-Agent"] = "NetScrot/0.7";
			byte[] scrot = File.ReadAllBytes(Environment.CurrentDirectory + "\\temp.png");
			if (scrot.LongLength > maxupload) {
				balloonText("File too large to upload (internal limit is 12MB)", 2);
				return;
			}
			tray.Icon = Properties.Resources.uploading;
			tray.Text = "NetScrot - Uploading";
			pdata.Add("fupld", Convert.ToBase64String(scrot));
			r.UploadProgressChanged += new UploadProgressChangedEventHandler(r_UploadProgressChanged);
			r.UploadValuesCompleted += new UploadValuesCompletedEventHandler(r_UploadValuesCompleted);
			r.UploadValuesAsync(new Uri(sURL), "POST", pdata);
		}

		private void r_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e) {
			tray.Text = "NetScrot - Uploading (" + e.ProgressPercentage + "%)";
		}

		private void r_UploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e) {
			if (e.Error != null) {
				WebException ex = (WebException)e.Error;
				if (ex.Status == WebExceptionStatus.ProtocolError) {
					HttpWebResponse rsp = (HttpWebResponse) ex.Response;
					if (rsp.StatusCode == HttpStatusCode.Forbidden)
						balloonText("Username/Password invalid.", 2);
					else if (rsp.StatusCode == HttpStatusCode.BadRequest)
						balloonText("File upload failed.", 2);
					else if (rsp.StatusCode == HttpStatusCode.ServiceUnavailable)
						balloonText("Could not contact Slurp database.", 2);
					else if ((int) rsp.StatusCode == 418)
						balloonText("Server is a teapot.", 3);
					else
						balloonText("Error: " + (int) rsp.StatusCode, 2);
				} else
					balloonText("File may not have been uploaded. Error was: " + ex.Message, 1);
				tray.Icon = Properties.Resources.idle;
				return;
			}
			tray.Icon = Properties.Resources.idle;
			tray.Text = "NetScrot";
			string rspns = Encoding.UTF8.GetString(e.Result);
			if (rspns.Length != 4) {
				balloonText("File upload may have failed: " + rspns, 1);
			}
			clipboardContentSet(Properties.Settings.Default.basedomain + rspns);
			balloonText("File uploaded, URL copied to your clipboard - " + Properties.Settings.Default.basedomain + rspns);
		}

		public void uploadFile(NameValueCollection pdata, string fileuri) {
			Properties.Settings.Default.Reload();
			string sURL = Properties.Settings.Default.basedomain + "sApi";
			string baseURL = Properties.Settings.Default.basedomain;
			pdata.Add("fn", Path.GetFileName(fileuri));
			if (new FileInfo(fileuri).Length > maxupload) {
				balloonText("File too large to upload (internal limit is 12MB)", 2);
				return;
			}
			//Checking filesize before reading the file prevents it from freaking the shit out if you give it a 25gb file.
			byte[] upfile = File.ReadAllBytes(fileuri);
			tray.Icon = Properties.Resources.uploading;
			tray.Text = "NetScrot - Uploading";
			WebClient r = new WebClient();
			r.Headers["User-Agent"] = "NetScrot/0.6";
			pdata.Add("fupld", Convert.ToBase64String(upfile));
			r.UploadProgressChanged += new UploadProgressChangedEventHandler(r_FileUploadProgressChanged);
			r.UploadValuesCompleted += new UploadValuesCompletedEventHandler(r_FileUploadValuesCompleted);
			r.UploadValuesAsync(new Uri(sURL), "POST", pdata);
		}

		private void r_FileUploadProgressChanged(object sender, UploadProgressChangedEventArgs e) {
			tray.Text = "NetScrot - Uploading (" + e.ProgressPercentage + "%)";
		}

		private void r_FileUploadValuesCompleted(object sender, UploadValuesCompletedEventArgs e) {
			if (e.Error != null) {
				WebException ex = (WebException) e.Error;
				if (ex.Status == WebExceptionStatus.ProtocolError) {
					HttpWebResponse rsp = (HttpWebResponse) ex.Response;
					if (rsp.StatusCode == HttpStatusCode.Forbidden)
						balloonText("Username/Password invalid.", 2);
					else if (rsp.StatusCode == HttpStatusCode.BadRequest)
						balloonText("File upload failed.", 2);
					else if (rsp.StatusCode == HttpStatusCode.ServiceUnavailable)
						balloonText("Could not contact Slurp database.", 2);
					else if ((int) rsp.StatusCode == 418)
						balloonText("Server is a teapot.", 3);
					else
						balloonText("Error: " + (int) rsp.StatusCode, 2);
				} else
					balloonText("File may not have been uploaded. Error was: " + ex.Message, 1);
				tray.Icon = Properties.Resources.idle;
				return;
			}
			tray.Icon = Properties.Resources.idle;
			tray.Text = "NetScrot";
			string rspns = Encoding.UTF8.GetString(e.Result);
			if (rspns.Length != 4) {
				balloonText("File upload may have failed: " + rspns, 1);
			}
			clipboardContentSet(Properties.Settings.Default.basedomain + rspns);
			balloonText("File uploaded, URL copied to your clipboard - " + Properties.Settings.Default.basedomain + rspns);
		}

		public void shorten(string lURL) {
			Properties.Settings.Default.Reload();
			string sURL = Properties.Settings.Default.basedomain + "sApi";
			string baseURL = Properties.Settings.Default.basedomain;
			WebClient r = new WebClient();
			string result = "";
			string b64URL = Convert.ToBase64String(Encoding.UTF8.GetBytes(lURL));
			try {
				result = r.DownloadString(sURL + "?b64=1&u=" + b64URL);
			} catch (WebException e) {
				if (e.Status == WebExceptionStatus.ProtocolError) {
					HttpWebResponse rsp = (HttpWebResponse) e.Response;
					if (rsp.StatusCode == HttpStatusCode.BadRequest) {
						balloonText("Invalid URL", 2);
						return;
					} else if (rsp.StatusCode == HttpStatusCode.ServiceUnavailable) {
						balloonText("Could not shorten URL: Server error", 2);
						return;
					} else if (rsp.StatusCode == HttpStatusCode.InternalServerError) {
						balloonText("Could not shorten URL: Server error. Server may be down or broken.", 2);
						return;
					} else if ((int) rsp.StatusCode == 418) {
						balloonText("Server is a teapot.", 3);
						return;
					} else {
						balloonText("Unknown error: " + (int) rsp.StatusCode, 2);
						return;
					}
				} else {
					balloonText("URL may not have been shortened. Error was: " + e.Message, 1);
					return;
				}
			}
			if(result != "")
				Clipboard.SetText(result);
			balloonText("URL shortened, URL copied to your clipboard - " + result);
		}

		public void upload(NameValueCollection pdata) {
			if (isDroppedItemURL)
				shorten(uri);
			else
				uploadFile(pdata, uri);
			isDroppedItemURL = false;
			uri = "";
		}

		public void balloonText(string notifyText) {
			balloonText(notifyText, 0);
		}

		public void clipboardContentSet(string setContent) {
			Clipboard.SetText(setContent);
		}

		public void balloonText(string notifyText, int warningLevel) {
			switch (warningLevel) {
				case 0:
					tray.ShowBalloonTip(500, "NetScrot", notifyText, ToolTipIcon.Info);
					break;
				case 1:
					tray.ShowBalloonTip(1000, "Warning - NetScrot", notifyText, ToolTipIcon.Warning);
					break;
				case 2:
					tray.ShowBalloonTip(5000, "Error - NetScrot", notifyText, ToolTipIcon.Error);
					break;
				default:
					tray.ShowBalloonTip(2500, "?!", notifyText, ToolTipIcon.None);
					break;
			}
		}

		public void init( ) {
			tray.Text = "NetScrot";
			tray.Visible = true;
			tray.Icon = Properties.Resources.idle;
			Application.ApplicationExit += new EventHandler(quitting);
			if (Properties.Settings.Default.firstrun) {
				getSettings();
			}
			tray.ContextMenu = new ContextMenu(new MenuItem[] {
				new MenuItem("&Login Settings", (s, e) => { getSettings(); }),
				new MenuItem("&Start with Windows", (s, e) => { swapStartup(); }),
				new MenuItem("-"),
				new MenuItem("&About NetScrot", (s, e) => { MessageBox.Show("NetScrot is a small utility which uploads screenshots/images to a Slurp (or API-compatible) install on the internet.\nThis application can also be used to shorten URLs in the same manner.\nThis application makes use of icons from the Tango project.", "About NetScrot", MessageBoxButtons.OK); }),
				new MenuItem("&Quit", (s, e) => { tray.Visible = false; tray.Dispose(); Environment.Exit(0); })
			});
			tray.ContextMenu.MenuItems[1].Checked = getStartup();
			tray.MouseClick += new MouseEventHandler(tray_MouseClick);
		}

		private void tray_MouseClick(object sender, MouseEventArgs e) {
			if (e.Button != MouseButtons.Left)
				return;
			if (Properties.Settings.Default.firstrun)
				return;
			if (isCollectionPointOpen)
				return;
			dragDropToast tmpToast = new dragDropToast();
			tmpToast.Top = Screen.PrimaryScreen.WorkingArea.Height - (tmpToast.Height + 5);
			tmpToast.Left = Screen.PrimaryScreen.WorkingArea.Width - (tmpToast.Width + 5);
			isCollectionPointOpen = true;
			DialogResult rslt = tmpToast.ShowDialog();
			tmpToast.Dispose();
			isCollectionPointOpen = false;
			if (rslt != DialogResult.OK) {
				if (rslt == DialogResult.Abort)
					balloonText("NetScrot only accepts files, images and URLs. Please don't attempt to upload an entire folder.", 2);
				return;
			}
			NameValueCollection postdata = new NameValueCollection();
			postdata.Add("u", Properties.Settings.Default.username);
			postdata.Add("p", Properties.Settings.Default.password);
			System.Threading.Thread uthrd = new System.Threading.Thread(( ) => {
				upload(postdata);
			});
			uthrd.SetApartmentState(System.Threading.ApartmentState.STA);
			uthrd.Start();
		}

		private void quitting(object sender, EventArgs e) {
			tray.Visible = false;
			tray.Dispose();
		}

		private void swapStartup( ) {
			if (getStartup())
				regSet.DeleteValue("NetScrot");
			else
				regSet.SetValue("NetScrot", @"""" + Application.ExecutablePath + @"""");
			tray.ContextMenu.MenuItems[1].Checked = getStartup();
		}

		private bool getStartup( ) {
			if (regSet.GetValue("NetScrot") != null)
				return true;
			else
				return false;
		}

		private void getSettings( ) {
			Form settings = new frmUsrSettings();
			if (settings.ShowDialog() == DialogResult.OK)
				settings.Dispose();
			Properties.Settings.Default.Reload();
		}

		static public bool verifyBaseDomain(string baseDomain) {
			Regex urlValidator = new Regex(@"^(http|https|sftp|ftp)://(([\w-]+)\.([a-z]{2,10}))+([\w\.\?&_/=%\-\[\]\+#])*\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			return urlValidator.IsMatch(baseDomain);
		}
	}
}
