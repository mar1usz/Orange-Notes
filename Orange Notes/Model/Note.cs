namespace Orange_Notes.Model
{
    public class Note
    {
        public string content { get; set; }

        public Note(string content)
        {
            this.content = content;
        }

        public Note()
        {
        }
    }
}
