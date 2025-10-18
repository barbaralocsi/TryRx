// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;

Console.WriteLine("Hello, World!");


static IObservable<int> WithoutDeferal()
{
    Console.WriteLine("Doing some startup work...");
    return Observable.Range(1, 3);
}

static IObservable<int> WithDeferal()
{
    return Observable.Defer(() =>
    {
        Console.WriteLine("Doing some startup work...");
        return Observable.Range(1, 3);
    });
}

Console.WriteLine("Calling factory method");
IObservable<int> s = WithDeferal();

Console.WriteLine("First subscription");
s.Subscribe(Console.WriteLine);

Console.WriteLine("Second subscription");
s.Subscribe(Console.WriteLine);