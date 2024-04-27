#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <qchart.h>
#include "quakes.h"

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    QChart *chart = new QChart();

private slots:
    void on_suchen_clicked();

    void on_ortauswahl_currentTextChanged(const QString &arg1);

private:
    list_quakes quakes;
    Ui::MainWindow *ui;
};
#endif // MAINWINDOW_H
