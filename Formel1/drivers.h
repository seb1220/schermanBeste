#ifndef DRIVERS_H
#define DRIVERS_H

class results;

class QX_FONE_DLL_EXPORT drivers
{
public:
    typedef std::shared_ptr<results> result_ptr;
    typedef std::vector<result_ptr> list_result;

    long id;
    QString forename;
    QString surname;
    QDate dob;

    list_result resultX;

    drivers() : id(0) { ; };
    virtual ~drivers() { ; };
};

QX_REGISTER_PRIMARY_KEY(drivers, long)
QX_REGISTER_HPP_QX_FONE(drivers, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<drivers> driver_ptr;
typedef qx::QxCollection<QString, driver_ptr> list_driver;

#endif // DRIVERS_H
