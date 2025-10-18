// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

var observable = Fibonacci();

observable.Subscribe(onNext => Console.WriteLine($"OnNext: {onNext}"),
    onError => Console.WriteLine($"OnError: {onError}"),
    () => Console.WriteLine($"OnCompleted.")
    );

Console.WriteLine("After subscribe");

Console.WriteLine("Before custom timer");

var customTimer = CustomTimer(TimeSpan.Zero, TimeSpan.FromMilliseconds(250));
customTimer.Subscribe(
    x=>Console.WriteLine($"From customTimer: {x}"), 
    () => Console.WriteLine("From customTimer: completed"));

Console.WriteLine("After custom subscribe");

Console.WriteLine("Before variable");
var variableTimer = Backoff(TimeSpan.FromMilliseconds(250));
variableTimer.Subscribe(
    x=>Console.WriteLine($"From variableTimer: {x}"), 
    () => Console.WriteLine("From variableTimer: completed"));

Console.WriteLine("After variable timer");

Console.ReadLine();

IObservable<BigInteger> Fibonacci()
{
    return Observable.Generate(
        (v1: new BigInteger(1), v2: new BigInteger(1)),
        value => true, // It never ends!
        value => (value.v2, value.v1 + value.v2),
        value => value.v1,
        x=> TimeSpan.FromMilliseconds(250));
}

IObservable<int> CustomTimer(TimeSpan initialDelay, TimeSpan laterDelays)
{
    return Observable.Generate(
        initialState: 0,
        condition: a => true,
        iterate: (a) => a + 1,
        resultSelector: a => a,
        timeSelector: (a) => a == 0 ? initialDelay : laterDelays
    );
}

IObservable<int> CustomInterval(TimeSpan delay)
{
    return Observable.Generate(
        initialState: 0,
        condition: a => true,
        iterate: (a) => a + 1,
        resultSelector: a => a,
        timeSelector: a => delay
    );
}

IObservable<int> Backoff(TimeSpan delay)
{
    return Observable.Generate(
        initialState: 0,
        condition: a => true,
        iterate: (a) => a + 1,
        resultSelector: a => a,
        timeSelector: (a) => a * delay
    );
}