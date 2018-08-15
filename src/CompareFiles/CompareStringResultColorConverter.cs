using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CompareFiles
{
    public class CompareStringResultColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = value as CompareStringResult;
            if (v == null) return Brushes.Black;


            var color = GetMessageColor(v.Action);
            var brush = new SolidColorBrush(color);
            return brush;
        }

        private Color GetMessageColor(CompareStringResult.ActionType action)
        {
            if (action == CompareStringResult.ActionType.Added) return Colors.Green;
            if (action == CompareStringResult.ActionType.Deleted) return Colors.Red;
            if (action == CompareStringResult.ActionType.Changed) return Colors.CornflowerBlue;

            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}