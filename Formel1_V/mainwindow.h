#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "drivers.h"
#include "nationality.h"
#include "results.h"
#include "constructors.h"
#include <QtCharts/QChartView>
#include <QVBoxLayout>
#include <QLineSeries>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();
    list_drivers drivers_from;
    list_drivers drivers;
    list_constr constr;
    QList<QString> natios;
    int currId;
    QChart *chart = new QChart();

private slots:
    void on_but_suchen_clicked();

    void on_comBox_drivers_currentIndexChanged(int index);

private:
    Ui::MainWindow *ui;
    void load_db();
};
#endif // MAINWINDOW_H
