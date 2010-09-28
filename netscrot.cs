﻿using System;
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

namespace NetScrot {
	class netscrot {
		public Utilities.globalKeyboardHook hook = new Utilities.globalKeyboardHook();
		public NotifyIcon tray = new NotifyIcon();
		private Microsoft.Win32.RegistryKey regSet = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

		public void upload(NameValueCollection pdata, System.Drawing.Image scrnshot) {
			Properties.Settings.Default.Reload();
			string sURL = Properties.Settings.Default.basedomain + "sApi";
			string baseURL = Properties.Settings.Default.basedomain;
			scrnshot.Save(Environment.CurrentDirectory + "\\temp.png", System.Drawing.Imaging.ImageFormat.Png);
			scrnshot.Dispose();
			tray.Icon = Properties.Resources.uploading;
			WebClient r = new WebClient();
			r.Headers["User-Agent"] = "NetScrot/0.6";
			byte[] scrot = File.ReadAllBytes(Environment.CurrentDirectory + "\\temp.png");
			pdata.Add("fupld", Convert.ToBase64String(scrot));
			byte[] resp = null;
			try {
				resp = r.UploadValues(sURL, "POST", pdata);
			} catch (WebException e) {
				if (e.Status == WebExceptionStatus.ProtocolError) {
					HttpWebResponse rsp = (HttpWebResponse) e.Response;
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
					balloonText("File may not have been uploaded. Error was: " + e.Message, 1);
				tray.Icon = Properties.Resources.idle;
				return;
			}
			tray.Icon = Properties.Resources.idle;
			string rspns = Encoding.UTF8.GetString(resp);
			if (rspns.Length != 4) {
				balloonText("File upload may have failed: " + rspns, 1);
			}
			Clipboard.SetText(baseURL + rspns);
			balloonText("File uploaded, URL copied to your clipboard - " + baseURL + rspns);
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

		public void balloonText(string notifyText) {
			balloonText(notifyText, 0);
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