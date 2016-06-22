// <copyright file="MyReactivePropertyExtensions.cs" company="tmori3y2.hatenablog.com">
// Copyright (c) 2016 tmori3y2.hatenablog.com. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/06/22</date>
// <summary>Implements my reactive property extensions class</summary>
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
    /// <summary>my reactive property extensions.</summary>
    /// <remarks>tmori3y2, 2016/06/22.</remarks>
    [CLSCompliant(false)]
    public static class MyReactivePropertyExtensions
    {
        /// <summary>An IObservable&lt;decimal&gt; extension method that converts.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">    The self to act on.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>An IObservable&lt;string&gt;</returns>
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

        /// <summary>An IObservable&lt;string&gt; extension method that convert back.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">    The self to act on.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>The back converted.</returns>
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

        /// <summary>
        /// A ReactiveProperty&lt;string&gt; extension method that sets decimal validate notify error.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">      The self to act on.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="decimals">  The decimals.</param>
        /// <returns>A ReactiveProperty&lt;string&gt;</returns>
        public static ReactiveProperty<string> SetDecimalValidateNotifyError(this ReactiveProperty<string> self, decimal lowerBound, decimal upperBound, int decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.SetValidateNotifyError(s => s.ValidateDecimal(lowerBound, upperBound, decimals));
        }

        /// <summary>
        /// A ReactiveProperty&lt;string&gt; extension method that sets decimal validate notify error.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">      The self to act on.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="decimals">  The decimals.</param>
        /// <returns>A ReactiveProperty&lt;string&gt;</returns>
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

        /// <summary>
        /// A ReactiveProperty&lt;string&gt; extension method that sets decimal validate notify error.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">      The self to act on.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="decimals">  The decimals.</param>
        /// <returns>A ReactiveProperty&lt;string&gt;</returns>
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

        /// <summary>An IObservable&lt;int&gt; extension method that subscribe and reformat.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">    The self to act on.</param>
        /// <param name="source">  Source for the.</param>
        /// <param name="target">  Target for the.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>An IDisposable.</returns>
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

        /// <summary>An IObservable&lt;int&gt; extension method that subscribe and reformat.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">    The self to act on.</param>
        /// <param name="source">  Source for the.</param>
        /// <param name="target">  Target for the.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>An IDisposable.</returns>
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

        /// <summary>
        /// An IObservable&lt;int&gt; extension method that subscribe and reformat.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">  The self to act on.</param>
        /// <param name="source">Source for the.</param>
        /// <param name="target">Target for the.</param>
        /// <returns>An IDisposable.</returns>
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
