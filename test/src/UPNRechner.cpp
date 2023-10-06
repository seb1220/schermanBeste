#include "../include/UPNRechner.h"

UPNRechner::UPNRechner(QWidget *parent)
    : QMainWindow(parent), ui(new Ui::UPNRechner)
{
    ui->setupUi(this);
}

UPNRechner::~UPNRechner()
{}

void UPNRechner::on_calculateButton_clicked() {
	ui->resultField->setText(QString::number(calculatePostShit(ui.inputField->text())));
}

double UPNRechner::calculatePostShit(QString input) {
	
	QStringList elements = input.split(" ");
	QVector<double> stack;
	QVector<QString> operators;
	
	for (QString element : elements) {
		if (element == "+" || element == "-" || element == "*" || element == "/") {
			double a = stack.last();
			stack.pop_back();
			double b = stack.last();
			stack.pop_back();
			double result = 0;
			if (element == "+") {
				result = a + b;
			}
			else if (element == "-") {
				result = a - b;
			}
			else if (element == "*") {
				result = a * b;
			}
			else if (element == "/") {
				result = a / b;
			}
			stack.push_back(result);
		}
		else {
			stack.push_back(element.toDouble());
		}
	}
	
	return stack.last();
}
