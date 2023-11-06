#ifndef FLUGPLANER_AIRPORT_H
#define FLUGPLANER_AIRPORT_H

#include "QString"


class airport {
public:
    int id;
    QString name;
    QString iata;
    qreal longitude;
    qreal latitude;
};


#endif //FLUGPLANER_AIRPORT_H
