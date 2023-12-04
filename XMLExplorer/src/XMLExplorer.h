#pragma once

#include <QtWidgets/QMainWindow>
#include <QFileSystemModel>
#include "ui_XMLExplorer.h"

class XMLExplorer : public QMainWindow
{
    Q_OBJECT

public:
    XMLExplorer(QWidget *parent = nullptr);
    ~XMLExplorer();

private slots:
    void on_openFile_clicked();

private:
    Ui::XMLExplorerClass ui;
    QFileSystemModel model;
};
