using InterfaceTest;


var duck = new CompositionRoot().GetDuck();
string quacks = duck.Quack(5);

Console.WriteLine(quacks);