#include "airport.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_QX_FLUG(airport)


namespace qx {
template <> void register_class(QxClass<airport> & t)
{
    t.id(& airport::id, "id");

    t.data(& airport::latitude, "latitude");
    t.data(& airport::longitude, "longitude");
    t.data(& airport::name, "name");
    t.data(& airport::iata, "iata");
}
}
