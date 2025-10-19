using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace _15_AdaptingCommonTypes;

public static class FromEnumerable
{
    public static void Example()
    {
        
    }
    
    // Example code only - do not use!
    public static IObservable<T> ToObservableOversimplified<T>(this IEnumerable<T> source)
    {
        return Observable.Create<T>(o =>
        {
            foreach (var item in source)
            {
                o.OnNext(item);
            }

            o.OnCompleted();

            // Incorrectly ignoring unsubscription.
            return Disposable.Empty;
        });
    }
}