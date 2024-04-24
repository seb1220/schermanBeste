#include "quakes.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_PA2(quakes)


namespace qx {
template <> void register_class(QxClass<quakes> & t)
{
    t.data(& quakes::time, "time");
    t.data(& quakes::latitude, "latitude");
    t.data(& quakes::longitude, "longitude");
    t.data(& quakes::mag, "mag");
    t.data(& quakes::place, "place");

}
}
