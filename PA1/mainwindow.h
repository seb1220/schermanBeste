#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <dbmanager.h>

QT_BEGIN_NAMESPACE
namespace Ui { class MainWindow; }
QT_END_NAMESPACE

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    MainWindow(QWidget *parent = nullptr);
    ~MainWindow();

private slots:
    void on_choosePicture_clicked();

    void on_insertPicture_clicked();

    void on_nextPicture_clicked();

private:
    Ui::MainWindow *ui;
    QString filename;
    dbmanager manager;
};
#endif // MAINWINDOW_H
