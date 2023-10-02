#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_Taschenrechner.h"

class Taschenrechner : public QMainWindow
{
    Q_OBJECT

public:
    Taschenrechner(QWidget *parent = nullptr);
    ~Taschenrechner();

private slots:
    void add_digit();
    void on_clearButton_clicked();
    void on_calcButton_clicked();

private:
    Ui::TaschenrechnerClass ui;
};
