<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

// GetCultures.linq
// Language: C# Program
// [Query]>[Namespace Imports]: System.Globalization

void Main()
{
	// Dumps all culture informations.
	var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
		.Where(culture => !culture.IsNeutralCulture);
	cultures.Dump();
	
	var invariant = CultureInfo.InvariantCulture;
	var culturesSummary = cultures
		.Select(culture => new CultureInfoSummary(culture));
	
	var americanStyleCultures = culturesSummary
		.Where(culture => culture.IsAmericanStyle);
	americanStyleCultures.Dump();
	
	var germanStyleCultures = culturesSummary
		.Where(culture => culture.IsGermanStyle);
	germanStyleCultures.Dump();
		
	var frenchSiStyleCultures = culturesSummary
		.Where(culture => culture.IsFrenchSiStyle); // U+0x00A0: No-Break Space
	frenchSiStyleCultures.Dump();
		
	var englishSiStyleCultures = culturesSummary
		.Where(culture => culture.IsEnglishSiStyle); // U+0x00A0: No-Break Space
	englishSiStyleCultures.Dump();
	
	var arabicStyleCultures = culturesSummary
		.Where(culture => culture.IsArabicStyle);
	arabicStyleCultures.Dump();
	
	var swissStyleCultures = culturesSummary
		.Where(culture => culture.IsSwissStyle); // U+0x0027: Aostrophe, U+0x2019: Right Single Quotation Mark
	swissStyleCultures.Dump();
	
	var persianStyleCultures = culturesSummary
		.Where(culture => culture.IsPersianStyle);
	persianStyleCultures.Dump();
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

public class CultureInfoSummary
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
	public bool IsAmericanStyle { get; }
	public bool IsGermanStyle { get; }
	public bool IsEnglishSiStyle { get; }
	public bool IsFrenchSiStyle { get; }
	public bool IsArabicStyle { get; }
	public bool IsSwissStyle { get; }
	public bool IsPersianStyle { get; }

	private string decimalSeparatorChar;
	private string groupSeparatorChar;
	private string negativeSignChar;

    public CultureInfoSummary()
        : this(CultureInfo.InvariantCulture)
    {
    }

    public CultureInfoSummary(CultureInfo parent)
    {
		decimalSeparatorChar = parent.NumberFormat.NumberDecimalSeparator;
		groupSeparatorChar = parent.NumberFormat.NumberGroupSeparator;
		negativeSignChar = parent.NumberFormat.NegativeSign;
		
        LCID = parent.LCID;
        Name = parent.Name;
        EnglishName = parent.EnglishName;
        DecimalDigits = parent.NumberFormat.NumberDecimalDigits;
        DecimalSeparator = decimalSeparatorChar.GetDisplayString();
        GroupSeparator = groupSeparatorChar.GetDisplayString();
		NegativeSign = negativeSignChar.GetDisplayString();
		NegativePattern = parent.NumberFormat.NumberNegativePattern;
		FloatingSample = string.Format(parent, "{0:F}", -1500.005m);
		NumberSample = string.Format(parent, "{0:N}", -1500.005m);
		IsAmericanStyle = IsAmericanStyleBody();
		IsGermanStyle = IsGermanStyleBody();
		IsEnglishSiStyle = IsEnglishSiStyleBody();
		IsFrenchSiStyle = IsFrenchSiStyleBody();
		IsArabicStyle = IsArabicStyleBody();
		IsSwissStyle = IsSwissStyleBody();
		IsPersianStyle = IsPersianStyleBody();
    }
	
	public bool IsAmericanStyleBody()
	{
		return (decimalSeparatorChar == CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator &&
				groupSeparatorChar == CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator &&
				NegativePattern == CultureInfo.InvariantCulture.NumberFormat.NumberNegativePattern);
	}
	
	public bool IsGermanStyleBody()
	{
		return (decimalSeparatorChar == CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator &&
				groupSeparatorChar == CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator &&
				NegativePattern == CultureInfo.InvariantCulture.NumberFormat.NumberNegativePattern);
	}
	
	public bool IsEnglishSiStyleBody()
	{
		return (decimalSeparatorChar == CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator &&
				groupSeparatorChar == " " &&
				NegativePattern == CultureInfo.InvariantCulture.NumberFormat.NumberNegativePattern);
	}
	
	public bool IsFrenchSiStyleBody()
	{
		return (decimalSeparatorChar == CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator &&
				groupSeparatorChar == " " &&
				NegativePattern == CultureInfo.InvariantCulture.NumberFormat.NumberNegativePattern);
	}
	
	public bool IsArabicStyleBody()
	{
		return (NegativePattern == 3);
	}
	
	public bool IsSwissStyleBody()
	{
		return (groupSeparatorChar == "'" ||
				groupSeparatorChar == "’");
	}
	
	public bool IsPersianStyleBody()
	{
		return (decimalSeparatorChar == "/");
	}
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