#ifndef mainwindow_h
#define mainwindow_h

#include <QtWidgets/QMainWindow>
#include <QMainWindow>
#include <QScopedPointer>
#include "ui_UPNRechner.h"


namespace Ui
{
    class UPNRechner;
}

class UPNRechner : public QMainWindow
{
    Q_OBJECT

public:
    UPNRechner(QWidget *parent = nullptr);
    ~UPNRechner();

private:
	double calculatePostShit(QString input);
    
private slots:
    void on_calculateButton_clicked();

private:
    QScopedPointer<Ui::UPNRechner> ui;
};

#endif
