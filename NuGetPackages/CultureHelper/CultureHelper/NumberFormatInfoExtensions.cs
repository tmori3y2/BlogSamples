// <copyright file="NumberFormatInfoExtensions.cs" company="tmori3y2">
// Copyright (c) 2016 tmori3y2. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/08/04</date>
// <summary>Implements the number format information extensions class</summary>
namespace CultureHelper
{
    using System.Globalization;

    /// <summary>A number format information extensions.</summary>
    /// <remarks>tmori3y2, 2016/08/04.</remarks>
    public static class NumberFormatInfoExtensions
    {
        /// <summary>The apostrophe.</summary>
        public const string Apostrophe = "'";

        /// <summary>The comma.</summary>
        public const string Comma = ",";

        /// <summary>The hyphen minus.</summary>
        public const string HyphenMinus = "-";

        /// <summary>The full stop.</summary>
        public const string FullStop = ".";

        /// <summary>The solidus.</summary>
        public const string Solidus = "/";

        /// <summary>The no break space.</summary>
        public const string NoBreakSpace = " ";

        /// <summary>The right single quotation.</summary>
        public const string RightSingleQuotation = "’";

        /// <summary>A NumberFormatInfo extension method that query if 'culture' is american style.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if american style, false if not.</returns>
        public static bool IsAmericanStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ?
               (numberFormat.NumberDecimalSeparator == FullStop &&
                numberFormat.NumberGroupSeparator == Comma &&
               (numberFormat.NumberNegativePattern == 1 ||
                numberFormat.NumberNegativePattern == 2)) : false;

        /// <summary>A NumberFormatInfo extension method that query if 'culture' is german style.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if german style, false if not.</returns>
        public static bool IsGermanStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ?
               (numberFormat.NumberDecimalSeparator == Comma &&
                numberFormat.NumberGroupSeparator == FullStop &&
               (numberFormat.NumberNegativePattern == 1 ||
                numberFormat.NumberNegativePattern == 2)) : false;

        /// <summary>
        /// A NumberFormatInfo extension method that query if 'culture' is english SI style.
        /// </summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if english SI style, false if not.</returns>
        public static bool IsEnglishSIStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ?
               (numberFormat.NumberDecimalSeparator == FullStop &&
                numberFormat.NumberGroupSeparator == NoBreakSpace &&
               (numberFormat.NumberNegativePattern == 1 ||
                numberFormat.NumberNegativePattern == 2)) : false;

        /// <summary>A NumberFormatInfo extension method that query if 'culture' is french SI style.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if french SI style, false if not.</returns>
        public static bool IsFrenchSIStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ?
               (numberFormat.NumberDecimalSeparator == Comma &&
                numberFormat.NumberGroupSeparator == NoBreakSpace &&
               (numberFormat.NumberNegativePattern == 1 ||
                numberFormat.NumberNegativePattern == 2)) : false;

        /// <summary>A NumberFormatInfo extension method that query if 'culture' is arabic style.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if arabic style, false if not.</returns>
        public static bool IsArabicStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ?
               (numberFormat.NumberNegativePattern == 3 ||
                numberFormat.NumberNegativePattern == 4) : false;

        /// <summary>A NumberFormatInfo extension method that query if 'culture' is swiss style.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if swiss style, false if not.</returns>
        public static bool IsSwissStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ?
               (numberFormat.NumberGroupSeparator == Apostrophe ||
                numberFormat.NumberGroupSeparator == RightSingleQuotation) : false;

        /// <summary>A NumberFormatInfo extension method that query if 'culture' is persian style.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="numberFormat">The numberFormat to act on.</param>
        /// <returns>true if persian style, false if not.</returns>
        public static bool IsPersianStyle(this NumberFormatInfo numberFormat)
            => (numberFormat != null) ? (numberFormat.NumberDecimalSeparator == Solidus) : false;
    }
}
