<Query Kind="Program">
  <Output>DataGrids</Output>
  <Namespace>System.Globalization</Namespace>
</Query>

// GetCultures.linq
// Language: C# Program
// [Query]>[Namespace Imports]: System.Globalization

void Main()
{
	// Dumps all culture informations.
	var cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
	cultures.Dump();
	
	var invariant = CultureInfo.InvariantCulture;
	var cultures2 = cultures
		.Select(culture => new {
							Lcid = culture.LCID,
							Name = culture.Name,
							EnglishName = culture.EnglishName,
							IsoLanguageName = culture.TwoLetterISOLanguageName,
							NumberDecimalSeparator = culture.NumberFormat.NumberDecimalSeparator,
							NumberDecimalSeparatorCode = (int)(culture.NumberFormat.NumberDecimalSeparator)[0],
							NumberGroupSeparator = culture.NumberFormat.NumberGroupSeparator,
							NumberGroupSeparatorCode = (int)(culture.NumberFormat.NumberGroupSeparator)[0]
						})
		.Where(item =>	item.NumberDecimalSeparator != invariant.NumberFormat.NumberDecimalSeparator ||
						item.NumberGroupSeparator != invariant.NumberFormat.NumberGroupSeparator);
	cultures2.Dump();
	
	var cultures3 = cultures2
		.Where(item =>	item.NumberDecimalSeparator != invariant.NumberFormat.NumberGroupSeparator ||
						item.NumberGroupSeparator != invariant.NumberFormat.NumberDecimalSeparator);
	cultures3.Dump();
	
	var cultures4 = cultures3
		.Where(item =>	item.NumberDecimalSeparator != invariant.NumberFormat.NumberDecimalSeparator &&
						item.NumberDecimalSeparator != invariant.NumberFormat.NumberGroupSeparator);
	cultures4.Dump();
	
	var cultures5 = cultures3
		.Where(item =>	item.NumberGroupSeparatorCode == 160); // U+0x00A0: No-Break Space
	cultures5.Dump();
	
	var cultures6 = cultures3
		.Where(item =>	item.NumberGroupSeparatorCode == 39); // U+0x0027: Aostrophe
	cultures6.Dump();
	
	var cultures7 = cultures3
		.Where(item =>	item.NumberGroupSeparatorCode == 8217); // U+0x2019: Right Single Quotation Mark
	cultures7.Dump();
}

// Define other methods and classes here
