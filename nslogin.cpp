#include "nslogin.h"
#include "ui_nslogin.h"
#include <QMessageBox>

nsLogin::nsLogin(QWidget *parent) : QDialog(parent), ui(new Ui::nsLogin) {
    ui->setupUi(this);
	ui->nsBaseDomain->setText("maffl.es");

}

nsLogin::~nsLogin() {
    delete ui;
}

void nsLogin::on_btnCancel_clicked() {
	this->close();
}

void nsLogin::on_btnOK_clicked() {
	checkStrings();
	if((ui->nsBaseDomain->text() == "" || ui->nsUsername->text() == "" || ui->nsPassword->text() == "") || !validateBaseDomain(ui->nsBaseDomain->text())) {
		//Oh no!
		QMessageBox::critical(this, "Error", "Your username, password or base domain was not valid.");
	}

}

void nsLogin::checkStrings() {
	QString basedomain = ui->nsBaseDomain->text();
	/*QString username = ui->nsUsername->text();
	QString passwd = ui->nsPassword->text();*/
	if(basedomain != "") {
		if(basedomain.endsWith("/", Qt::CaseInsensitive))
			basedomain += "/";
		if(basedomain.startsWith("http://", Qt::CaseInsensitive))
			basedomain = "http://" + basedomain;
	}
	ui->nsBaseDomain->setText(basedomain);
}

bool nsLogin::validateBaseDomain(QString basedomain) {
	if(basedomain == "")
		return false;
	if(!basedomain.endsWith("/", Qt::CaseSensitive))
		return false;
	if(!basedomain.startsWith("http://", Qt::CaseSensitive))
		return false;
	return true;
}
