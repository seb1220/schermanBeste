/********************************************************************************
** Form generated from reading UI file 'UPNRechner.ui'
**
** Created by: Qt User Interface Compiler version 6.4.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_UPNRECHNER_H
#define UI_UPNRECHNER_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QLabel>
#include <QtWidgets/QLineEdit>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_UPNRechnerClass
{
public:
    QWidget *centralWidget;
    QPushButton *calculateButton;
    QLineEdit *inputField;
    QLabel *resultField;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *UPNRechnerClass)
    {
        if (UPNRechnerClass->objectName().isEmpty())
            UPNRechnerClass->setObjectName("UPNRechnerClass");
        UPNRechnerClass->resize(600, 400);
        centralWidget = new QWidget(UPNRechnerClass);
        centralWidget->setObjectName("centralWidget");
        calculateButton = new QPushButton(centralWidget);
        calculateButton->setObjectName("calculateButton");
        calculateButton->setGeometry(QRect(210, 170, 75, 24));
        inputField = new QLineEdit(centralWidget);
        inputField->setObjectName("inputField");
        inputField->setGeometry(QRect(190, 120, 113, 21));
        resultField = new QLabel(centralWidget);
        resultField->setObjectName("resultField");
        resultField->setGeometry(QRect(220, 220, 49, 16));
        UPNRechnerClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(UPNRechnerClass);
        menuBar->setObjectName("menuBar");
        menuBar->setGeometry(QRect(0, 0, 600, 22));
        UPNRechnerClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(UPNRechnerClass);
        mainToolBar->setObjectName("mainToolBar");
        UPNRechnerClass->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(UPNRechnerClass);
        statusBar->setObjectName("statusBar");
        UPNRechnerClass->setStatusBar(statusBar);

        retranslateUi(UPNRechnerClass);

        QMetaObject::connectSlotsByName(UPNRechnerClass);
    } // setupUi

    void retranslateUi(QMainWindow *UPNRechnerClass)
    {
        UPNRechnerClass->setWindowTitle(QCoreApplication::translate("UPNRechnerClass", "UPNRechner", nullptr));
        calculateButton->setText(QCoreApplication::translate("UPNRechnerClass", "Calculate", nullptr));
        resultField->setText(QString());
    } // retranslateUi

};

namespace Ui {
    class UPNRechnerClass: public Ui_UPNRechnerClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_UPNRECHNER_H
