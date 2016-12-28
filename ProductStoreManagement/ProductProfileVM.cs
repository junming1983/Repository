using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;

namespace ProductStoreMananement
{
  public class ProductProfileVM : ViewModelBase
  {
    #region private filed
    private long _id;
    private string _name;
    private string _groupName;
    private string _typeName;
    private int _count;
    #endregion

    #region properties
    public long Id
    {
      get { return _id; }
      set 
      { 
        _id = value;
        OnPropertyChanged("Id");
      }
    }
    
    public string Name
    {
      get { return _name; }
      set 
      { 
        _name = value;
        OnPropertyChanged("Name");
      }
    }
    
    public string GroupName
    {
      get { return _groupName; }
      set
      { 
        _groupName = value;
        OnPropertyChanged("GroupName");
      }
    }
    
    public string TypeName
    {
      get { return _typeName; }
      set { _typeName = value; }
    }

    public int Count
    {
      get { return _count; }
      set 
      { 
        _count = value;
        OnPropertyChanged("Count");
      }
    }
    #endregion

    public ProductProfileVM Clone()
    {
      ProductProfileVM pVM = new ProductProfileVM();
      pVM.Id = this.Id;
      pVM.Name = this.Name;
      pVM.GroupName = this.GroupName;
      pVM.TypeName = this.TypeName;
      return pVM;
    }
  }
}
