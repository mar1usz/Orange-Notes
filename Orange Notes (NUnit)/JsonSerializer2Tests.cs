using NUnit.Framework;
using Orange_Notes.Model;
using System.IO;

namespace Orange_Notes__NUnit_
{
    public class JsonSerializer2Tests
    {
        int x1 = 7;
        string filepath = "test1.json";

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void SerializeTest()
        {
            JsonSerializer2<int>.Serialize(x1, filepath);
            Assert.IsTrue(new FileInfo(filepath).Length > 0);
        }

        [Test, Order(2)]
        public void DeserializeTest()
        {
            int x2;
            x2 = JsonSerializer2<int>.Deserialize(filepath);
            Assert.AreEqual(x1, x2);
        }

    }
}