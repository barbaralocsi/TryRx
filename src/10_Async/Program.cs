using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Disposables;
using System.Reactive.Threading.Tasks;

// This is not based on the book, but based on a github issue: https://github.com/dotnet/reactive/issues/459

Observable.Interval(TimeSpan.FromSeconds(1))
    .SubscribeAsync(number => DoSomeWorkAsync(number));

Console.ReadLine();

async Task DoSomeWorkAsync(long number)
{
    Console.WriteLine($"DoSomeWorkAsync BEGIN '{number}'");
    await Task.Delay(TimeSpan.FromSeconds(3));
    Console.WriteLine($"DoSomeWorkAsync END '{number}'");
}

#if true
public static class MyObservableExtensions
{
    public static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
        source
            .Select(number => Observable.FromAsync(() => onNextAsync(number)))
            .Concat()
            .Subscribe();

    public static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
        source
            .Select(number => Observable.FromAsync(() => onNextAsync(number)))
            .Merge()
            .Subscribe();

    public static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync, int maxConcurrent) =>
        source
            .Select(number => Observable.FromAsync(() => onNextAsync(number)))
            .Merge(maxConcurrent)
            .Subscribe();
}
#else
public static class MyObservableExtensions
{
    public static IDisposable SubscribeAsync<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
        source
            .Select(number => onNextAsync(number).ToObservable()) // note ToObservable instead of FromAsync!
            .Concat()
            .Subscribe();

    public static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync) =>
        source
            .Select(number => onNextAsync(number).ToObservable()) // note ToObservable instead of FromAsync!
            .Merge()
            .Subscribe();

    public static IDisposable SubscribeAsyncConcurrent<T>(this IObservable<T> source, Func<T, Task> onNextAsync, int maxConcurrent) =>
        source
            .Select(number => onNextAsync(number).ToObservable()) // note ToObservable instead of FromAsync!
            .Merge(maxConcurrent)
            .Subscribe();
}
#endif