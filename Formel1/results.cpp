#include "results.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_QX_FONE(results)

namespace qx {
template <> void register_class(QxClass<results> & t)
{
    t.id(& results::id, "resultId");
    
    t.data(& results::position, "position");
    
    t.relationManyToOne(& results::race, "raceId");
    t.relationManyToOne(& results::driver, "driverId");
    t.relationManyToOne(& results::constructor, "constructorId");
}
}
