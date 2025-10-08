// See https://aka.ms/new-console-template for more information

using System.Reactive.Linq;
using System.Text.Json;
using FileSystemExample;

Console.WriteLine("Hello, World!");

var fs = new RxFsEvents("C:\\Barbi\\TryRx\\FileSystemExample\\test");

IObservable<FileSystemEventArgs> configChanges =
    fs.Where(e => Path.GetExtension(e.Name) == ".config");

IObservable<FileSystemEventArgs> deletions =
    fs.Where(e => e.ChangeType == WatcherChangeTypes.Deleted);
    
fs.Subscribe(
    value => Console.WriteLine($"Inline - Received value {JsonSerializer.Serialize(value)}"),
    error => Console.WriteLine($"Inline - Sequence faulted with {JsonSerializer.Serialize(error)}"),
    () => Console.WriteLine("Inline - Sequence terminated")
);

Console.ReadLine();