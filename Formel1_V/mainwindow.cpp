#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "drivers.h"
#include "results.h"
#include <QtWidgets/QApplication>
#include <QtWidgets/QMainWindow>
#include <QtCharts/QChartView>
#include <QtCharts/QBarSeries>
#include <QtCharts/QBarSet>
#include <QtCharts/QBarCategoryAxis>
#include <QLineSeries>
#include <QVBoxLayout>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    load_db();

    QChartView *chartView = new QChartView(chart);
    chartView->setRenderHint(QPainter::Antialiasing);

    QLayout *layout = new QVBoxLayout(ui->widgetChart);
    layout->addWidget(chartView);
    ui->widgetChart->setLayout(layout);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::load_db()
{
    qx::QxSqlDatabase::getSingleton()->setDriverName("QSQLITE");
    qx::QxSqlDatabase::getSingleton()->setDatabaseName("../Formula1.sqlite");
    qx::QxSqlDatabase::getSingleton()->setHostName("localhost");
    qx::QxSqlDatabase::getSingleton()->setUserName("root");
    qx::QxSqlDatabase::getSingleton()->setPassword("");


    //list_drivers drivers;
    qx::dao::fetch_all(drivers);

    //qx::QxSqlQuery query("WHERE drivers.nationality = :natio");

    qx::QxSqlQuery query_from("SELECT DISTINCT(nationality) from drivers;");
    QSqlError e = qx::dao::call_query(query_from);

    if(e.isValid()){
        qDebug() << "Falsche Query!";
    }
    else{
        for (int i = 0; i < query_from.getSqlResultRowCount(); i++) {
            //std::cout << query_from.getSqlResultAt(i).at(0).toString().toStdString() << std::endl;
            natios.append(query_from.getSqlResultAt(i).at(0).toString());
        }
    }

    /*
    for (const auto& d : drivers) {
        if(!natios.contains(d->nationality)){
            natios.append(d->nationality);
        }
    }*/

    for (const auto& n : natios) {
        std::cout << n.toStdString() << std::endl;
    }
}

void MainWindow::on_but_suchen_clicked()
{
    ui->comBox_drivers->clear();

    QString input = ui->txt_natio->toPlainText();
    if(natios.contains(input)){
        qx::QxSqlQuery query("WHERE drivers.nationality = :natio");
        query.bind(":natio", input);
        qx::dao::fetch_by_query_with_all_relation(query, drivers_from);
        for (const auto& d : drivers_from) {
            ui->comBox_drivers->addItem(d->forename + " " + d->surname);
        }
    }
}


void MainWindow::on_comBox_drivers_currentIndexChanged(int index)
{
    if(index < 0){
        return;
    }
    drivers_ptr driver = drivers_from.at(index);
    ui->comBox_constr->clear();

    ui->label_forename->setText(driver->forename);
    ui->label_surname->setText(driver->surname);
    ui->label_date->setText(driver->dob);

    //results
    ui->label_count->setText(QString::number(driver->results.count()));

    //constructors
    for (const auto& d : driver->constructors) {
        ui->comBox_constr->addItem(d->name);
    }

    //chart
    QLineSeries *series = new QLineSeries();
    //series->clear();
    series->setName("Rennen");

    int i = 0;
    for (const auto& d : driver->results) {
        series->append(i, d->position);
        i ++;
    }

    chart->removeAllSeries();
    chart->addSeries(series);
    chart->createDefaultAxes();
}

