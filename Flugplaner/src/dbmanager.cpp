#include <QSqlQuery>
#include "dbmanager.h"

#include <iostream> // TODO: remove this

dbmanager::dbmanager() {
    db = QSqlDatabase::addDatabase("QSQLITE");
    db.setDatabaseName("../AirlineRoutes.db");
    db.open();

    loadAirports();
    loadAirlines();
    loadAlliances();
    loadFlights();
}

dbmanager::~dbmanager() {
    db.close();
    QSqlDatabase::removeDatabase("QSQLITE");
}

void dbmanager::loadAirports() {

    QSqlQuery query;
    query = db.exec("SELECT * FROM Airport");

    while (query.next()) {
        airport_indexes[query.value(0).toInt()] = query.value(3).toString();
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

void dbmanager::loadAirlines() {
    QSqlQuery query;
    query = db.exec("select * from Airline");

    while (query.next()) {
        airlines[query.value(0).toInt()] = airline {
                query.value(0).toInt(),
                query.value(1).toString(),
                query.value(2).toInt()
        };
    }

    std::cout << "Airlines loaded." << std::endl;
}

void dbmanager::loadAlliances() {
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

void dbmanager::loadFlights() {
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
