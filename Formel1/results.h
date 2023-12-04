#ifndef RESULTS_H
#define RESULTS_H

#include "races.h"
#include "constructors.h"
#include "drivers.h"

class QX_FONE_DLL_EXPORT results
{
public:
    long id;
    race_ptr race;
    driver_ptr driver;
    constructor_ptr constructor;
    int position;

    results() : id(0) { ; };
    virtual ~results() { ; };
};

QX_REGISTER_PRIMARY_KEY(results, long)
QX_REGISTER_HPP_QX_FONE(results, qx::trait::no_base_class_defined, 0)

typedef std::shared_ptr<results> result_ptr;
typedef QMap<QString, result_ptr> list_result;

#endif // RESULTS_H
