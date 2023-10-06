#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_XMLExplorer.h"

class XMLExplorer : public QMainWindow
{
    Q_OBJECT

public:
    XMLExplorer(QWidget *parent = nullptr);
    ~XMLExplorer();

private:
    Ui::XMLExplorerClass ui;
};
