using NUnit.Framework;
using Orange_Notes.ViewModel;
using System;

namespace Orange_Notes__NUnit_
{
    public class NoteViewModelTests
    {
        NoteViewModel vm1, vm2;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTest()
        {
            vm1 = new NoteViewModel();
            vm2 = new NoteViewModel();
            Assert.AreEqual(NoteViewModel.noteIds.Count, 2);
        }

        [Test]
        public void RemoveTest()
        {
            vm1.removeNote.Execute(vm2.noteId);
            Assert.AreEqual(NoteViewModel.noteIds.Count, 1);
        }

        [Test]
        public void GettersSettersTests()
        {
            vm1.noteTitle = "#TestNote1";
            vm1.noteContent = "Test content 1";

            Assert.AreEqual(vm1.noteTitle, "#TestNote1");
            Assert.AreEqual(vm1.noteContent, "Test content 1");
        }


    }
}
