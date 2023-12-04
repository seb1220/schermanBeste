#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "airport.h"

#include <QxOrm_Impl.h>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    qx::QxSqlDatabase::getSingleton()->setDriverName("QSQLITE");
    qx::QxSqlDatabase::getSingleton()->setDatabaseName("../AirlineRoutes.db");
    qx::QxSqlDatabase::getSingleton()->setHostName("localhost");
    qx::QxSqlDatabase::getSingleton()->setUserName("root");
    qx::QxSqlDatabase::getSingleton()->setPassword("");
}

MainWindow::~MainWindow()
{
    delete ui;
}


void MainWindow::on_pushButton_clicked()
{
    qx::QxSqlQuery query("WHERE airport.iata = :iata");
    query.bind(":iata", "VIE");

    list_airport vienna_airport;
    qx::dao::fetch_by_query(query, vienna_airport);
//    qAssert(vienna_airport.count() == 1);

    // set the ui label to the count
    ui->label->setText(vienna_airport.getByIndex(0)->name);
}
