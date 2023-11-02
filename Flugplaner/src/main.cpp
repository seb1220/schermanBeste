#include "flightplaner.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    flightplaner w;
    w.show();
    return a.exec();
}
