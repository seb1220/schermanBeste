#include "drivers.h"
#include "results.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_QX_FONE(drivers)


namespace qx {
template <> void register_class(QxClass<drivers> & t)
{
    t.id(& drivers::id, "driverId");
    
    t.data(& drivers::forename, "forename");
    t.data(& drivers::surname, "surname");
    t.data(& drivers::dob, "dob");
    
    t.relationOneToMany(& drivers::resultX, "list_result", "driverId");
}
}
