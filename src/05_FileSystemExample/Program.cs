// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using System.Text.Json;
using FileSystemExample;

Console.WriteLine("Hello, World!");


//OversimplifiedExample();
MultiSubscriberExample();

Console.ReadLine();

void OversimplifiedExample()
{
    var fs = new RxFsEvents("C:\\Barbi\\TryRx\\src\\05_FileSystemExample\\test");

    fs.Subscribe(
        value => Console.WriteLine($"Inline - Received value {JsonSerializer.Serialize(value)}"),
        error => Console.WriteLine($"Inline - Sequence faulted with {JsonSerializer.Serialize(error)}"),
        () => Console.WriteLine("Inline - Sequence terminated")
    );
}

void MultiSubscriberExample()
{
    var fs = new RxFsEventsMultiSubscriber("C:\\Barbi\\TryRx\\src\\05_FileSystemExample\\test");
    
    IObservable<FileSystemEventArgs> configChanges =
        fs.Where(e => Path.GetExtension(e.Name) == ".config");

    IObservable<FileSystemEventArgs> deletions =
        fs.Where(e => e.ChangeType == WatcherChangeTypes.Deleted);
    
    configChanges.Subscribe(
        value => Console.WriteLine($"Inline Config - Received value {JsonSerializer.Serialize(value)}"),
        error => Console.WriteLine($"Inline Config - Sequence faulted with {JsonSerializer.Serialize(error)}"),
        () => Console.WriteLine("Inline Config - Sequence terminated")
    );
    
    deletions.Subscribe(
        value => Console.WriteLine($"Inline deletion - Received value {JsonSerializer.Serialize(value)}"),
        error => Console.WriteLine($"Inline deletion - Sequence faulted with {JsonSerializer.Serialize(error)}"),
        () => Console.WriteLine("Inline deletion - Sequence terminated")
    );
}