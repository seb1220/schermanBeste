#include <QFileDialog>
#include <QFileSystemModel>
#include <iostream>

#include "XMLExplorer.h"
#include "lib/tinyxml2.h"

XMLExplorer::XMLExplorer(QWidget *parent)
    : QMainWindow(parent)
{
    ui.setupUi(this);

}

XMLExplorer::~XMLExplorer()
{}

void XMLExplorer::on_openFile_clicked() {
    ui.openFile->setText("Close");

    QFileDialog dialog(this);
    dialog.setFileMode(QFileDialog::AnyFile);
    dialog.setNameFilter(tr("XML files (*.xml)"));
    dialog.setViewMode(QFileDialog::Detail);

    QStringList fileNames;
    if (dialog.exec()) {
        fileNames = dialog.selectedFiles();
    }

    tinyxml2::XMLDocument doc;
    doc.LoadFile(fileNames[0].toStdString().c_str());

    QModelIndex index = model.index(0, 0);
    QVariant data = QVariant::fromValue(doc.RootElement());
//    model.setRootPath("/home/dusack/Downloads/");
    model.setData(index, data);

//    QVariant rootData = QVariant::fromValue(doc.RootElement());
//    model.setData(model.index(0, 0), rootData);
//    model.setData(model.index(1, 0), rootData);
//    model.setData(model.index(1, 1), rootData);

    std::cout << "abc" << std::endl;

    auto data2 = model.data(index);
    auto data3 = data2.value<tinyxml2::XMLElement*>();
    std::cout << data3->Name() << std::endl;
    std::cout << "help" << std::endl;
//    auto data2 = data.value<tinyxml2::XMLElement*>();

    ui.xmlTree->setModel(&model);

    ui.openFile->setText(model.data(model.index(0, 0)).toString());
}
