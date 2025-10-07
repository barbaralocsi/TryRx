// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using ConsoleWriter;

Console.WriteLine("Hello, World!");

var observer = new MyConsoleObserver<long>();

IObservable<long> ticks = Observable.Timer(
    dueTime: TimeSpan.Zero,
    period: TimeSpan.FromSeconds(1));
    
ticks.Subscribe(observer);

Console.ReadLine();