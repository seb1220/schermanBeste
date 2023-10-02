#pragma once

#include <QtWidgets/QMainWindow>

class Calculator
{
public:
	Calculator();
	~Calculator();

	static double calculate(QString equation);
};
