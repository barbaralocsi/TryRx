// See https://aka.ms/new-console-template for more information

using System.Reactive.Disposables;
using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

var someNumbersObservable = Observable.Create<int>(
        (IObserver<int> observer) =>
        {
            Console.WriteLine("Callback");
            observer.OnNext(1);
            observer.OnNext(2);
            observer.OnNext(3);
            observer.OnCompleted();

            return Disposable.Empty;
        });
        
Console.WriteLine("Before subscribe");

var subscriptionDisposable = someNumbersObservable.Subscribe(
    value => Console.WriteLine($"Inline - Received value {value}"),
    error => Console.WriteLine($"Inline - Sequence faulted with {error}"),
    () => Console.WriteLine("Inline - Sequence terminated")
);

Console.WriteLine("After subscribe");

Console.Read();