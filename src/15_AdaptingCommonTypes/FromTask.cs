using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace _15_AdaptingCommonTypes;

public static class FromTask
{
    public static void Example()
    {
        //UsingToObservable();
        UsingFromAsync();
    }

    private static void UsingToObservable()
    {
        Task<string> t = Task.Run(async () =>
        {
            Console.WriteLine("Task running...");
            await Task.Delay(1000);
            Console.WriteLine("Task done waiting");
            return "Test";
        });
        IObservable<string> source = t.ToObservable();
        source.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
        source.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
    }
    
    private static void UsingFromAsync()
    {
        // Task<string> t = Task.Run(async () =>
        // {
        //     Console.WriteLine("Task running...");
        //     await Task.Delay(1000);
        //     Console.WriteLine("Task done waiting");
        //     return "Test";
        // });
        //IObservable<string> source = t.ToObservable();

        IObservable<string> source = Observable.FromAsync(async () =>
        {
            Console.WriteLine("Task running...");
            await Task.Delay(50);
            return "Test";
        });
        
        source.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
        source.Subscribe(
            Console.WriteLine,
            () => Console.WriteLine("completed"));
    }
}