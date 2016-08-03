/// <Query Kind="Program">
///   <Output>DataGrids</Output>
///   <Namespace>System</Namespace>
///   <Namespace>System.Globalization</Namespace>
/// </Query>

using System;
using System.Globalization;

void Main()
{
    // Dumps all specific cultures and invariant culture informations.
    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
        .Where(culture => !culture.IsNeutralCulture);
    cultures.Dump();
    
    var americanStyleInfos = cultures
        .Where(culture => culture.IsAmericanStyle())
        .Select(culture => new NumberInfoSummary(culture));
    americanStyleInfos.Dump();
    
    var germanStyleInfos = cultures
        .Where(culture => culture.IsGermanStyle())
        .Select(culture => new NumberInfoSummary(culture));
    germanStyleInfos.Dump();
        
    var frenchSiStyleInfos = cultures
        .Where(culture => culture.IsFrenchSiStyle())
        .Select(culture => new NumberInfoSummary(culture));
    frenchSiStyleInfos.Dump();
        
    var englishSiStyleInfos = cultures
        .Where(culture => culture.IsEnglishSiStyle())
        .Select(culture => new NumberInfoSummary(culture));
    englishSiStyleInfos.Dump();
    
    var arabicStyleInfos = cultures
        .Where(culture => culture.IsArabicStyle())
        .Select(culture => new NumberInfoSummary(culture));
    arabicStyleInfos.Dump();
    
    var swissStyleInfos = cultures
        .Where(culture => culture.IsSwissStyle())
        .Select(culture => new NumberInfoSummary(culture));
    swissStyleInfos.Dump();
    
    var persianStyleInfos = cultures
        .Where(culture => culture.IsPersianStyle())
        .Select(culture => new NumberInfoSummary(culture));
    persianStyleInfos.Dump();
}

// Define other methods and classes here
public enum SeparatorCharacter
{
    Unknown = 0x000,
    Aphostophe = 0x0027, // 39, "'"
    Comma = 0x002C, // 44, ","
    HyphenMinus = 0x002D, // 45. "-"
    FullStop = 0x002E, // 46, "."
    Solidus = 0x002F, // 47, "/"
    NoBreakSpace = 0x00A0, // 160, " "
    RightSingleQuatation = 0x2019 // 8217, "’"
}

public static class SeparatorCharacterExtensions
{
    public static string GetDisplayString(this string charactor)
    {
        int code = (int)charactor[0];
        SeparatorCharacter enumValue = SeparatorCharacter.Unknown;
        if (Enum.IsDefined(typeof(SeparatorCharacter), code))
        {
            enumValue = (SeparatorCharacter)Enum.ToObject(typeof(SeparatorCharacter), code);
        }

        return string.Format(CultureInfo.InvariantCulture, "{0:X4}:{1}", code, enumValue);
    }
}

public class NumberInfoSummary
{
    public int LCID { get; }
    public string Name { get; }
    public string EnglishName { get; }

    // NumberFormatInfo Summary
    public int DecimalDigits { get; }
    public string DecimalSeparator { get; }
    public string GroupSeparator { get; }
    public string NegativeSign { get; }
    public int NegativePattern { get; }
    public string FloatingSample { get; }
    public string NumberSample { get; }

    public NumberInfoSummary()
        : this(CultureInfo.InvariantCulture)
    {
    }

    public NumberInfoSummary(CultureInfo culture)
    {
        LCID = culture.LCID;
        Name = culture.Name;
        EnglishName = culture.EnglishName;

        var number
        DecimalDigits = culture.NumberFormat.NumberDecimalDigits;
        DecimalSeparator = culture.NumberFormat.NumberDecimalSeparator.GetDisplayString();
        GroupSeparator = culture.NumberFormat.NumberGroupSeparator.GetDisplayString();
        NegativeSign = culture.NumberFormat.NegativeSign.GetDisplayString();
        NegativePattern = culture.NumberFormat.NumberNegativePattern;
        FloatingSample = string.Format(culture, "{0:F}", -1500.005m);
        NumberSample = string.Format(culture, "{0:N}", -1500.005m);
    }
}

public static class CultureInfoSummaryExtensions
{
	public const string Aphostrophe = "'";
	public const string Comma = ",";
	public const string HyphenMinus = "-";
	public const string FullStop = ".";
	public const string Solidus = "/";
	public const string NoBreakSpace = " ";
	public const string RightSingleQuatation = "’";

	public static bool IsAmericanStyle(this CultureInfo culture)
	{
		return (culture.NumberFormat.NumberDecimalSeparator == FullStop &&
				culture.NumberFormat.NumberGroupSeparator == Comma &&
				(culture.NumberFormat.NumberNegativePattern == 1 || culture.NumberFormat.NumberNegativePattern == 2));
	}

	public static bool IsGermanStyle(this CultureInfo culture)
	{
		return (culture.NumberFormat.NumberDecimalSeparator == Comma &&
				culture.NumberFormat.NumberGroupSeparator == FullStop &&
				(culture.NumberFormat.NumberNegativePattern == 1 || culture.NumberFormat.NumberNegativePattern == 2));
	}
    
    public static bool IsEnglishSiStyle(this CultureInfo culture)
    {
        return (culture.NumberFormat.NumberDecimalSeparator == FullStop &&
                culture.NumberFormat.NumberGroupSeparator == NoBreakSpace &&
                (culture.NumberFormat.NumberNegativePattern == 1 || culture.NumberFormat.NumberNegativePattern == 2));
    }
    
    public static bool IsFrenchSiStyle(this CultureInfo culture)
    {
        return (culture.NumberFormat.NumberDecimalSeparator == Comma &&
                culture.NumberFormat.NumberGroupSeparator == NoBreakSpace &&
                (culture.NumberFormat.NumberNegativePattern == 1 || culture.NumberFormat.NumberNegativePattern == 2));
    }
    
    public static bool IsArabicStyle(this CultureInfo culture)
    {
        return (culture.NumberFormat.NumberNegativePattern == 3 ||
				culture.NumberFormat.NumberNegativePattern == 4);
    }
    
    public static bool IsSwissStyle(this CultureInfo culture)
    {
        return (culture.NumberFormat.NumberGroupSeparator == Aphostrophe ||
                culture.NumberFormat.NumberGroupSeparator == RightSingleQuatation);
    }
    
    public static bool IsPersianStyle(this CultureInfo culture)
    {
        return (culture.NumberFormat.NumberDecimalSeparator == Solidus);
    }
}