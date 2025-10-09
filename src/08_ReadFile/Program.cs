// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;

Console.WriteLine("Hello, World!");

var path = "test.txt";
StreamReader reader = File.OpenText(path);
var fileLines = ReadFileLines(path);

Console.WriteLine("Before subscribe");
var subscribeDisposable = fileLines.Subscribe(
    value => Console.WriteLine($"Inline - Received value {value}"),
    error => Console.WriteLine($"Inline - Sequence faulted with {error}"),
    () => Console.WriteLine("Inline - Sequence terminated")
);
Console.WriteLine("After subscribe");

Console.ReadLine();

IObservable<string> ReadFileLines(string path) =>
    Observable.Create<string>(async (observer, cancellationToken) =>
    {
        using (StreamReader reader = File.OpenText(path))
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                string? line = await reader.ReadLineAsync(cancellationToken).ConfigureAwait(false);
                if (line is null)
                {
                    break;
                }

                observer.OnNext(line);
            }

            observer.OnCompleted();
        }
    });