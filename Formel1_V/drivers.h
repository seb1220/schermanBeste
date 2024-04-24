#ifndef DRIVERS_H
#define DRIVERS_H

class Constructors;
class Results;

class QX_FORMEL1_DLL_EXPORT Drivers
{
public:
    //constructors Datentypen
    typedef std::shared_ptr<Constructors> constr_ptr;
    typedef QList<constr_ptr> list_constr;

    //results Datentypen
    typedef std::shared_ptr<Results> result_ptr;
    typedef QList<result_ptr> list_results;

    int driverId;
    QString forename;
    QString surname;
    QString dob;
    QString nationality;

    list_constr constructors;
    list_results results;

    Drivers() { ; }
    virtual ~Drivers() { ; }
};

QX_REGISTER_PRIMARY_KEY(Drivers, int);
QX_REGISTER_HPP_QX_FORMEL1(Drivers, qx::trait::no_base_class_defined, 0);

typedef std::shared_ptr<Drivers> drivers_ptr;
typedef QList<drivers_ptr> list_drivers;

#endif // DRIVERS_H
