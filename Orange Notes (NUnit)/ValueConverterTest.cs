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

        [TestCase("","")]
        [TestCase("Notka1", "#Notka1")]
        public void ConvertTest(string in1, string out1)
        {
            ValueConverter c = new ValueConverter();
            string out2 = c.Convert(in1, typeof(string), null, null) as string;

            Assert.AreEqual(out1, out2);
        }

        [TestCase("", "")]
        [TestCase("#Notka1", "Notka1")]
        public void ConvertBackTest(string in1, string out1)
        {
            ValueConverter c = new ValueConverter();
            string out2 = c.ConvertBack(in1, typeof(string), null, null) as string;

            Assert.AreEqual(out1, out2);
        }


    }
}