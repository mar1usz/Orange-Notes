using System.Windows;

namespace Orange_Notes.View
{
    public static class Extensions
    {
        public static void setRestoreBounds(this Window w, Rect bounds)
        {
            w.Left = bounds.Left;
            w.Top = bounds.Top;
            w.Width = bounds.Width;
            w.Height = bounds.Height;
        }
    }
}
