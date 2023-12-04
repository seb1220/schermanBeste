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
#include <QtWidgets/QComboBox>
#include <QtWidgets/QGridLayout>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QTableView>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>
#include "flightmap.h"

QT_BEGIN_NAMESPACE

class Ui_flightplanerClass
{
public:
    QWidget *centralWidget;
    QGridLayout *gridLayout;
    QComboBox *cb_from;
    flightmap *widget;
    QComboBox *cb_to;
    QComboBox *cb_airline;
    QTableView *tableView;
    QPushButton *startSearch;
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
        cb_from = new QComboBox(centralWidget);
        cb_from->setObjectName("cb_from");

        gridLayout->addWidget(cb_from, 0, 0, 1, 1);

        widget = new flightmap(centralWidget);
        widget->setObjectName("widget");

        gridLayout->addWidget(widget, 2, 0, 1, 3);

        cb_to = new QComboBox(centralWidget);
        cb_to->setObjectName("cb_to");

        gridLayout->addWidget(cb_to, 1, 0, 1, 1);

        cb_airline = new QComboBox(centralWidget);
        cb_airline->setObjectName("cb_airline");

        gridLayout->addWidget(cb_airline, 0, 1, 1, 1);

        tableView = new QTableView(centralWidget);
        tableView->setObjectName("tableView");
        QSizePolicy sizePolicy(QSizePolicy::Minimum, QSizePolicy::Expanding);
        sizePolicy.setHorizontalStretch(0);
        sizePolicy.setVerticalStretch(0);
        sizePolicy.setHeightForWidth(tableView->sizePolicy().hasHeightForWidth());
        tableView->setSizePolicy(sizePolicy);

        gridLayout->addWidget(tableView, 0, 2, 2, 1);

        startSearch = new QPushButton(centralWidget);
        startSearch->setObjectName("startSearch");

        gridLayout->addWidget(startSearch, 1, 1, 1, 1);

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
        startSearch->setText(QCoreApplication::translate("flightplanerClass", "Start", nullptr));
    } // retranslateUi

};

namespace Ui {
    class flightplanerClass: public Ui_flightplanerClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_FLIGHTPLANER_H
