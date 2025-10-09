// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
IHost host = builder.Build();

Console.WriteLine("Hello, World!");

IObservable<char> KeyPresses() =>
    Observable.Create<char>(observer =>
    {
        CancellationTokenSource cts = new();
        Task.Run(() =>
        {
            while (!cts.IsCancellationRequested)
            {
                ConsoleKeyInfo ki = Console.ReadKey();
                observer.OnNext(ki.KeyChar);
            }
        });

        return () => cts.Cancel();
    });

var keyPresses = KeyPresses();
var subscribeDisposable = keyPresses.Subscribe(
    value => Console.WriteLine($"Inline - Received value {value}"),
    error => Console.WriteLine($"Inline - Sequence faulted with {error}"),
    () => Console.WriteLine("Inline - Sequence terminated")
);

await Task.Delay(5000);
subscribeDisposable.Dispose();

host.Run();