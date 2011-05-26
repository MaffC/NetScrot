#ifndef NSSETTINGS_H
#define NSSETTINGS_H

class nsSettings {

public:
	nsSettings();

private:
	struct nsConf {
		QString username;
		QString passwd;
		QString basedomain;
		bool firstrun;
	};

};

#endif // NSSETTINGS_H
