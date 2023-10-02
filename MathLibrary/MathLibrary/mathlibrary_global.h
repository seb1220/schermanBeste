#pragma once

#include <QtCore/qglobal.h>

#ifndef BUILD_STATIC
# if defined(MATHLIBRARY_LIB)
#  define MATHLIBRARY_EXPORT Q_DECL_EXPORT
# else
#  define MATHLIBRARY_EXPORT Q_DECL_IMPORT
# endif
#else
# define MATHLIBRARY_EXPORT
#endif
