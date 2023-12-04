#include "mainwindow.h"
#include "ui_mainwindow.h"

#include "drivers.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    qx::QxSqlDatabase::getSingleton()->setDriverName("QSQLITE");
    qx::QxSqlDatabase::getSingleton()->setDatabaseName("../Formula1.sqlite");
    qx::QxSqlDatabase::getSingleton()->setHostName("localhost");
    qx::QxSqlDatabase::getSingleton()->setUserName("root");
    qx::QxSqlDatabase::getSingleton()->setPassword("");

    qx::QxSqlQuery query("");

    list_driver drivers;
    qx::dao::fetch_by_query(query, drivers);

    for (int i = 0; i < drivers.count(); i++)
    {
        ui->displayedDrivers->addItem(drivers.getByIndex(i)->forename + " " + drivers.getByIndex(i)->surname);
    }
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_searchDriver_textChanged(const QString &arg1)
{
    qx::QxSqlQuery query("where forename like '%" + arg1 + "%' or surname like '%" + arg1 + "%' or forename || ' ' || surname like '%" + arg1 + "%' or surname || ' ' || forename like '%" + arg1 + "%'");

    list_driver drivers;
    qx::dao::fetch_by_query(query, drivers);

    ui->displayedDrivers->clear();
    for (int i = 0; i < drivers.count(); i++)
    {
        ui->displayedDrivers->addItem(drivers.getByIndex(i)->forename + " " + drivers.getByIndex(i)->surname);
    }
}
