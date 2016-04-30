using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Linq;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace MySampleExtensions
{
    [CLSCompliant(false)]
    public static class MyReactivePropertyExtensions
    {
        public static IObservable<string> Convert(this IObservable<decimal> self, IReadOnlyReactiveProperty<int> decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (decimals == null)
            {
                throw new ArgumentNullException("decimals");
            }

            return self.Select(d => d.Convert(decimals.Value));
        }

        public static IObservable<decimal> ConvertBack(this IObservable<string> self, IReadOnlyReactiveProperty<int> decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (decimals == null)
            {
                throw new ArgumentNullException("decimals");
            }

            return self.Select(s => s.ConvertBack(decimals.Value));
        }

        public static ReactiveProperty<string> SetDecimalValidateNotifyError(this ReactiveProperty<string> self, decimal lowerBound, decimal upperBound, int decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.SetValidateNotifyError(s => s.ValidateDecimal(lowerBound, upperBound, decimals));
        }

        public static ReactiveProperty<string> SetDecimalValidateNotifyError(this ReactiveProperty<string> self, decimal lowerBound, decimal upperBound, IReadOnlyReactiveProperty<int> decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (decimals == null)
            {
                throw new ArgumentNullException("decimals");
            }

            return self.SetValidateNotifyError(s => s.ValidateDecimal(lowerBound, upperBound, decimals.Value));
        }

        public static ReactiveProperty<string> SetDecimalValidateNotifyError(this ReactiveProperty<string> self, decimal lowerBound, IReadOnlyReactiveProperty<decimal> upperBound, IReadOnlyReactiveProperty<int> decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (upperBound == null)
            {
                throw new ArgumentNullException("upperBound");
            }

            if (decimals == null)
            {
                throw new ArgumentNullException("decimals");
            }

            return self.SetValidateNotifyError(s => s.ValidateDecimal(lowerBound, upperBound.Value, decimals.Value, true, true, null));
        }

        public static IDisposable SubscribeAndReformat(this IObservable<decimal> self, IReactiveProperty<decimal> source, IReactiveProperty<string> target, int decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.Subscribe(d =>
            {
                // Subscribes to source.
                source.Value = d;
                // Reformats target if source not changed.
                target.Value = source.Value.Convert(decimals);
            });
        }

        public static IDisposable SubscribeAndReformat(this IObservable<decimal> self, IReactiveProperty<decimal> source, IReactiveProperty<string> target, IReadOnlyReactiveProperty<int> decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            if (decimals == null)
            {
                throw new ArgumentNullException("decimals");
            }

            return self.Subscribe(d =>
            {
                // Subscribes to source.
                source.Value = d;
                // Reformats target if source not changed.
                target.Value = source.Value.Convert(decimals.Value);
            });
        }

        public static IDisposable SubscribeAndReformat(this IObservable<int> self, IReactiveProperty<int> source, IReactiveProperty<string> target)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.Subscribe(i =>
            {
                // Subscribes to source.
                source.Value = i;
                // Reformats target if source not changed.
                target.Value = source.Value.Convert();
            });
        }
    }
}
