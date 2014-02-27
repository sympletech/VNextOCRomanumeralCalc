using System;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RomanNumeralzTranslator
{
    [UseReporter(typeof(DiffReporter))]

    [TestClass]
    public class RomanToNumericTest
    {
        
        [TestMethod]
        public void RomanToDecimalTest()
        {
            string[] romanDigits = new[]
            {
                "I", "II", "III", "IV", "V", "VI", "VII", "VIII","IX","X",
                "XI", "XII", "XIII", "XIV", "XV", "XVI", "XVII", "XVIII","XIX","XX",
                "XXX","XXXIX","", "XL", "XLIX", "L", "l", "XC", "C",
                "XLVIII"
                
            };
            var results = romanDigits.Select(x => new {key = x, value = RomanNumeralTranslator.Translate(x)});
            ApprovalTests.Approvals.VerifyAll(results, "");
        }

        [TestMethod]
        public void RomanToDecimalExceptionTest()
        {
            var ex = ExceptionUtilities.GetException(() => RomanNumeralTranslator.Translate("Z"));
            Approvals.Verify(ex);
        }

        [TestMethod]
        public void BadDigitsTest()
        {
            string[] badDigits = {"IIX","IIZ","MIM", "MOM", "IVI", "HIV","XXVX", "VV","LL","LLCOOLJ"};
            var ex =
                badDigits.Select(
                    x =>
                        new
                        {
                            Key = x,
                            Value = ExceptionUtilities.GetException(() => RomanNumeralTranslator.Translate(x))
                        });
            Approvals.VerifyAll(ex,"");
        }
    }
}