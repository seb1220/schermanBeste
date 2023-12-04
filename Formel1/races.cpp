#include "races.h"
#include "results.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_QX_FONE(races)

namespace qx {
template <> void register_class(QxClass<races> & t)
{
    t.id(& races::id, "raceId");
    
    t.data(& races::date, "date");
    t.data(& races::name, "name");
    
    t.relationOneToMany(& races::resultX, "list_result", "raceId");
}
}
