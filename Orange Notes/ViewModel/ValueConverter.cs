using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Orange_Notes.ViewModel
{
    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if (!s.StartsWith("● "))
                s = "● " + s;
            s = s.Replace(System.Environment.NewLine, System.Environment.NewLine + "● ");

            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = value as string;
            if (s.StartsWith("● "))
                s = s.Remove(0, 2);
            s = s.Replace(System.Environment.NewLine + "● ", System.Environment.NewLine);

            return s;
        }
    }
}
