// <copyright file="MyReactiveExtensions.cs" company="tmori3y2.hatenablog.com">
// Copyright (c) 2016 tmori3y2.hatenablog.com. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/06/22</date>
// <summary>Implements my reactive extensions class</summary>
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
    /// <summary>my reactive extensions.</summary>
    /// <remarks>tmori3y2, 2016/06/22.</remarks>
    [CLSCompliant(true)]
    public static class MyReactiveExtensions
    {
        /// <summary>An IObservable&lt;int&gt; extension method that converts the given self.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">    The self to act on.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>An IObservable&lt;string&gt;</returns>
        public static IObservable<string> Convert(this IObservable<decimal> self, int decimals)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.Select(d => d.Convert(decimals));
        }

        /// <summary>
        /// An IObservable&lt;int&gt; extension method that converts the given self.
        /// </summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <param name="self">The self to act on.</param>
        /// <returns>An IObservable&lt;string&gt;</returns>
        public static IObservable<string> Convert(this IObservable<int> self)
        {
            if (self == null)
            {
                throw new ArgumentNullException("self");
            }

            return self.Select(i => i.Convert());
        }

        /// <summary>An IObservable&lt;string&gt; extension method that convert back.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="self">    The self to act on.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>The back converted.</returns>
        public static IObservable<decimal> ConvertBack(this IObservable<string> self, int decimals)
        {
            return self.Select(s => s.ConvertBack(decimals));
        }

        /// <summary>An IObservable&lt;string&gt; extension method that convert back.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="self">The self to act on.</param>
        /// <returns>The back converted.</returns>
        public static IObservable<int> ConvertBack(this IObservable<string> self)
        {
            return self.Select(s => s.ConvertBack());
        }

        /// <summary>An IObservable&lt;T&gt; extension method that changed as observable.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="self">The self to act on.</param>
        /// <returns>An IObservable&lt;bool&gt;</returns>
        public static IObservable<bool> ChangedAsObservable<T>(this IObservable<T> self)
        {
            return self.ChangedAsObservable(true);
        }

        /// <summary>An IObservable&lt;T&gt; extension method that changed as observable.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown when one or more required arguments are null.
        /// </exception>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="self">     The self to act on.</param>
        /// <param name="skipFirst">true to skip first.</param>
        /// <returns>An IObservable&lt;bool&gt;</returns>
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
