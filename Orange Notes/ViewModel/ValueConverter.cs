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
            string n = System.Environment.NewLine;
            string s = value as string;
            int last;

            if (s.Contains(n))
            {
                s = s.StartsWith("● ") ? s : "● " + s;
                s = s.Replace(n, n + "● ");
                last = s.LastIndexOf(n + "● ") + (n).Length;
                s = s.Remove(last, 2);
            }

            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string n = System.Environment.NewLine;
            string s = value as string;

            if (s.Contains(n))
            {
                s = s.StartsWith("● ") ? s.Remove(0, 2) : s;
                s = s.Replace(n + "● ", n);
            } 

            return s;
        }
    }
}
