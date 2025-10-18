using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using FileSystemExample;

namespace _15_AdaptingCommonTypes;

public static class FromEvents
{
    public static void Example()
    {
        var folder = "C:\\Barbi\\TryRx\\src\\15_AdaptingCommonTypes\\test";
        CurrentExample(folder);
        //OversimplifiedExample(folder);
    }

    private static void CurrentExample(string folder)
    {
        FileSystemWatcher watcher = new (folder);
        
        // Here nameof(watcher.Created) is bad - uses reflection - might cause issues with AOT and trimming (MissingMethodException)
        // IObservable<EventPattern<FileSystemEventArgs>> changeEvents = Observable
        //     .FromEventPattern<FileSystemEventArgs>(watcher, nameof(watcher.Created));
        
        watcher.EnableRaisingEvents = true;
        
        IObservable<EventPattern<FileSystemEventArgs>> changeEvents = Observable
            .FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                h => watcher.Changed += h,
                h => watcher.Changed -= h);

        watcher.Changed += (sender, args) =>
        {
            Console.WriteLine($"Using the event directly: {JsonSerializer.Serialize(args)}");
        };
        
        changeEvents.Subscribe(
            onNext => Console.WriteLine($"Receved: {JsonSerializer.Serialize(onNext)}"), 
            () => Console.WriteLine("Action completed"));
    }
    
    private static void OversimplifiedExample(string folder)
    {
        var fs = new RxFsEvents(folder);

        fs.Subscribe(
            value => Console.WriteLine($"Inline - Received value {JsonSerializer.Serialize(value)}"),
            error => Console.WriteLine($"Inline - Sequence faulted with {JsonSerializer.Serialize(error)}"),
            () => Console.WriteLine("Inline - Sequence terminated")
        );
    }
    
}