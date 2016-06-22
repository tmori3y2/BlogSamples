// <copyright file="MyDecimalExtensions.cs" company="tmori3y2.hatenablog.com">
// Copyright (c) 2016 tmori3y2.hatenablog.com. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/06/22</date>
// <summary>Implements my decimal extensions class</summary>
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Linq;

using Reactive.Bindings;
using Reactive.Bindings.Extensions;

using MySampleExtensions.Properties;


namespace MySampleExtensions
{
    /// <summary>my decimal extensions.</summary>
    /// <remarks>tmori3y2, 2016/06/22.</remarks>
    [CLSCompliant(true)]
    public static class MyDecimalExtensions
    {
        /// <summary>A decimal extension method that converts.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="source">         The source to act on.</param>
        /// <param name="decimals">       The decimals.</param>
        /// <param name="allowsThousands">true to allows thousands.</param>
        /// <param name="provider">       The provider.</param>
        /// <returns>A string.</returns>
        public static string Convert(this decimal source, int decimals, bool allowsThousands, IFormatProvider provider)
        {
            string format = (decimals >= 0) ?
                string.Format(CultureInfo.InvariantCulture, allowsThousands ? "N{0}" : "F{0}", decimals) :
                allowsThousands ? "N" : "F";
            provider = provider ?? CultureInfo.CurrentUICulture;

            return source.ToString(format, provider);
        }

        /// <summary>A string extension method that convert back.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="FormatException">Thrown when the format of the ? is incorrect.</exception>
        /// <param name="input">          The input to act on.</param>
        /// <param name="decimals">       The decimals.</param>
        /// <param name="allowsThousands">true to allows thousands.</param>
        /// <param name="awayFromZero">   true to away from zero.</param>
        /// <param name="provider">       The provider.</param>
        /// <returns>The back converted.</returns>
        public static decimal ConvertBack(this string input, int decimals, bool allowsThousands, bool awayFromZero, IFormatProvider provider)
        {
            NumberStyles style = NumberStyles.Number & ~NumberStyles.AllowTrailingSign;
            if (!allowsThousands)
            {
                style &= ~NumberStyles.AllowThousands;
            } 
            
            decimal value = decimal.Zero;
            provider = provider ?? CultureInfo.CurrentUICulture;

            if (!decimal.TryParse(input, style, provider, out value))
            {
                string message = string.Format(provider, Resources.InputDecimalString);
                throw new FormatException(message);
            }
            return (decimals < 0) ? value : Math.Round(value, decimals, awayFromZero ? MidpointRounding.AwayFromZero : MidpointRounding.ToEven);
        }

        /// <summary>A string extension method that validates the decimal.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <exception cref="ArgumentException">
        /// Thrown when one or more arguments have unsupported or illegal values.
        /// </exception>
        /// <param name="input">          The input to act on.</param>
        /// <param name="lowerBound">     The lower bound.</param>
        /// <param name="upperBound">     The upper bound.</param>
        /// <param name="decimals">       The decimals.</param>
        /// <param name="allowsThousands">true to allows thousands.</param>
        /// <param name="checksDecimals"> true to checks decimals.</param>
        /// <param name="provider">       The provider.</param>
        /// <returns>A string.</returns>
        public static string ValidateDecimal(this string input, decimal lowerBound, decimal upperBound, int decimals, bool allowsThousands, bool checksDecimals, IFormatProvider provider)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentException("upperBound < lowerBound");
            }

            provider = provider ?? CultureInfo.CurrentUICulture;
            string format = (decimals >= 0) ?
                string.Format(CultureInfo.InvariantCulture, allowsThousands ? "N{0}" : "F{0}", decimals) :
                allowsThousands ? "N" : "F";

            string lowerBoundString = lowerBound.ToString(format, provider);
            string upperBoundString = upperBound.ToString(format, provider);
            decimal step = (decimals >= 0) ? Math.Round((decimal)Math.Pow(0.1, decimals), decimals, MidpointRounding.AwayFromZero) : 0;
            string stepString = step.ToString(format, provider);
            string message;
            if (checksDecimals && (decimals >= 0))
            {
                message = string.Format(provider, Resources.InputDecimalRangeWithStepString, lowerBoundString, upperBoundString, stepString);
            }
            else
            {
                message = string.Format(provider, Resources.InputDecimalRangeString, lowerBoundString, upperBoundString);
            }

            NumberStyles style = NumberStyles.Number & ~NumberStyles.AllowTrailingSign;
            if (!allowsThousands)
            {
                style &= ~NumberStyles.AllowThousands;
            }

            decimal value = decimal.Zero;
            if (!decimal.TryParse(input, style, provider, out value))
            {
                return message;
            }
            else if (value < lowerBound || upperBound < value)
            {
                return message;
            }
            else if (checksDecimals && (decimals >= 0))
            {
                decimal remainer = decimal.Remainder(value, step);
                if (remainer != decimal.Zero)
                {
                    return message;
                }
            }
            return null;
        }

        /// <summary>A decimal extension method that converts.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="source">  The source to act on.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>A string.</returns>
        public static string Convert(this decimal source, int decimals)
        {
            return source.Convert(decimals, true, null);
        }

        /// <summary>A decimal extension method that converts.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="source">The source to act on.</param>
        /// <returns>A string.</returns>
        public static string Convert(this int source)
        {
            return ((decimal)source).Convert(0, true, null);
        }

        /// <summary>A string extension method that convert back.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="input">   The input to act on.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>The back converted.</returns>
        public static decimal ConvertBack(this string input, int decimals)
        {
            return input.ConvertBack(decimals, true, true, null);
        }

        /// <summary>A string extension method that convert back.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="input">The input to act on.</param>
        /// <returns>The back converted.</returns>
        public static int ConvertBack(this string input)
        {
            return (int)input.ConvertBack(0, true, true, null);
        }

        /// <summary>A string extension method that validates the decimal.</summary>
        /// <remarks>tmori3y2, 2016/06/22.</remarks>
        /// <param name="input">     The input to act on.</param>
        /// <param name="lowerBound">The lower bound.</param>
        /// <param name="upperBound">The upper bound.</param>
        /// <param name="decimals">  The decimals.</param>
        /// <returns>A string.</returns>
        public static string ValidateDecimal(this string input, decimal lowerBound, decimal upperBound, int decimals)
        {
            return input.ValidateDecimal(lowerBound, upperBound, decimals, true, true, null);
        }
    }
}
