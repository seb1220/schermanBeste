#ifndef AIRPORT_H
#define AIRPORT_H

#include <QMap>

class QX_FLUG_DLL_EXPORT airport
{
public:
    long id;
    double latitude;
    double longitude;
    QString name;
    QString iata;

    airport() : id(0) { ; }
    virtual ~airport() { ; }
};

QX_REGISTER_PRIMARY_KEY(airport, long)
QX_REGISTER_HPP_QX_FLUG(airport, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<airport> airport_ptr;
typedef QMap<QString, airport_ptr> list_airport;

#endif // AIRPORT_H
