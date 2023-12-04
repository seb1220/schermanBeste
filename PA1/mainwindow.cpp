#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <qfiledialog.h>
#include "models.h"

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

void MainWindow::on_choosePicture_clicked()
{
    filename = QFileDialog::getOpenFileName(this, "Select .jpeg or .jpg files", "", "*.jpeg *.jpg");
    ui->fileName->setText(filename.split("/").last());

    ui->picture->setFilename(filename);
    ui->detailFilename->setText(filename.split("/").last());
}


void MainWindow::on_insertPicture_clicked()
{
    if (!manager.addNewEntry(filename, ui->fileName->text(), ui->priority->value(), ui->descriptionEdit->text()))
        ui->errorLabel->setText("Fehler: Die angegebene Priorität existiert bereits!");
    else
        ui->errorLabel->setText("");
}

void MainWindow::on_nextPicture_clicked()
{
    pic p = manager.getNextEntry(ui->priority->value());

    ui->fileName->setText("");

    ui->picture->setFilename(p.path);

    ui->priority->setValue(p.priority);
    ui->descriptionEdit->setText(p.description);

    ui->detailFilename->setText(p.name);
    ui->detailDescription->setText(p.description);
}
