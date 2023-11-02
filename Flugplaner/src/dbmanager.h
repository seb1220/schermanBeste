
#ifndef FLUGPLANER_DBMANAGER_H
#define FLUGPLANER_DBMANAGER_H

#include <QSqlDatabase>
#include <QMap>
#include "airport.h"

class dbmanager {
public:
    explicit dbmanager();

private:
    QSqlDatabase db;
    QMap<int, QString> airport_indexes;
    QMap<QString, airport> airports;

    void getAirports();

};


#endif //FLUGPLANER_DBMANAGER_H
