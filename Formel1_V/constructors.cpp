#include "constructors.h"
#include "precompiled.h"
#include <QxOrm_Impl.h>
#include "drivers.h"

QX_REGISTER_CPP_QX_FORMEL1(Constructors);

namespace qx {
template <> void register_class(QxClass<Constructors> & t)
{
    t.id(& Constructors::constructorId, "constructorId");
    t.data(& Constructors::name, "name");

    t.relationManyToMany(& Constructors::m_drivers, "list_drivers", "results", "constructorId", "driverId");
}}
