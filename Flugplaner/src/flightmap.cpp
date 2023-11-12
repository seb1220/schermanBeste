
#include "flightmap.h"
#include "QPainter"

flightmap::flightmap(QWidget *parent) : QWidget{parent} {
    cities.append(QPointF(16, 48));
    cities.append(QPointF(48, 16));
}

flightmap::~flightmap() = default;

void flightmap::paintEvent(QPaintEvent *event) {
    QWidget::paintEvent(event);

    QPainter painter(this);
    painter.setPen(QPen(Qt::black, 4));
    painter.drawImage(QRectF(0, 0, QWidget::width(), QWidget::height()), QImage("../img.png"));

    for (QPointF point : cities) {
        painter.drawEllipse(getPixelPos(point), 30, 30);
    }
    painter.setPen(QPen(Qt::red, 1));
    for (auto conn : conns) {
        painter.drawLine(getPixelPos(conn.first), getPixelPos(conn.second));
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

void flightmap::addConnection(QPointF from, QPointF to) {
    conns.append({from, to});
    repaint();
}

void flightmap::clear() {
    cities.clear();
    conns.clear();
}
