#ifndef SQUARE_H
#define SQUARE_H

#include "register.h"

class PLUGIN Square : public Form
{
protected:
    int width;
public:
    Square(int x = 0, int y = 0, int width = 0);
    void draw(QPainter &painter);
    Form *parseForm(const QString &data);
};

#endif // SQUARE_H
