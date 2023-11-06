#ifndef FLIGHTPLANER_H
#define FLIGHTPLANER_H

#include <QtWidgets/QMainWindow>
#include <QFileSystemModel>
#include "ui_flightplaner.h"
#include "dbmanager.h"

class flightplaner : public QMainWindow
{
    Q_OBJECT

public:
    flightplaner(QWidget *parent = nullptr);
    ~flightplaner();

private slots:
    void findFlightPaths();

private:
    Ui::flightplanerClass ui;
    QFileSystemModel model;

    dbmanager db;
};

#endif // FLIGHTPLANER_H
