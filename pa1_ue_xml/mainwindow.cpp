#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QDebug>
#include <QFileDialog>
#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_actionOpen_File_triggered()
{
    QString filename = QFileDialog::getOpenFileName(this, "Select .xml files", "", "*.xml");

    QDir dir = QFileDialog::getExistingDirectory(this, "Open dir", "");

    for (auto fileinfo : dir.entryInfoList()) {
        qDebug() << fileinfo.fileName();
    }

    doc.LoadFile(filename.toStdString().c_str());

    if (doc.Error())
        QMessageBox::critical(this, "Error", "Bad xml File");

    displayInTree(doc.RootElement(), ui->xmlWidget->invisibleRootItem());
}

void MainWindow::displayInTree(tinyxml2::XMLElement *xmlElement, QTreeWidgetItem *uiElement)
{
    uiElement->setText(0, xmlElement->Name());
    uiElement->setData(0, Qt::UserRole, QVariant::fromValue(xmlElement));

    for (auto* element = xmlElement->FirstChildElement(); element != nullptr; element = element->NextSiblingElement()) {
        auto *item = new QTreeWidgetItem(uiElement);
        displayInTree(element, item);
    }
}

void MainWindow::on_xmlWidget_currentItemChanged(QTreeWidgetItem *current, QTreeWidgetItem *previous)
{
    auto element = (tinyxml2::XMLElement*) current->data(0, Qt::UserRole).value<tinyxml2::XMLElement*>();

    ui->content->setText(element->GetText());

    ui->attributes->clear();
    for (auto attribute = element->FirstAttribute(); attribute != nullptr; attribute = attribute->Next()) {
        ui->attributes->addItem(attribute->Name(), QVariant::fromValue(attribute));
    }
    //ui->attributes->setText(element->FirstAttribute()->Value());
}

void MainWindow::on_attributes_currentIndexChanged(int index)
{
    auto attribute = ui->attributes->itemData(index).value<tinyxml2::XMLAttribute*>();

    ui->value->clear();

    qDebug() << attribute;

    if (attribute != nullptr)
        ui->value->setText(attribute->Value());
}
