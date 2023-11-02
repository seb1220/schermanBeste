/********************************************************************************
** Form generated from reading UI file 'XMLExplorer.ui'
**
** Created by: Qt User Interface Compiler version 6.4.2
**
** WARNING! All changes made in this file will be lost when recompiling UI file!
********************************************************************************/

#ifndef UI_XMLEXPLORER_H
#define UI_XMLEXPLORER_H

#include <QtCore/QVariant>
#include <QtWidgets/QApplication>
#include <QtWidgets/QHeaderView>
#include <QtWidgets/QMainWindow>
#include <QtWidgets/QMenuBar>
#include <QtWidgets/QPushButton>
#include <QtWidgets/QStatusBar>
#include <QtWidgets/QToolBar>
#include <QtWidgets/QTreeView>
#include <QtWidgets/QWidget>

QT_BEGIN_NAMESPACE

class Ui_XMLExplorerClass
{
public:
    QWidget *centralWidget;
    QPushButton *openFile;
    QTreeView *xmlTree;
    QMenuBar *menuBar;
    QToolBar *mainToolBar;
    QStatusBar *statusBar;

    void setupUi(QMainWindow *XMLExplorerClass)
    {
        if (XMLExplorerClass->objectName().isEmpty())
            XMLExplorerClass->setObjectName("XMLExplorerClass");
        XMLExplorerClass->resize(600, 400);
        centralWidget = new QWidget(XMLExplorerClass);
        centralWidget->setObjectName("centralWidget");
        openFile = new QPushButton(centralWidget);
        openFile->setObjectName("openFile");
        openFile->setGeometry(QRect(20, 30, 87, 26));
        xmlTree = new QTreeView(centralWidget);
        xmlTree->setObjectName("xmlTree");
        xmlTree->setGeometry(QRect(20, 90, 551, 241));
        XMLExplorerClass->setCentralWidget(centralWidget);
        menuBar = new QMenuBar(XMLExplorerClass);
        menuBar->setObjectName("menuBar");
        menuBar->setGeometry(QRect(0, 0, 600, 23));
        XMLExplorerClass->setMenuBar(menuBar);
        mainToolBar = new QToolBar(XMLExplorerClass);
        mainToolBar->setObjectName("mainToolBar");
        XMLExplorerClass->addToolBar(Qt::TopToolBarArea, mainToolBar);
        statusBar = new QStatusBar(XMLExplorerClass);
        statusBar->setObjectName("statusBar");
        XMLExplorerClass->setStatusBar(statusBar);

        retranslateUi(XMLExplorerClass);

        QMetaObject::connectSlotsByName(XMLExplorerClass);
    } // setupUi

    void retranslateUi(QMainWindow *XMLExplorerClass)
    {
        XMLExplorerClass->setWindowTitle(QCoreApplication::translate("XMLExplorerClass", "XMLExplorer", nullptr));
        openFile->setText(QCoreApplication::translate("XMLExplorerClass", "Open", nullptr));
    } // retranslateUi

};

namespace Ui {
    class XMLExplorerClass: public Ui_XMLExplorerClass {};
} // namespace Ui

QT_END_NAMESPACE

#endif // UI_XMLEXPLORER_H
