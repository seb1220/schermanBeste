
#include "flightmap.h"
#include "QPainter"

flightmap::flightmap(QWidget *parent) : QWidget{parent} {
    cities.append(QPointF(16, 48));
    cities.append(QPointF(48, 16));
}

flightmap::~flightmap() {

}

void flightmap::paintEvent(QPaintEvent *event) {
    QWidget::paintEvent(event);

    QPainter painter(this);
    painter.drawImage(QRectF(0, 0, QWidget::width(), QWidget::height()), QImage("../img.png"));

    for (QPointF point : cities) {
        painter.drawEllipse(getPixelPos(point), 30, 30);
    }
}

QPointF flightmap::getPixelPos(QPointF coord) {
    coord.setX((coord.x() + 180.0) / 360.0 * QWidget::width());
    coord.setY((-coord.y() + 90.0) / 180.0 * QWidget::height());
    return coord;
}

void flightmap::addCity(QPointF city) {
    cities.append(city);
    repaint();
}
