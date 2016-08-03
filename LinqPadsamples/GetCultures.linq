<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference Relative="CultureHelper.dll">CultureHelper.dll</Reference>
  <Namespace>CultureHelper</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
    // Dumps all culture informations.
    var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
        .Where(culture => !culture.IsNeutralCulture);
    cultures.Dump();
    
    var americanStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsAmericanStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    americanStyleCultures.Dump();
    
    var germanStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsGermanStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    germanStyleCultures.Dump();
        
    var frenchSiStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsFrenchSIStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    frenchSiStyleCultures.Dump();
        
    var englishSiStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsEnglishSIStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    englishSiStyleCultures.Dump();
    
    var arabicStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsArabicStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    arabicStyleCultures.Dump();
    
    var swissStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsSwissStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    swissStyleCultures.Dump();
    
    var persianStyleCultures = cultures
        .Where(culture => culture.NumberFormat.IsPersianStyle())
        .Select(culture => new NumberFormatInfoSummary(culture));
    persianStyleCultures.Dump();
}

// Define other methods and classes here