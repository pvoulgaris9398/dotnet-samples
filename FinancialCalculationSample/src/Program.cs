/*
 * https://quantrl.com/volatility-of-a-portfolio-calculation/#What_is_Portfolio_Volatility_and_Why_is_it_Important
 * 
 * 1. Gather historical returns data
 * 2. Calculate individual security volatility
 * 
 */
using FinancialCalculationSample;

decimal[] returns = [-0.4m, 0.65m, 0.33m, 0.22m, -0.02m, 0.13m, -0.04m, 0.27m];

var result1 = GeometricMeanReturn.Calculate(returns);

Console.WriteLine($"{nameof(GeometricMeanReturn)}: {result1:p4}\n");

var result2 = ArithmeticMeanReturn.Calculate(returns);

Console.WriteLine($"{nameof(ArithmeticMeanReturn)}: {result2:p4}\n");

Root root = 2;
bool logOutput = false;

TestRootCalculator.ExecuteMany(logOutput, Enumerable.Range(1, 20).Select(i => new RootCalculatorInput(root, i)));
