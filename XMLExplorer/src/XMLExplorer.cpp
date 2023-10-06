#include "XMLExplorer.h"
#include "lib/tinyxml2.h"

XMLExplorer::XMLExplorer(QWidget *parent)
    : QMainWindow(parent)
{
    ui.setupUi(this);

    tinyxml2::XMLDocument doc;

}

XMLExplorer::~XMLExplorer()
{}
