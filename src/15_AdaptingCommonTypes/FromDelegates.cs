using System.Reactive.Linq;

namespace _15_AdaptingCommonTypes;

public static class FromDelegates
{
    public static void Example()
    {
        StartFunc();
    }
    
    static void StartAction()
    {
        var start = Observable.Start(() =>
        {
            Console.Write("Working away");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.Write(".");
            }
        });

        start.Subscribe(
            unit => Console.WriteLine("Unit published"), 
            () => Console.WriteLine("Action completed"));
    }

    static void StartFunc()
    {
        var start = Observable.Start(() =>
        {
            Console.Write("Working away");
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
                Console.Write(".");
            }
            return "Published value";
        });

        start.Subscribe(
            Console.WriteLine, 
            () => Console.WriteLine("Action completed"));
    }
}