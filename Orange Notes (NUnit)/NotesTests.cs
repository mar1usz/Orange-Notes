using NUnit.Framework;
using Orange_Notes.Model;

namespace Orange_Notes__NUnit_
{
    public class NotesTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTest()
        {
            Notes n = new Notes();
            n.AddNote();
            n.AddNote();
            Assert.AreEqual(n.GetNoteIds().Count, 2);
            Assert.AreEqual(n.GetNoteIds()[1], "2");
        }

        [Test]
        public void RemoveTest()
        {
            Notes n = new Notes();
            n.AddNote();
            Assert.AreEqual(n.GetNoteIds().Count, 1);

            n.RemoveNote(n.GetNoteIds()[0]);
            Assert.AreEqual(n.GetNoteIds().Count, 0);
        }

        [Test]
        public void GettersSettersTests()
        {
            Notes n = new Notes();
            n.AddNote();
            string noteId = n.GetNoteIds()[0];

            Assert.IsTrue(n.SetNoteTitle(noteId, "#TestNote1"));
            Assert.IsTrue(n.SetNoteContent(noteId, "Test note content 1"));

            Assert.AreEqual(n.GetNoteTitle(noteId), "#TestNote1");
            Assert.AreEqual(n.GetNoteContent(noteId), "Test note content 1");


            Assert.IsFalse(n.SetNoteTitle("27", "#FakeNoteTitle"));
            Assert.IsFalse(n.SetNoteContent("27", "Fake note content"));

            Assert.IsNull(n.GetNoteTitle("27"));
            Assert.IsNull(n.GetNoteContent("27"));
        }
    }
}
