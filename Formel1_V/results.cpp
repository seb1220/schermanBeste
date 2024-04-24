#include "results.h"
#include "drivers.h"

QX_REGISTER_CPP_QX_FORMEL1(Results);

namespace qx {
template <> void register_class(QxClass<Results> & t)
{
    t.id(& Results::resultId, "resultId");
    t.data(& Results::position, "position");

    t.relationManyToOne(& Results::driver, "driverId");
}}
