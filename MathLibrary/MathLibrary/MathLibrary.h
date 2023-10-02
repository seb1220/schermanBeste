#pragma once

#include "mathlibrary_global.h"
#include <QtWidgets/QMainWindow>

class MATHLIBRARY_EXPORT MathLibrary
{
public:
    MathLibrary();

    static double calculate(QString equation);
};
