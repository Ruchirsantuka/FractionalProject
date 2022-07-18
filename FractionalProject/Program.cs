// See https://aka.ms/new-console-template for more information
using FractionalNumberType;

Console.WriteLine("Hello, World!");

var fracA = new Fraction(1,1,2);
var fracB = new Fraction(1,1,2);
var fracC = fracA / fracB;

var frac1 = new Fraction(1,3) + new Fraction(1,3) + new Fraction(1,3);

Console.WriteLine(fracC);
Console.WriteLine(frac1);