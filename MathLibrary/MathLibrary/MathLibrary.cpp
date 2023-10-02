#include "MathLibrary.h"
#include "exprtk.hpp"


double MathLibrary::calculate(QString equation)
{
	exprtk::expression<double> expression;
	exprtk::parser<double> parser;

	if (!parser.compile(equation.toUtf8().constData(), expression))
	{
		printf("Compilation error...\n");
		return 42069.0;
	}

	return expression.value();
}
