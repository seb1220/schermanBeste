#ifndef AIRLINE_H
#define AIRLINE_H

#include <QMap>
#include "alliance.h"

class QX_FLUG_DLL_EXPORT airline
{
public:
    long id;
    QString name;
    long alliance_id;

    airline() : id(0) { ; };
    virtual ~airline() { ; };
};

QX_REGISTER_PRIMARY_KEY(airline, long)
QX_REGISTER_HPP_QX_FLUG(airline, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<airline> airline_ptr;
typedef QMap<QString, airline_ptr> list_airline;

#endif // AIRLINE_H
