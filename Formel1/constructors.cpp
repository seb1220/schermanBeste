#include "constructors.h"
#include "results.h"
#include "precompiled.h"

#include <QxOrm_Impl.h>

QX_REGISTER_CPP_QX_FONE(constructors)


namespace qx {
template <> void register_class(QxClass<constructors> & t)
{
    t.id(& constructors::id, "constructorId");
    
    t.data(& constructors::name, "name");
    
    t.relationOneToMany(& constructors::resultX, "list_result", "constructorId");
}
}
