using System.Linq;
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

        public static void CloseWindowsOfType<windowType>(this Application a) where windowType : Window
        {
            windowType[] windowTypeWindows = a.Windows.OfType<windowType>().ToArray();
            foreach (windowType w in windowTypeWindows)
                w.Close();
        }

        public static void HideWindowsOfType<windowType>(this Application a) where windowType : Window
        {
            windowType[] windowTypeWindows = a.Windows.OfType<windowType>().ToArray();
            foreach (windowType w in windowTypeWindows)
                w.Hide();
        }

        public static int CountWindowsOfType<windowType>(this Application a) where windowType : Window
        {
            windowType[] windowTypeWindows = a.Windows.OfType<windowType>().ToArray();
            return windowTypeWindows.Count();
        }

        public static windowType[] GetWindowsOfType<windowType>(this Application a) where windowType : Window
        {
            windowType[] windowTypeWindows = a.Windows.OfType<windowType>().ToArray();
            return windowTypeWindows;
        }

        public static bool WindowOfTypeExists<windowType>(this Application a) where windowType : Window
        {
            windowType[] windowTypeWindows = a.Windows.OfType<windowType>().ToArray();
            return windowTypeWindows.Any();
        }
    }
}
