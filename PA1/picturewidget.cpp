#include "picturewidget.h"
#include "ui_picturewidget.h"

#include <QPainter>
#include "TinyExif/TinyEXIF.h"
#include "TinyExif/TinyExif_global.h"
#include <iostream>
#include <fstream>

pictureWidget::pictureWidget(QWidget *parent) :
    QWidget(parent),
    ui(new Ui::pictureWidget)
{
    ui->setupUi(this);
}

pictureWidget::~pictureWidget()
{
    delete ui;
}

void pictureWidget::paintEvent(QPaintEvent *event)
{
    QPainter painter(this);
    painter.setPen(QPen(Qt::black, 4));

    if (filename != "") {

        std::ifstream istream(filename.toStdString().c_str(), std::ifstream::binary);

        // parse image EXIF and XMP metadata
        TinyEXIF::EXIFInfo imageEXIF(istream);
        if (imageEXIF.Fields)
            std::cout
                << "Image Description " << imageEXIF.ImageDescription << "\n"
                << "Image Resolution " << imageEXIF.ImageWidth << "x" << imageEXIF.ImageHeight << " pixels\n"
                << "Camera Model " << imageEXIF.Make << " - " << imageEXIF.Model << "\n"
                << "Focal Length " << imageEXIF.FocalLength << " mm" << std::endl;

        painter.drawImage(QRectF(0, 0, QWidget::width(), QWidget::height()), QImage(filename));
    }
}

void pictureWidget::setFilename(QString filename)
{
    this->filename = filename;
    repaint();
}
