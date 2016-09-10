using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NumericTest
{
    public static class TestHelper
    {
        public static decimal ParseDecimal(this string input) => decimal.Parse(input, CultureInfo.InstalledUICulture);
        public static int GetActualDecimals(this decimal input) => (decimal.GetBits(input)[3] >> 16) & 0x00FF;
    }

    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestCaseSource("TestDecimalSorce")]
        public void TestDecimal()
        {
            //79228162514264337593543950336m.Is(79228162514264337593543950336m); Compile error
            AssertEx.Throws<OverflowException>(() => "79228162514264337593543950336".ParseDecimal());
            //79228162514264337593543950335.5m.Is(79228162514264337593543950336m); Compile error
            AssertEx.Throws<OverflowException>(() => "79228162514264337593543950335.5".ParseDecimal());

            TestContext.Run((decimal value, int decimals, decimal literal, string input) =>
            {
                literal.Is(value);
                input.ParseDecimal().Is(value);
                literal.GetActualDecimals().Is(decimals);
                input.ParseDecimal().GetActualDecimals().Is(decimals);
            });

            1.00m.ToString().Is("1.00");
            1.0m.ToString().Is("1.0");
            1m.ToString().Is("1");
            Math.Round(1.00m, 1, MidpointRounding.AwayFromZero).ToString().Is("1.0");
            Math.Round(1.0m, 1,  MidpointRounding.AwayFromZero).ToString().Is("1.0");
            Math.Round(1m, 1,    MidpointRounding.AwayFromZero).ToString().Is("1");
            Math.Round(1.00m, 1, MidpointRounding.AwayFromZero).GetActualDecimals().Is(1);
            Math.Round(1.0m,  1, MidpointRounding.AwayFromZero).GetActualDecimals().Is(1);
            Math.Round(1m,    1, MidpointRounding.AwayFromZero).GetActualDecimals().Is(0);
            decimal.GetBits(Math.Round(1.00m, 1, MidpointRounding.AwayFromZero)).Is(decimal.GetBits(1.0m), (x, y) => x == y);
            decimal.GetBits(Math.Round(1.0m,  1, MidpointRounding.AwayFromZero)).Is(decimal.GetBits(1.0m), (x, y) => x == y);
            decimal.GetBits(Math.Round(1m,    1, MidpointRounding.AwayFromZero)).Is(decimal.GetBits(1m),  (x, y) => x == y);
        }

        public static object[] TestDecimalSorce = new[]
        {
            new object[] { decimal.MaxValue,                 0, 79228162514264337593543950335m,       "79228162514264337593543950335"       },
            new object[] { 79228162514264337593543950335m,   0, 79228162514264337593543950335m,       "79228162514264337593543950335"       },
            new object[] { 79228162514264337593543950335m,   0, 79228162514264337593543950335.4m,     "79228162514264337593543950335.4"     },
            new object[] { 10000000000000000000000000002m,   0, 10000000000000000000000000001.5m,     "10000000000000000000000000001.5"     },
            new object[] { 10000000000000000000000000001m,   0, 10000000000000000000000000001.4m,     "10000000000000000000000000001.4"     },
            new object[] { 10000000000000000000000000001m,   0, 10000000000000000000000000000.6m,     "10000000000000000000000000000.6"     },
            new object[] { 10000000000000000000000000000m,   0, 10000000000000000000000000000.5m,     "10000000000000000000000000000.5"     },
            new object[] { 1000000000000000000000000000.2m,  1, 1000000000000000000000000000.15m,     "1000000000000000000000000000.15"     },
            new object[] { 1000000000000000000000000000.1m,  1, 1000000000000000000000000000.14m,     "1000000000000000000000000000.14"     },
            new object[] { 1000000000000000000000000000.1m,  1, 1000000000000000000000000000.06m,     "1000000000000000000000000000.06"     },
            new object[] { 1000000000000000000000000000.0m,  1, 1000000000000000000000000000.05m,     "1000000000000000000000000000.05"     },
            new object[] { 1000000000000000000000000000m,    0, 1000000000000000000000000000m,        "1000000000000000000000000000"        },
            new object[] { 100000000000000.00000000000002m, 14, 100000000000000.000000000000015m,     "100000000000000.000000000000015"     },
            new object[] { 100000000000000.00000000000001m, 14, 100000000000000.000000000000014m,     "100000000000000.000000000000014"     },
            new object[] { 100000000000000.00000000000001m, 14, 100000000000000.000000000000006m,     "100000000000000.000000000000006"     },
            new object[] { 100000000000000.00000000000000m, 14, 100000000000000.000000000000005m,     "100000000000000.000000000000005"     },
            new object[] { 100000000000000m,                 7, 100000000000000.0000000m,             "100000000000000.0000000"             },
            new object[] { 1.0000000000000000000000000001m, 28, 1.00000000000000000000000000014m,     "1.00000000000000000000000000014"     },
            new object[] { 1.0000000000000000000000000002m, 28, 1.00000000000000000000000000015m,     "1.00000000000000000000000000015"     },
            new object[] { 1.0000000000000000000000000001m, 28, 1.00000000000000000000000000006m,     "1.00000000000000000000000000006"     },
            new object[] { 1.0000000000000000000000000000m, 28, 1.00000000000000000000000000005m,     "1.00000000000000000000000000005"     },
            new object[] { 1m,                              14, 1.00000000000000m,                    "1.00000000000000"                    },
            new object[] { 0.0000000000000000000000000000m, 28, 0.000000000000000000000000000000123m, "0.000000000000000000000000000000123" },
            new object[] { 7.9228162514264337593543950335m, 28, 7.9228162514264337593543950335m,      "7.9228162514264337593543950335"     },
            new object[] { 7.922816251426433759354395034m,  27, 7.9228162514264337593543950336m,      "7.9228162514264337593543950336"     },
        };
    }
}
