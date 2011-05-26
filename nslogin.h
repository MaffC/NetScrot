#ifndef NSLOGIN_H
#define NSLOGIN_H

#include <QDialog>

namespace Ui {
    class nsLogin;
}

class nsLogin : public QDialog
{
    Q_OBJECT

public:
    explicit nsLogin(QWidget *parent = 0);
    ~nsLogin();

private slots:
	void on_btnCancel_clicked();

	void on_btnOK_clicked();

private:
    Ui::nsLogin *ui;
	void checkStrings();
	bool validateBaseDomain(QString basedomain);
};

#endif // NSLOGIN_H
