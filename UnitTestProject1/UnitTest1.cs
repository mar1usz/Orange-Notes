using Microsoft.VisualStudio.TestTools.UnitTesting;
using Orange_Notes.ViewModel;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Notes n = new Notes();
            // n.AddNote();
            //Console.WriteLine(n.GetNoteTitle(1));

            NoteViewModel n = new NoteViewModel();
            n.noteTitle = "bujaka";
            Console.WriteLine(n.noteTitle);
        }
    }
}
