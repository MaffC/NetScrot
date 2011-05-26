#include "nssettings.h"
#include <QFile>

//Initialise netscrot settings class
nsConf settings;

nsSettings::nsSettings() {
	//Load settings from file
	QFile nsConfFile("~/");
	nsConfFile.open()
}
