// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var f1 = new Fracao(5, 10);
Console.WriteLine(f1.Numerador == 1); // True
Console.WriteLine(f1.Denominador == 2);
Console.WriteLine(f1.ToString() == "1/2");
Console.WriteLine(f1); // 1/2

var f2 = new Fracao(3); // 3 inteiros
Console.WriteLine(f2); // "3/1"

var f3 = new Fracao("30/40"); // Necessário fazer o split e parse da string, ver "".Split e int.Parse
Console.WriteLine(f3.Numerador == 3);
Console.WriteLine(f3.Denominador == 4);
Console.WriteLine(f3); // "3/4"

var f4 = new Fracao(0.345);
// https://www.wolframalpha.com/input/?i=rationalize+0.345
Console.WriteLine(f4); // "69/200"

var f5 = new Fracao(0.4);
Console.WriteLine(f5.Numerador == 2);
Console.WriteLine(f5.Denominador == 5);
Console.WriteLine(f5); // "2/5"

// https://www.wolframalpha.com/input?i=1%2F2+%2B+2%2F1
var f6 = f1.Somar(2); // 1/2 + 2/1
Console.WriteLine(f6); // "5/2"

var f7 = f1 + 2;
Console.WriteLine(f7); // "5/2"

var f8 = f1 + 0.5; // 5/2 + 1/2
Console.WriteLine(f8); // "3/1"

var f9 = f1 + 0.2862; // https://www.wolframalpha.com/input/?i=rationalize+0.2862
Console.WriteLine(f1); // "16431/5000"

var f10 = f3 + "7/8";
Console.WriteLine(f3.Numerador == 13);
Console.WriteLine(f3.Denominador == 8);
Console.WriteLine(f3); // "13/8"

var f11 = f3 + 6.45;
Console.WriteLine(f3.Numerador == 323);
Console.WriteLine(f3.Denominador == 40);
Console.WriteLine(f3); // "323/40"

Fracao f12 = new(1, 5);
Fracao f13 = new(1, 3);
Fracao f14 = new(125, 375);
Fracao f15 = new(15, 75);

Console.WriteLine(f12.Equals(f14)); // false

// escreva o operador == e !=
Console.WriteLine(f12 == f14); // false
Console.WriteLine(f12 != f14); // true

Console.WriteLine(f12 == f15); // true
Console.WriteLine(f13 == f14); // true

Console.WriteLine(new Fracao("3/19").Equals(new Fracao(3, 19))); // true

// escreva os operadores > < >= e <=

var f16 = new Fracao(2, 12);
var f17 = new Fracao(3, 4);
var f18 = new Fracao(9, 10);
var f19 = new Fracao(5);
var f20 = new Fracao(24, 18);
var f21 = new Fracao(16, 8);
var f22 = new Fracao(1, 8);
var f23 = new Fracao(10, 80);

// Todas estas assertivas devem imprimir true
Console.WriteLine(f16 < f17);
Console.WriteLine(f18 > f17);
Console.WriteLine(f19 > f18);
Console.WriteLine(f12 >= f15);
Console.WriteLine(f16 < f20);

// Consultas:
Console.WriteLine(f16.IsImpropria); // False
Console.WriteLine(f16.IsPropria); // True
Console.WriteLine(f20.IsImpropria); // True
Console.WriteLine(f20.IsAparente); // False
Console.WriteLine(f21.IsPropria); // False
Console.WriteLine(f21.IsAparente); // True
Console.WriteLine(f21.IsUnitaria); // False
Console.WriteLine(f22.IsUnitaria); // True
Console.WriteLine(f23.IsUnitaria); // True

// O que acontece se:
var f24 = new Fracao(5, 0);
// Usar ArgumentOutOfRangeException.ThrowIf... e impedir a instanciação!
// Pesquisar sobre.