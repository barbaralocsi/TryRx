// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using ConsoleWriter;

Console.WriteLine("Hello, World!");

var observer = new MyConsoleObserver<long>();

IObservable<long> ticks = Observable.Timer(
    dueTime: TimeSpan.Zero,
    period: TimeSpan.FromSeconds(1));

ticks.Subscribe(observer);

ticks.Subscribe(
    value => Console.WriteLine($"Inline - Received value {value}"),
    error => Console.WriteLine($"Inline - Sequence faulted with {error}"),
    () => Console.WriteLine("Inline - Sequence terminated")
);

Console.ReadLine();