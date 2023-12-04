#ifndef PICTUREWIDGET_H
#define PICTUREWIDGET_H

#include <QWidget>

namespace Ui {
class pictureWidget;
}

class pictureWidget : public QWidget
{
    Q_OBJECT

public:
    explicit pictureWidget(QWidget *parent = nullptr);
    ~pictureWidget();

    void paintEvent(QPaintEvent *event);

    void setFilename(QString filename);

private:
    Ui::pictureWidget *ui;
    QString filename = "";
};

#endif // PICTUREWIDGET_H
