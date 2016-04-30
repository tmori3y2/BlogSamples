using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Linq;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MySampleExtensions
{
    [CLSCompliant(true)]
    public static class MyReactiveExtensions
    {
        public static IObservable<string> Convert(this IObservable<decimal> self, int decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.Select(d => d.Convert(decimals));
        }

        public static IObservable<string> Convert(this IObservable<int> self)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.Select(i => i.Convert());
        }
        
        public static IObservable<decimal> ConvertBack(this IObservable<string> self, int decimals)
        {
            return self.Select(s => s.ConvertBack(decimals));
        }

        public static IObservable<int> ConvertBack(this IObservable<string> self)
        {
            return self.Select(s => s.ConvertBack());
        }

        public static IObservable<bool> ChangedAsObservable<T>(this IObservable<T> self)
        {
            return self.ChangedAsObservable(true);
        }

        public static IObservable<bool> ChangedAsObservable<T>(this IObservable<T> self, bool skipFirst)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            var result = self.AsObservable();
            if (skipFirst)
            {
                result = result.Skip(1);
            }
            return result.Select(_ => true);
        }
    }
}
