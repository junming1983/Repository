using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Administrator
{
  public class NumberToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      long count;
      if(value == DependencyProperty.UnsetValue || !long.TryParse(value.ToString(), out count))
      {
        if (parameter == null || bool.Parse((string)parameter) == true)
        {
          return Visibility.Collapsed;
        }
        else
        {
          return Visibility.Visible;
        }
      }

      if (parameter == null || bool.Parse((string)parameter) == true)
      {
        return count < 1 ? Visibility.Collapsed : Visibility.Visible;
      }
      else
      {
        return count < 1 ? Visibility.Visible : Visibility.Collapsed;
      }

    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
