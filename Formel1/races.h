#ifndef RACES_H
#define RACES_H

class results;

class QX_FONE_DLL_EXPORT races
{
public:
    typedef std::shared_ptr<results> result_ptr;
    typedef std::vector<result_ptr> list_result;

    long id;
    QDate date;
    QString name;

    list_result resultX;

    races() : id(0) { ; };
    virtual ~races() { ; };
};

QX_REGISTER_PRIMARY_KEY(races, long)
QX_REGISTER_HPP_QX_FONE(races, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<races> race_ptr;
typedef QMap<QString, race_ptr> list_race;

#endif // RACES_H
