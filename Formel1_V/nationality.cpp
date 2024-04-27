#include "nationality.h"
#include "precompiled.h"
#include <QxOrm_Impl.h>
#include "drivers.h"

QX_REGISTER_CPP_QX_FORMEL1(Nationality);

namespace qx {
template <> void register_class(QxClass<Nationality> & t)
{
    t.id(& Nationality::name, "nationality");

    t.relationOneToMany(& Nationality::drivers, "list_driver", "nationality");
}}

