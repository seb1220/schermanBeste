#include "map.h"
#include "ui_map.h"

#include <QPainter>

map::map(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::map)
{
    ui->setupUi(this);
}

map::~map()
{
    delete ui;
}

void map::paintEvent(QPaintEvent *event)
{
    QPainter painter(this);
    painter.setPen(QPen(Qt::black, 4));
    painter.drawImage(QRectF(0, 0, QWidget::width(), QWidget::height()), QImage("../img.png"));
}
