require 'Win32API'

#cOpen = Win32API.new("user32","OpenClipboard",['L'],'L')
#cData = Win32API.new("user32","GetClipboardData",['L'],'L')
#cClose = Win32API.new("user32","CloseClipboard",['L'],'L')
#gLock = Win32API.new("kernel32","GlobalLock",['L'],'P')
#gUnlock = Win32API.new("kernel32","GlobalUnlock",['L'],'L')

#cOpen.Call(0)
#toOutput = gLock.Call(cData.Call(1))
#gUnlock.Call(cData.Call(1))
#cClose.Call(0)

#puts toOutput

#Win32API.new('user32','MessageBox',['L','P','P','L'],'I').call(0,"Hello world!","Ruby Testmessage",0)

class Clipboard
	CF_TEXT = 1
	CF_BITMAP = 2
	CF_METAFILEPICT = 3
	CF_SYLK = 4
	CF_DIF = 5
	CF_TIFF = 6
	CF_OEMTEXT = 7
	CF_DIB = 8
	CF_PALETTE = 9
	CF_PENDATA = 10
	CF_RIFF = 11
	CF_WAVE = 12
	CF_UNICODETEXT = 13
	CF_ENHMETAFILE = 14
	CF_HDROP = 15
	CF_LOCALE = 16
	CF_MAX = 17

	CF_OWNERDISPLAY = 0x0080
	CF_DSPTEXT = 0x0081
	CF_DSPBITMAP = 0x0082
	CF_DSPMETAFILEPICT  = 0x0083
	CF_DSPENHMETAFILE = 0x008E

	CF_PRIVATEFIRST = 0x0200
	CF_PRIVATELAST = 0x02FF

	CF_GDIOBJFIRST = 0x0300
	CF_GDIOBJLAST = 0x03FF

	GMEM_MOVEABLE = 0x0002

	@@openClipboard = Win32API.new('user32', 'OpenClipboard', ['L'], 'I')
	@@closeClipboard = Win32API.new('user32', 'CloseClipboard', [], 'I')
	@@getClipboardData = Win32API.new('user32', 'GetClipboardData', ['I'], 'P')
	@@setClipboardData = Win32API.new('user32', 'SetClipboardData', ['I', 'I'], 'I')
	@@isClipboardFormatAvailable = Win32API.new('user32', 'IsClipboardFormatAvailable', ['I'], 'I')
	@@emptyClipboard = Win32API.new('user32', 'EmptyClipboard', [], 'I')
	@@globalAlloc = Win32API.new('kernel32', 'GlobalAlloc', ['I', 'I'], 'I')
	@@globalLock = Win32API.new('kernel32', 'GlobalLock', ['I'], 'I')
	@@globalUnlock = Win32API.new('kernel32', 'GlobalUnlock', ['I'], 'I')
	@@memcpy = Win32API.new('msvcrt', 'memcpy', ['I', 'P', 'I'], 'I')

	def open
		raise RuntimeError('Can not open clipboard') unless @@openClipboard.Call(0) != 0
	end

	def close
		@@closeClipboard.Call()
	end

	def data(format = CF_TEXT)
		@@getClipboardData.Call(format)
	end

	def hasFormat?(format)
		@@isClipboardFormatAvailable.Call(format) != 0
	end

	def data=(data)
		setData(data)
		data
	end

	def setData(data, type = CF_TEXT)
		# Empty the clipboard
		@@emptyClipboard.Call

		# Null terminate
		data << 0 if type == CF_TEXT

		# Global Allocate a movable piece of memory.
		hmem = @@globalAlloc.Call(GMEM_MOVEABLE, data.length)
		mem = @@globalLock.Call(hmem)
		@@memcpy.Call(mem, data, data.length)
		@@globalUnlock.Call(hmem)

		# Set the new data
		@@setClipboardData.Call(type, hmem) != 0
	end
end

cB = Clipboard.new
cB.open
if cB.hasFormat?(Clipboard::CF_BITMAP)
	File.open('bitmap.bmp','w') {|f| f.write(cB.data(Clipboard::CF_BITMAP)) }
end
cB.close