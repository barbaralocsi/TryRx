// See https://aka.ms/new-console-template for more information

using ConsoleWriter;

Console.WriteLine("Hello, World!");

// var observer = new MyConsoleObserver<int>();
// var go = new GoUntilStopped(observer);
// go.Go();
// go.Stop();

var myObserver = new MyObserver();

myObserver.Run();


Console.ReadLine();