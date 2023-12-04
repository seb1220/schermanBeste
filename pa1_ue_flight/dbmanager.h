#ifndef DBMANAGER_H
#define DBMANAGER_H

#include "models.h"

#include <QSqlDatabase>

#include <QList>
#include <QMap>

class dbManager
{
public:
    dbManager();
    ~dbManager();

    QMap<int, QString> airports_id;
    QMap<QString, airport> airports;
    QMap<QString, airline> airlines;
    QMap<int, QString> airlines_id;
    QMap<int, alliance> alliances;
    QList<flight> flights;

private:
    QSqlDatabase db;

    void loadFlights();
    void loadAirports();
    void loadAirlines();
    void loadAlliances();
};

#endif // DBMANAGER_H
