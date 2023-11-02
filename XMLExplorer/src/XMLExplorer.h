#pragma once

#include <QtWidgets/QMainWindow>
#include <QFileSystemModel>
#include "ui_XMLExplorer.h"
#include "lib/tinyxml2.h"

class XMLExplorer : public QMainWindow
{
    Q_OBJECT

public:
    XMLExplorer(QWidget *parent = nullptr);
    ~XMLExplorer();

private slots:
    void on_openFile_clicked();
    void on_treeWidget_currentItemChanged(QTreeWidgetItem *current, QTreeWidgetItem *previous);
    void on_tableWidget_itemChanged(QTableWidgetItem *item);

private:
    Ui::XMLExplorerClass ui;
    tinyxml2::XMLDocument doc;
    void fillTreeWidget(tinyxml2::XMLElement* element);
    tinyxml2::XMLElement* findElement(tinyxml2::XMLElement* element, const QString& name);
};
