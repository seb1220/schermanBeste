#ifndef ALLIANCE_H
#define ALLIANCE_H

#include <QMap>

class airline;

class QX_FLUG_DLL_EXPORT alliance
{
public:
    long id;
    QString name;

    list_airline airlines;

    alliance() : id(0) { ; };
    virtual ~alliance() { ; };
};

QX_REGISTER_PRIMARY_KEY(alliance, long)
QX_REGISTER_HPP_QX_FLUG(alliance, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<alliance> alliance_ptr;
typedef QMap<QString, alliance_ptr> list_alliance;

#endif // ALLIANCE_H
