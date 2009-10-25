import win32clipboard as w
import win32con

def getText():
	w.OpenClipboard()
	d = w.GetClipboardData(win32con.CF_TEXT)
	w.CloseClipboard()
	return d

def setText(aType,aString):
	w.OpenClipboard()
	w.EmptyClipboard()
	w.SetClipboardData(aType,aString)
	w.CloseClipboard

print getText()