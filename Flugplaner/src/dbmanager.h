
#ifndef FLUGPLANER_DBMANAGER_H
#define FLUGPLANER_DBMANAGER_H

#include <QSqlDatabase>
#include <QMap>
#include "airport.h"
#include "airline.h"
#include "alliance.h"
#include "flight.h"

class dbmanager {
public:
    explicit dbmanager();
    ~dbmanager();

    QMap<int, QString> airport_indexes;
    QMap<QString, airport> airports;
    QMap<int, airline> airlines;
    QMap<int, alliance> alliances;
    QList<flight> flights;

private:
    QSqlDatabase db;

    void loadAirports();
    void loadAirlines();
    void loadAlliances();
    void loadFlights();
};


#endif //FLUGPLANER_DBMANAGER_H
