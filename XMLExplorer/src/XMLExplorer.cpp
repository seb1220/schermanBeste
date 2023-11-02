#include <QFileDialog>
#include <QFileSystemModel>
#include <iostream>

#include "XMLExplorer.h"
#include "lib/tinyxml2.h"

XMLExplorer::XMLExplorer(QWidget *parent)
        : QMainWindow(parent) {
    ui.setupUi(this);

}

XMLExplorer::~XMLExplorer()
= default;

void XMLExplorer::on_openFile_clicked() {
    QFileDialog dialog(this);
    dialog.setFileMode(QFileDialog::AnyFile);
    dialog.setNameFilter(tr("XML files (*.xml)"));
    dialog.setViewMode(QFileDialog::Detail);

    QStringList fileNames;
    if (dialog.exec()) {
        fileNames = dialog.selectedFiles();
    }

    doc.LoadFile(fileNames[0].toStdString().c_str());

    ui.treeWidget->setHeaderLabels(QStringList() << "XML Tree");
    ui.treeWidget->setCurrentItem(new QTreeWidgetItem(ui.treeWidget, QStringList(QString(doc.RootElement()->Name()))));

    fillTreeWidget(doc.RootElement());
}

void XMLExplorer::fillTreeWidget(tinyxml2::XMLElement *element) {

    auto *item = new QTreeWidgetItem(ui.treeWidget->currentItem(), QStringList(QString(element->Name())));
    item->setData(0, Qt::UserRole, QVariant::fromValue(element));
    ui.treeWidget->currentItem()->addChild(item);
    ui.treeWidget->setCurrentItem(item);

    for (auto child = element->FirstChildElement(); child != nullptr; child = child->NextSiblingElement()) {
        fillTreeWidget(child);
    }

    ui.treeWidget->setCurrentItem(ui.treeWidget->currentItem()->parent());
}

void XMLExplorer::on_treeWidget_currentItemChanged(QTreeWidgetItem *current, QTreeWidgetItem *previous) {
    ui.tableWidget->clear();
    ui.tableWidget->insertRow(0);
    ui.tableWidget->insertColumn(0);
//    ui.tableWidget->insertColumn(1);
//    ui.tableWidget->setItem(0, 0, new QTableWidgetItem("test"));
//    ui.tableWidget->setItem(0, 1, new QTableWidgetItem("test"));

    ui.listWidget->clear();

    auto *element = current->data(0, Qt::UserRole).value<tinyxml2::XMLElement *>();

    if (element != nullptr) {
        for (auto attribute = element->FirstAttribute(); attribute != nullptr; attribute = attribute->Next()) {
            ui.listWidget->addItem(QString(attribute->Name()) + ": " + QString(attribute->Value()));
            ui.tableWidget->setItem(0, 0, new QTableWidgetItem(QString(attribute->Name())));
            ui.tableWidget->setItem(0, 1, new QTableWidgetItem(QString(attribute->Value())));
        }
    }
}

tinyxml2::XMLElement *XMLExplorer::findElement(tinyxml2::XMLElement *element, const QString &name) {
    if (element->Name() == name) {
        return element;
    }
    for (auto child = element->FirstChildElement(); child != nullptr; child = child->NextSiblingElement()) {
        findElement(child, name);
        if (child->Name() == name) {
            return child;
        }
    }

    return nullptr;
}

void XMLExplorer::on_tableWidget_itemChanged(QTableWidgetItem *item) {
//    std::cout << item->text().toStdString() << std::endl;

    auto* element = ui.treeWidget->selectedItems()[0]->data(0, Qt::UserRole).value<tinyxml2::XMLElement *>();

    if (element != nullptr) {
//        std::cout << element->Name() << std::endl;
//        if (item->column() == 1 && ui.tableWidget->item(item->row(), 0) != nullptr)
//            element->SetAttribute(ui.tableWidget->item(item->row(), 0)->text().toStdString().c_str(), item->text().toStdString().c_str());
    }
}
