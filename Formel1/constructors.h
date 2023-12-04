#ifndef CONSTRUCTORS_H
#define CONSTRUCTORS_H

class results;

class QX_FONE_DLL_EXPORT constructors
{
public:
    typedef std::shared_ptr<results> result_ptr;
    typedef std::vector<result_ptr> list_result;

    long id;
    QString name;

    list_result resultX;

    constructors() : id(0) { ; };
    virtual ~constructors() { ; };
};

QX_REGISTER_PRIMARY_KEY(constructors, long)
QX_REGISTER_HPP_QX_FONE(constructors, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<constructors> constructor_ptr;
typedef QMap<QString, constructor_ptr> list_constructor;

#endif // CONSTRUCTORS_H
