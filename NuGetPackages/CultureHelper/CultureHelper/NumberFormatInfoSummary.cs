// <copyright file="NumberFormatInfoSummary.cs" company="tmori3y2">
// Copyright (c) 2016 tmori3y2. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/08/04</date>
// <summary>Implements the number format information summary class</summary>
namespace CultureHelper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>A number format information summary.</summary>
    /// <remarks>tmori3y2, 2016/08/04.</remarks>
    public class NumberFormatInfoSummary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFormatInfoSummary"/> class.
        /// </summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        public NumberFormatInfoSummary()
            : this(CultureInfo.InvariantCulture)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberFormatInfoSummary"/> class.
        /// </summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="culture">The culture.</param>
        public NumberFormatInfoSummary(CultureInfo culture)
        {
            culture = culture ?? CultureInfo.InvariantCulture;
            var numberFormat = culture.NumberFormat;
            Name = culture.Name;
            EnglishName = culture.EnglishName;
            DecimalDigits = numberFormat.NumberDecimalDigits;
            DecimalSeparator = numberFormat.NumberDecimalSeparator.GetDisplayString();
            GroupSeparator = numberFormat.NumberGroupSeparator.GetDisplayString();
            NegativeSign = numberFormat.NegativeSign.GetDisplayString();
            NegativePattern = numberFormat.NumberNegativePattern;
            FloatingSample = string.Format(culture, "{0:F}", -1500.005m);
            NumberSample = string.Format(culture, "{0:N}", -1500.005m);
        }

        /// <summary>Gets the name.</summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>Gets the name of the english.</summary>
        /// <value>The name of the english.</value>
        public string EnglishName { get; }

        /// <summary>Gets the decimal digits.</summary>
        /// <value>The decimal digits.</value>
        public int DecimalDigits { get; }

        /// <summary>Gets the decimal separator.</summary>
        /// <value>The decimal separator.</value>
        public string DecimalSeparator { get; }

        /// <summary>Gets the group separator.</summary>
        /// <value>The group separator.</value>
        public string GroupSeparator { get; }

        /// <summary>Gets the negative sign.</summary>
        /// <value>The negative sign.</value>
        public string NegativeSign { get; }

        /// <summary>Gets the negative pattern.</summary>
        /// <value>The negative pattern.</value>
        public int NegativePattern { get; }

        /// <summary>Gets the floating sample.</summary>
        /// <value>The floating sample.</value>
        public string FloatingSample { get; }

        /// <summary>Gets the number of samples.</summary>
        /// <value>The total number of sample.</value>
        public string NumberSample { get; }
    }
}
