// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

IObservable<long> interval = Observable.Interval(TimeSpan.FromMilliseconds(250));
interval.Subscribe(
    Console.WriteLine, 
    () => Console.WriteLine("completed"));

Console.WriteLine("After interval subscribe");

var timer = Observable.Timer(TimeSpan.FromSeconds(1));
timer.Subscribe(
    x=>Console.WriteLine($"From timer: {x}"), 
    () => Console.WriteLine("From timer: completed"));

var immediateTime = Observable.Timer(TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
immediateTime.Subscribe(
    x=>Console.WriteLine($"From immediateTime: {x}"), 
    () => Console.WriteLine("From immediateTime: completed"));


Console.Read();