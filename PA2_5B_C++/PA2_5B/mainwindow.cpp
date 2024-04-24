#include "mainwindow.h"
#include "ui_mainwindow.h"

#include "quakes.h"
#include <iostream>
#include <qboxlayout.h>
#include <qlineseries.h>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    qx::QxSqlDatabase::getSingleton()->setDriverName("QSQLITE");
    qx::QxSqlDatabase::getSingleton()->setDatabaseName("../../earthquakes.sqlite");
    qx::QxSqlDatabase::getSingleton()->setHostName("localhost");
    qx::QxSqlDatabase::getSingleton()->setUserName("root");
    qx::QxSqlDatabase::getSingleton()->setPassword("");

    qx::QxSqlQuery query("");

    qx::dao::fetch_by_query(query, quakes);

    QList<QString> found_names;
    for (int i = 0; i < quakes.size(); i++)
    {
        QString name = quakes.at(i)->place;
        auto names = name.split("of ");
        // std::cout << names.size() << std::endl;


        if (names.size() > 1 && !found_names.contains(names.at(1)))
        {

            found_names.append(names.at(1));
            ui->ortauswahl->addItem(names.at(1));
        } else if (!found_names.contains(names.at(0)) && !names.at(0).contains("km "))
        {
            // std::cout << names.at(0).toStdString() << std::endl;
            found_names.append(names.at(0));
            ui->ortauswahl->addItem(names.at(0));
        }
    }

    QChartView *chartView = new QChartView(chart);
    chartView->setRenderHint(QPainter::Antialiasing);

    QLayout *layout = new QVBoxLayout(ui->graph);
    layout->addWidget(chartView);
    ui->graph->setLayout(layout);
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_suchen_clicked()
{
    ui->ortauswahl->clear();

    QString ort = ui->ort->text();

    QList<QString> found_names;
    for (int i = 0; i < quakes.size(); i++)
    {
        bool found = false;
        for (QString name : ort.split(" "))
        {
            std::cout << name.toStdString() << std::endl;
            if (quakes.at(i)->place.contains(name))
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            std::cout << " found nothing " << std::endl;
            continue;
        }

        std::cout << " found something " << std::endl;

        QString name = quakes.at(i)->place;
        auto names = name.split("of ");
        std::cout << names.size() << std::endl;


        if (names.size() > 1 && !found_names.contains(names.at(1)))
        {

            found_names.append(names.at(1));
            ui->ortauswahl->addItem(names.at(1));
        } else if (!found_names.contains(names.at(0)) && !names.at(0).contains("km "))
        {
            std::cout << names.at(0).toStdString() << std::endl;
            found_names.append(names.at(0));
            ui->ortauswahl->addItem(names.at(0));
        }
    }
}


void MainWindow::on_ortauswahl_currentTextChanged(const QString &arg1)
{
    //chart
    QLineSeries *series = new QLineSeries();
    //series->clear();
    series->setName("Erdbeben");

    ui->erdbeben->insertRow ( ui->erdbeben->rowCount() );
    ui->erdbeben->setItem   ( 0,
                         0,
                         new QTableWidgetItem("test"));

    int i = 0;
    for (auto quake : quakes) {
        if (!quake->place.contains(arg1))
            continue;

        series->append(i, quake->mag);
        i ++;
    }


    chart->removeAllSeries();
    chart->addSeries(series);
    chart->createDefaultAxes();
}

