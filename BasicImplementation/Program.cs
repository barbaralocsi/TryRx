// See https://aka.ms/new-console-template for more information

using BasicImplementation;

Console.WriteLine("Hello, World!");


var numbers = new MySequenceOfNumbers();
numbers.Subscribe(
    number => Console.WriteLine($"Received value: {number}"),
    () => Console.WriteLine("Sequence terminated"));
    
    
Console.Read();