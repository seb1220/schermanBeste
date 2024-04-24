#include "mainwindow.h"
#include "ui_mainwindow.h"

#include "drivers.h"
#include "results.h"
#include "constructors.h"

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

    for (int i = 0; i < drivers.size(); i++)
    {
        ui->displayedDrivers->addItem(drivers.at(i)->forename + " " + drivers.at(i)->surname);
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
    for (unsigned long i = 0; i < drivers.size(); i++)
    {
        ui->displayedDrivers->addItem(drivers.at(i)->forename + " " + drivers.at(i)->surname);
    }
}

void MainWindow::on_displayedDrivers_currentIndexChanged(int index)
{
    if (index == -1)
    {
        ui->displayedDrivers->setCurrentIndex(0);
        return;
    }

    qx::QxSqlQuery query("where forename || ' ' || surname = '" + ui->displayedDrivers->currentText() + "'");

    list_driver drivers;
    qx::dao::fetch_by_query_with_all_relation(query, drivers);

    ui->driverName->setText(drivers.at(0)->forename + " " + drivers.at(0)->surname);
    ui->driverBDay->setText(drivers.at(0)->dob);

    list_result results = drivers.at(0)->resultX;
    ui->driverRaces->setText(QString::number(results.size()));

    ui->driverConstructors->setText(QString::number(results.at(0)->constructor->id));
    // QString constructors = "";
    // for (auto result : results) {
    //     constructors += result->constructor->name + ", ";
    // }
    // ui->driverConstructors->setText(constructors);
}
