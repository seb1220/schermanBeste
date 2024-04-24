#ifndef QUAKES_H
#define QUAKES_H

class PA2_DLL_EXPORT quakes
{
public:
    QString time;
    double latitude;
    double longitude;
    double mag;
    QString place;

    quakes() { ; };
    virtual ~quakes() { ; };
};

// QX_REGISTER_PRIMARY_KEY(quakes, long)
QX_REGISTER_HPP_PA2(quakes, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<quakes> quakes_ptr;
typedef std::vector<quakes_ptr> list_quakes;

#endif // QUAKES_H
