#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_UPNRechner.h"

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
    Ui::UPNRechnerClass ui;
};
