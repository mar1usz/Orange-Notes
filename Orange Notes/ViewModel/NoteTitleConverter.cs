using System;
using System.Globalization;
using System.Windows.Data;

namespace Orange_Notes.ViewModel
{
    public class NoteTitleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if(s != null)
            {
                if (s.Length > 0)
                    s = "#" + s;

                return s;
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if (s != null)
            {
                if (s.StartsWith("#"))
                    s = s.Remove(0, 1);

                return s;
            }
            else
            {
                return value;
            }
        }
    }
}
