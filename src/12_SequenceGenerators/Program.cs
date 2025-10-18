// See https://aka.ms/new-console-template for more information

using System.Numerics;
using System.Reactive.Disposables;
using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

IObservable<int> range1 = Observable.Range(10, 15);
IObservable<int> range2 = RangeWithCreate(10, 15);
IObservable<int> range3 = RangeWithGenerate(10, 15);
IObservable<BigInteger> range4 = Fibonacci();

range1.Subscribe(Console.WriteLine, () =>
{
    Console.WriteLine("Completed");
});

range2.Subscribe(Console.WriteLine, () =>
{
    Console.WriteLine("Completed");
});

range3.Subscribe(Console.WriteLine, () =>
{
    Console.WriteLine("Completed");
});

range4.Subscribe(onNext => Console.WriteLine(onNext), () =>
{
    Console.WriteLine("Completed");
});

Console.WriteLine("after");
//Console.Read();

// Not the best way to do it!
IObservable<int> RangeWithCreate(int start, int count) =>
    Observable.Create<int>(observer =>
    {
        for (int i = 0; i < count; ++i)
        {
            observer.OnNext(start + i);
        }

        return Disposable.Empty;
    });
    
// Example code only
static IObservable<int> RangeWithGenerate(int start, int count)
{
    int max = start + count;
    return Observable.Generate(
        start, 
        value => value < max, 
        value => value + 1, 
        value => value);
}

IObservable<BigInteger> Fibonacci()
{
    return Observable.Generate(
        (v1: new BigInteger(1), v2: new BigInteger(1)),
        value => true, // It never ends!
        value => (value.v2, value.v1 + value.v2),
        value => value.v1);
}
