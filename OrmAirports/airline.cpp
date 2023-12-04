#include "airline.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_QX_FLUG(airline)


namespace qx {
template <> void register_class(QxClass<airline> & t)
{
    t.id(& airline::id, "id");

    t.data(& airline::name, "name");
    t.data(& airline::alliance_id, "alliance");
}
}
