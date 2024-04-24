/********************************************************************************
** Form generated from reading UI file 'mainwindow.ui'
**
** Created by: Qt User Interface Compiler version 6.4.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_MAINWINDOW_H
#define UI_MAINWINDOW_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QComboBox>
#include <QtWidgets/QLabel>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTextEdit>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_MainWindow
{
public:
    QWidget *centralwidget;
    QComboBox *comBox_drivers;
    QTextEdit *txt_natio;
    QPushButton *but_suchen;
    QLabel *label_surname;
    QLabel *label_forename;
    QLabel *label_date;
    QLabel *label_count;
    QComboBox *comBox_constr;
    QWidget *widgetChart;
    QMenuBar *menubar;
    QStatusBar *statusbar;

    void setupUi(QMainWindow *MainWindow)
    {
        if (MainWindow->objectName().isEmpty())
            MainWindow->setObjectName("MainWindow");
        MainWindow->resize(800, 600);
        centralwidget = new QWidget(MainWindow);
        centralwidget->setObjectName("centralwidget");
        comBox_drivers = new QComboBox(centralwidget);
        comBox_drivers->setObjectName("comBox_drivers");
        comBox_drivers->setGeometry(QRect(390, 60, 121, 24));
        txt_natio = new QTextEdit(centralwidget);
        txt_natio->setObjectName("txt_natio");
        txt_natio->setGeometry(QRect(20, 60, 131, 31));
        but_suchen = new QPushButton(centralwidget);
        but_suchen->setObjectName("but_suchen");
        but_suchen->setGeometry(QRect(200, 60, 80, 27));
        label_surname = new QLabel(centralwidget);
        label_surname->setObjectName("label_surname");
        label_surname->setGeometry(QRect(30, 140, 131, 31));
        label_forename = new QLabel(centralwidget);
        label_forename->setObjectName("label_forename");
        label_forename->setGeometry(QRect(190, 140, 131, 31));
        label_date = new QLabel(centralwidget);
        label_date->setObjectName("label_date");
        label_date->setGeometry(QRect(350, 130, 121, 41));
        label_count = new QLabel(centralwidget);
        label_count->setObjectName("label_count");
        label_count->setGeometry(QRect(500, 130, 121, 41));
        comBox_constr = new QComboBox(centralwidget);
        comBox_constr->setObjectName("comBox_constr");
        comBox_constr->setGeometry(QRect(610, 60, 111, 24));
        widgetChart = new QWidget(centralwidget);
        widgetChart->setObjectName("widgetChart");
        widgetChart->setGeometry(QRect(60, 190, 661, 351));
        MainWindow->setCentralWidget(centralwidget);
        menubar = new QMenuBar(MainWindow);
        menubar->setObjectName("menubar");
        menubar->setGeometry(QRect(0, 0, 800, 21));
        MainWindow->setMenuBar(menubar);
        statusbar = new QStatusBar(MainWindow);
        statusbar->setObjectName("statusbar");
        MainWindow->setStatusBar(statusbar);

        retranslateUi(MainWindow);

        QMetaObject::connectSlotsByName(MainWindow);
    } // setupUi

    void retranslateUi(QMainWindow *MainWindow)
    {
        MainWindow->setWindowTitle(QCoreApplication::translate("MainWindow", "MainWindow", nullptr));
        but_suchen->setText(QCoreApplication::translate("MainWindow", "Suchen", nullptr));
        label_surname->setText(QString());
        label_forename->setText(QString());
        label_date->setText(QString());
        label_count->setText(QString());
    } // retranslateUi

};

namespace Ui {
    class MainWindow: public Ui_MainWindow {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_MAINWINDOW_H
