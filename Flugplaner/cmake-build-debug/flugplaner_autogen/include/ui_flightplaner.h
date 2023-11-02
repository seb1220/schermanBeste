/********************************************************************************
** Form generated from reading UI file 'flightplaner.ui'
**
** Created by: Qt User Interface Compiler version 6.4.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_FLIGHTPLANER_H
#define UI_FLIGHTPLANER_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>
#include "flightmap.h"

QT_BEGIN_NAMESPACE

class Ui_flightplanerClass
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    flightmap *widget;
    QPushButton *pushButton;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *flightplanerClass)
    {
        if (flightplanerClass->objectName().isEmpty())
            flightplanerClass->setObjectName("flightplanerClass");
        flightplanerClass->resize(1071, 626);
        centralWidget = new QWidget(flightplanerClass);
        centralWidget->setObjectName("centralWidget");
        gridLayout = new QGridLayout(centralWidget);
        gridLayout->setSpacing(6);
        gridLayout->setContentsMargins(11, 11, 11, 11);
        gridLayout->setObjectName("gridLayout");
        widget = new flightmap(centralWidget);
        widget->setObjectName("widget");

        gridLayout->addWidget(widget, 1, 0, 1, 1);

        pushButton = new QPushButton(centralWidget);
        pushButton->setObjectName("pushButton");

        gridLayout->addWidget(pushButton, 0, 0, 1, 1);

        flightplanerClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(flightplanerClass);
        menuBar->setObjectName("menuBar");
        menuBar->setGeometry(QRect(0, 0, 1071, 23));
        flightplanerClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(flightplanerClass);
        mainToolBar->setObjectName("mainToolBar");
        flightplanerClass->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(flightplanerClass);
        statusBar->setObjectName("statusBar");
        flightplanerClass->setStatusBar(statusBar);

        retranslateUi(flightplanerClass);

        QMetaObject::connectSlotsByName(flightplanerClass);
    } // setupUi

    void retranslateUi(QMainWindow *flightplanerClass)
    {
        flightplanerClass->setWindowTitle(QCoreApplication::translate("flightplanerClass", "flightplaner", nullptr));
        pushButton->setText(QCoreApplication::translate("flightplanerClass", "PushButton", nullptr));
    } // retranslateUi

};

namespace Ui {
    class flightplanerClass: public Ui_flightplanerClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_FLIGHTPLANER_H
