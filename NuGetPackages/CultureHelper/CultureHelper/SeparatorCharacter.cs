// <copyright file="SeparatorCharacter.cs" company="tmori3y2">
// Copyright (c) 2016 tmori3y2. All rights reserved.
// </copyright>
// <author>tmori3y2</author>
// <date>2016/08/04</date>
// <summary>Implements the separator character class</summary>

namespace CultureHelper
{
    using System;

    /// <summary>Values that represent separator characters.</summary>
    /// <remarks>tmori3y2, 2016/08/04.</remarks>
    [CLSCompliant(true)]
    public enum SeparatorCharacter
    {
        /// <summary>An enum constant representing the unknown option.</summary>
        Unknown = 0x000,

        /// <summary>An enum constant representing the apostrophe option.</summary>
        Apostrophe = 0x0027,

        /// <summary>An enum constant representing the comma option.</summary>
        Comma = 0x002C,

        /// <summary>An enum constant representing the hyphen minus option.</summary>
        HyphenMinus = 0x002D,

        /// <summary>An enum constant representing the full stop option.</summary>
        FullStop = 0x002E,

        /// <summary>An enum constant representing the solidus option.</summary>
        Solidus = 0x002F,

        /// <summary>An enum constant representing the no break space option.</summary>
        NoBreakSpace = 0x00A0,

        /// <summary>An enum constant representing the right single quotation option.</summary>
        RightSingleQuotation = 0x2019
    }
}
