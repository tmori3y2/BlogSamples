using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reactive.Linq;

namespace MyReactivePropertyControls.Extensions
{
    public static class DecimalPropertyExtensions
    {
        public static string Convert(this decimal source, int decimals, bool allowsThousands, IFormatProvider provider)
        {
            string format = (decimals >= 0) ?
                string.Format(CultureInfo.InstalledUICulture, allowsThousands ? "N{0}" : "F{0}", decimals) :
                allowsThousands ? "N" : "F";
            provider = provider ?? CultureInfo.CurrentUICulture;

            return source.ToString(format, provider);
        }

        public static decimal ConvertBack(this string input, int decimals, bool allowsThousands, bool awayFromZero, IFormatProvider provider)
        {
            NumberStyles style = NumberStyles.Number & ~NumberStyles.AllowTrailingSign;
            decimal value = 0m;
            provider = provider ?? CultureInfo.CurrentUICulture;

            if (!decimal.TryParse(input, style, provider, out value))
            {
                throw new FormatException("Input the decimal.");
            }
            return (decimals < 0) ? value : Math.Round(value, decimals, awayFromZero ? MidpointRounding.AwayFromZero : MidpointRounding.ToEven);
        }

        public static string ValidateDecimal(this string input, decimal lowerBound, decimal upperBound, int decimals, bool allowsThousands, bool checksDecimals, IFormatProvider provider)
        {
            if (upperBound < lowerBound)
            {
                throw new ArgumentException("upperBound < lowerBound");
            }

            provider = provider ?? CultureInfo.CurrentUICulture;
            string format = (decimals >= 0) ?
                string.Format(CultureInfo.InstalledUICulture, allowsThousands ? "N{0}" : "F{0}", decimals) :
                allowsThousands ? "N" : "F";

            string lowerBoundString = lowerBound.ToString(format, provider);
            string upperBoundString = upperBound.ToString(format, provider);
            decimal step = (decimals >= 0) ? Math.Round((decimal)Math.Pow(0.1, decimals), decimals, MidpointRounding.AwayFromZero) : 0;
            string stepString = step.ToString(format, provider);
            string message;
            if (checksDecimals && (decimals >= 0))
            {
                message = string.Format(provider, "Input the decimal from {0} to {1}, step {2}.", lowerBoundString, upperBoundString, stepString);
            }
            else
            {
                message = string.Format(provider, "Input the decimal from {0} to {1}.", lowerBoundString, upperBoundString);
            }

            NumberStyles style = NumberStyles.Number & ~NumberStyles.AllowTrailingSign;
            if (!allowsThousands)
            {
                style &= ~NumberStyles.AllowThousands;
            }

            decimal value = 0m;
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
                if (remainer != 0)
                {
                    return message;
                }
            }
            return null;
        }

        public static string Convert(this decimal source, int decimals)
        {
            return source.Convert(decimals, true, null);
        }

        public static string Convert(this int source)
        {
            return ((decimal)source).Convert(0, true, null);
        }

        public static decimal ConvertBack(this string input, int decimals)
        {
            return input.ConvertBack(decimals, true, true, null);
        }

        public static int ConvertBack(this string input)
        {
            return (int)input.ConvertBack(0, true, true, null);
        }

        public static string ValidateDecimal(this string input, decimal lowerBound, decimal upperBound, int decimals)
        {
            return input.ValidateDecimal(lowerBound, upperBound, decimals, true, true, null);
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
