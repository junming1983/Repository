using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Infrastructure
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }
    #region INotifyPropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;
    #endregion
  }
}
