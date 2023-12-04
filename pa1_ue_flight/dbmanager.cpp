#include "dbmanager.h"

#include <QSqlQuery>
#include <iostream>

dbManager::dbManager()
{
    db = QSqlDatabase::addDatabase("QSQLITE");
    db.setDatabaseName("../AirlineRoutes.db");
    db.open();

    loadFlights();
    loadAirports();
    loadAirlines();
    loadAlliances();
}

dbManager::~dbManager()
{
    db.close();
    QSqlDatabase::removeDatabase("QSQLITE");
}

void dbManager::loadAirports() {

    QSqlQuery query;
    query = db.exec("SELECT * FROM Airport");

    while (query.next()) {
        airports_id[query.value(0).toInt()] = query.value(3).toString();
        airports[query.value(3).toString()] = airport {
            query.value(0).toInt(),
            query.value(3).toString(),
            query.value(4).toString(),
            query.value(2).toDouble(),
            query.value(1).toDouble()
        };
    }

    std::cout << "Airports loaded." << std::endl;
}

void dbManager::loadAirlines() {
    QSqlQuery query;
    query = db.exec("select * from Airline");

    while (query.next()) {
        airlines_id[query.value(0).toInt()] = query.value(1).toString();


        airlines[query.value(1).toString()] = airline {
            query.value(0).toInt(),
            query.value(1).toString(),
            query.value(2).toInt()
        };
    }

    std::cout << "Airlines loaded." << std::endl;
}

void dbManager::loadAlliances() {
    QSqlQuery query;
    query = db.exec("select * from Alliance");

    while (query.next()) {
        alliances[query.value(0).toInt()] = alliance {
            query.value(0).toInt(),
            query.value(1).toString()
        };
    }

    std::cout << "Alliances loaded." << std::endl;
}

void dbManager::loadFlights() {
    QSqlQuery query;
    query = db.exec("select * from Route");

    while (query.next()) {
        flights.append(flight {
            query.value(1).toInt(),
            query.value(2).toInt(),
            query.value(3).toInt()
        });
    }

    std::cout << "Flights loaded." << std::endl;
}
