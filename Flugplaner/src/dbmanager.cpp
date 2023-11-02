//
// Created by dusack on 10/23/23.
//

#include <QSqlQuery>
#include "dbmanager.h"

dbmanager::dbmanager() {
//    db = QSqlDatabase::QSqlDatabase("QSQLITE");
    db = QSqlDatabase::addDatabase("QSQLITE");
    db.setHostName("../AirlineRoutes.db");
    db.open();

    getAirports();
}

void dbmanager::getAirports() {
    QSqlQuery query;
    query.exec("SELECT NAME, ID FROM STAFF");
}
