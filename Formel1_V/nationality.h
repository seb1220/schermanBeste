#ifndef NATIONALITY_H
#define NATIONALITY_H

class Drivers;

class QX_FORMEL1_DLL_EXPORT Nationality
{
public: 
    typedef std::shared_ptr<Drivers> driver_ptr;
    typedef QList<driver_ptr> list_driver;

    QString name;
    list_driver drivers;
    Nationality(){;}
    virtual ~Nationality() { ; }
};


QX_REGISTER_PRIMARY_KEY(Nationality, QString)
QX_REGISTER_HPP_QX_FORMEL1(Nationality, qx::trait::no_base_class_defined, 0)

// -- typedef
typedef std::shared_ptr<Nationality> natio_ptr;
typedef QList<natio_ptr> list_natio;

#endif // NATIONALITY_H
