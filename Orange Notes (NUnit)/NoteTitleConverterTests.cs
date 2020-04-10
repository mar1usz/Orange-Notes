using NUnit.Framework;
using Orange_Notes.ViewModel;
using System.Windows.Data;

namespace Orange_Notes__NUnit_
{
    public class NoteTitleConverterTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCase("","")]
        [TestCase("Notka1", "#Notka1")]
        public void ConvertTest(string in1, string out1)
        {
            IValueConverter c = new NoteTitleConverter();
            string out2 = c.Convert(in1, typeof(string), null, null) as string;

            Assert.AreEqual(out1, out2);
        }

        [TestCase("", "")]
        [TestCase("#Notka2", "Notka2")]
        public void ConvertBackTest(string in1, string out1)
        {
            IValueConverter c = new NoteTitleConverter();
            string out2 = c.ConvertBack(in1, typeof(string), null, null) as string;

            Assert.AreEqual(out1, out2);
        }


    }
}