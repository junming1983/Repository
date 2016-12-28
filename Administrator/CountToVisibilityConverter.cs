using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Administrator
{
  public class CountToVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      Visibility vb = Visibility.Collapsed;
      int count = 0;
      if(parameter != null)
      {
        try
        {
          vb = (Visibility)Enum.Parse(typeof(Visibility), (string)parameter);
        }
        catch (Exception ex)
        {
          throw;
        }
      }
      if(value == DependencyProperty.UnsetValue)
      {
        return vb;
      }
      else
      {
        if((int)value == 0)
        {
          return Visibility.Collapsed;
        }
        else
        {
          return vb;
        }
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
