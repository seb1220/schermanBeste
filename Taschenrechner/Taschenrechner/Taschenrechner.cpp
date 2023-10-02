#include "Taschenrechner.h"
#include "Calculator.h"

Taschenrechner::Taschenrechner(QWidget *parent)
    : QMainWindow(parent)
{
    ui.setupUi(this);

	connect(ui.zeroButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.oneButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.twoButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.threeButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.fourButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.fiveButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.sixButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.sevenButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.eightButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.nineButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.plusButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.minusButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.multiplyButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.divideButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.openBracketButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.closeBracketButton, SIGNAL(clicked()), this, SLOT(add_digit()));
	connect(ui.commaButton, SIGNAL(clicked()), this, SLOT(add_digit()));
}

Taschenrechner::~Taschenrechner()
{}

void Taschenrechner::add_digit()
{
	ui.equationField->setText(ui.equationField->text() + " " + qobject_cast<QPushButton*>(sender())->text());
}

void Taschenrechner::on_clearButton_clicked()
{
	ui.equationField->setText("");
}

void Taschenrechner::on_calcButton_clicked()
{
	ui.equationField->setText(QString::number(Calculator::calculate(ui.equationField->text())));
}
