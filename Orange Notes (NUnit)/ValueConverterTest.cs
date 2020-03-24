using NUnit.Framework;
using Orange_Notes.ViewModel;
using System.Collections.Generic;

namespace Orange_Notes__NUnit_
{
    public class ValueConverterTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("", "● ")]
        [TestCase("a", "● a")]
        [TestCaseSource("ConvertTestData")]
        public void ConvertTest(string in1, string out1)
        {
            ValueConverter c = new ValueConverter();
            string out2 = c.Convert(in1, typeof(string), null, null) as string;

            Assert.AreEqual(out1, out2);
        }

        public static IEnumerable<TestCaseData> ConvertTestData()
        {
            string n = System.Environment.NewLine;


            yield return new TestCaseData("b" + n, "● b" + n + "● ");
            yield return new TestCaseData("c" + n + "d", "● c" + n + "● d");
            yield return new TestCaseData(n + n + "e", "● " + n + "● " + n + "● e");
            yield return new TestCaseData(n + "f" + n + "g", "● " + n + "● f" + n + "● g");
        }

        [TestCase("● ", "")]
        [TestCase("● a", "a")]
        [TestCaseSource("ConvertBackTestData")]
        public void ConvertBackTest(string in1, string out1)
        {
            ValueConverter c = new ValueConverter();
            string out2 = c.ConvertBack(in1, typeof(string), null, null) as string;

            Assert.AreEqual(out1, out2);
        }

        public static IEnumerable<TestCaseData> ConvertBackTestData()
        {
            string n = System.Environment.NewLine;


            yield return new TestCaseData("● b" + n + "● ", "b" + n);
            yield return new TestCaseData("● c" + n + "● d", "c" + n + "d");
            yield return new TestCaseData("● " + n + "● " + n + "● e", n + n + "e");
            yield return new TestCaseData("● " + n + "● f" + n + "● g", n + "f" + n + "g");
        }
    }
}