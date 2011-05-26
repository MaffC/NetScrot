#include <QtGui/QApplication>
#include <QMessageBox>
#include "nslogin.h"

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    nsLogin w;
    w.show();

    return a.exec();
}
