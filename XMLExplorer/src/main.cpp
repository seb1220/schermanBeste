#include "XMLExplorer.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    XMLExplorer w;
    w.show();
    return a.exec();
}
