
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
    void addConnection(QPointF from, QPointF to);
    void clear();

private:
    QPointF getPixelPos(QPointF coord);

    QVector<QPointF> cities;
    QVector<std::pair<QPointF, QPointF>> conns;

signals:

};


#endif //XMLEXPLORER_FLUGMAP_H
