#include "precompiled.h"
#include <QxOrm_Impl.h>
#include "drivers.h"
#include "nationality.h"
#include "constructors.h"
#include "results.h"

QX_REGISTER_CPP_QX_FORMEL1(Drivers);

namespace qx {
template <> void register_class(QxClass<Drivers> & t)
{
    t.id(& Drivers::driverId, "driverId");
    t.data(& Drivers::forename, "forename");
    t.data(& Drivers::surname, "surname");
    t.data(& Drivers::dob, "dob");
    t.data(& Drivers::nationality, "nationality");

    t.relationManyToMany(& Drivers::constructors, "list_constr", "results", "driverId", "constructorId");
    t.relationOneToMany(& Drivers::results, "list_results", "driverId");
}}

