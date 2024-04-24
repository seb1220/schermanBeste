#ifndef CONSTRUCTORS_H
#define CONSTRUCTORS_H

class Drivers;

class QX_FORMEL1_DLL_EXPORT Constructors
{
public:
    typedef std::shared_ptr<Drivers> drivers_ptr;
    typedef QList<drivers_ptr> list_drivers;

    int constructorId;
    QString name;
    list_drivers m_drivers;
    Constructors() { ; }
    virtual ~Constructors() { ; }
};

QX_REGISTER_PRIMARY_KEY(Constructors, int);
QX_REGISTER_HPP_QX_FORMEL1(Constructors, qx::trait::no_base_class_defined, 0);

typedef std::shared_ptr<Constructors> constr_ptr;
typedef QList<constr_ptr> list_constr;

#endif // CONSTRUCTORS_H

