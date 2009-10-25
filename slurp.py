import os, sys
import ImageGrab
import Image
import urllib
import urllib2
import win32clipboard
import wx

#Slurp Client settings

slurpUsername = ''
slurpPassword = ''
slurpDomain = ''

#End settings

im = ImageGrab.grabclipboard()
if isinstance(im, Image.Image):
	#WE HAVE IMAGE.
	im.save("image.png")
	imgH = open('image.png','rb')
	img = imgH.read()
	uData = {'fupld': img, 'u': slurpUsername, 'p': slurpPassword }
	encData = urllib.urlencode(uData)
	req = urllib2.Request('http://%s/sApi' % slurpDomain)
	req.add_data(encData)
	req.add_header('User-agent', 'pySlurp')
	#req.add_header('Content-Type', 'image/png')
	reqdata = urllib2.urlopen(req)
	#if reqdata.info() == 'HTTP/1.1 200 OK':
	print reqdata.info()
	retrn = reqdata.read()
	win32clipboard.OpenClipboard()
	win32clipboard.EmptyClipboard()
	win32clipboard.SetClipboardText("http://%s/%s" % (slurpDomain,retrn))
	win32clipboard.CloseClipboard()
	print "Upload completed, url is: http://%s/%s" % (slurpDomain,retrn)
	#else:
	#	pui = wx.PySimpleApp()
	#	dlg = wx.MessageDialog(1, "Could not upload the image. Your screenshot is safely stored.", "Upload error", wx.OK | wx.ICON_WARNING)
	#	dlg.ShowModal()
	#	dlg.Destroy()