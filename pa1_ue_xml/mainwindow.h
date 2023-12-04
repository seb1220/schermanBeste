#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QTreeWidgetItem>

#include "lib/tinyxml2.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_actionOpen_File_triggered();

    void on_xmlWidget_currentItemChanged(QTreeWidgetItem *current, QTreeWidgetItem *previous);

    void on_attributes_currentIndexChanged(int index);

private:
    Ui::MainWindow *ui;
    tinyxml2::XMLDocument doc;
    void displayInTree(tinyxml2::XMLElement * xmlElement, QTreeWidgetItem* uiElement);
};
#endif // MAINWINDOW_H
