// <copyright file="SeparatorCharacterExtensions.cs" company="tmori3y2">
// Copyright (c) 2016 tmori3y2. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/08/04</date>
// <summary>Implements the separator character extensions class</summary>
namespace CultureHelper
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>A separator character extensions.</summary>
    /// <remarks>tmori3y2, 2016/08/04.</remarks>
    public static class SeparatorCharacterExtensions
    {
        /// <summary>A string extension method that gets display string.</summary>
        /// <remarks>tmori3y2, 2016/08/04.</remarks>
        /// <param name="character">The character to act on.</param>
        /// <returns>The display string.</returns>
        public static string GetDisplayString(this string character)
        {
            if (string.IsNullOrEmpty(character))
            {
                throw new ArgumentException("Null or empty string is invalid.");
            }

            int code = character[0];
            SeparatorCharacter enumValue = SeparatorCharacter.Unknown;
            if (Enum.IsDefined(typeof(SeparatorCharacter), code))
            {
                enumValue = (SeparatorCharacter)Enum.ToObject(typeof(SeparatorCharacter), code);
            }

            return string.Format(CultureInfo.InvariantCulture, "{0:X4}:{1}", code, enumValue);
        }
    }
}
