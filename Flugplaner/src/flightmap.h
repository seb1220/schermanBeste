
#ifndef XMLEXPLORER_FLUGMAP_H
#define XMLEXPLORER_FLUGMAP_H

#include "QWidget"


class flightmap : public QWidget {
    Q_OBJECT

public:
    explicit flightmap(QWidget *parent = nullptr);
    ~flightmap();
    void paintEvent(QPaintEvent *event);
    void addCity(QPointF city);
    void clearCities() { cities.clear(); }

private:
    QPointF getPixelPos(QPointF coord);

    QVector<QPointF> cities;

signals:

};


#endif //XMLEXPLORER_FLUGMAP_H
