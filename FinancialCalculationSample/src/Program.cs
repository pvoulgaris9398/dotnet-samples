using FinancialCalculationSample;

TestSecantMethod.Run();

TestNewtonRaphsonMethod.Run();

/*
 * https://quantrl.com/volatility-of-a-portfolio-calculation/#What_is_Portfolio_Volatility_and_Why_is_it_Important
 * 
 * 1. Gather historical returns data
 * 2. Calculate individual security volatility
 * 
 */

WriteLine(new string('*', 80));

Root root = 2;
bool logOutput = false;

TestRootCalculator.ExecuteMany(logOutput, Enumerable.Range(1, 20).Select(i => new RootCalculatorInput(root, i)));


//decimal[] returns = [-0.4m, 0.65m, 0.33m, 0.22m, -0.02m, 0.13m, -0.04m, 0.27m];

double[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };

double[] means = [2, 3, 4];

var result = CovarianceMatrix.Calculate(matrix, means);

WriteLine(result);

TestEulerMethod.Execute();

/*
var result1 = GeometricMeanReturn.Calculate(returns);

Console.WriteLine($"{nameof(GeometricMeanReturn)}: {result1:p4}\n");

var result2 = ArithmeticMeanReturn.Calculate(returns);

Console.WriteLine($"{nameof(ArithmeticMeanReturn)}: {result2:p4}\n");

*/