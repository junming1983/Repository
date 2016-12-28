using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Infrastructure
{
  public class DelegateCommand : ICommand
  {
    private Action<object> _action;
    public DelegateCommand(Action<object> action)
    {
      _action = action;
    }
    public bool CanExecute(object parameter)
    {
      return _action != null;
    }

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      if(_action != null)
      {
        _action.Invoke(parameter);
      }
    }
  }
}
