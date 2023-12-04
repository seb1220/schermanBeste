#ifndef MODELS_H
#define MODELS_H

#include <QString>


struct airport {
    int id;
    QString name;
    QString iata;
    double longitude;
    double latitude;
};

struct alliance {
    int id;
    QString name;
};

struct airline {
    int id;
    QString name;
    int alliance_id;
};

struct flight {
    int airline_id;
    int departure_airport_id;
    int arrival_airport_id;
};

#endif // MODELS_H
