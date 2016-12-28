using DataContract;
using Infrastructure;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Administrator
{
  public class GroupManagementVM : ViewModelBase
  {
    #region private field
    private bool _canAddCustomProductGroup;
    private string _customGroupName;
    private ProductGroup _selectedProductGroup;
    private ObservableCollection<ProductGroup> _productGroups;
    private IProductGroupManager _productGroupManager;
    private IProductSpecificationManager _productSpecificationManager;
    private string _productionSpecificationName;
    private ObservableCollection<ProductSpecification> _productSpecifications;
    #endregion

    public GroupManagementVM(IProductGroupManager productGroupManager, IProductSpecificationManager productSpecificationManager)
    {
      AddProductGroupCommand = new DelegateCommand(OnAddProductGroup);
      AddProductSpecificationCommand = new DelegateCommand(OnAddProductSpecification);
      _productGroupManager = productGroupManager;
      _productSpecificationManager = productSpecificationManager;
    }


    #region properties
    public DelegateCommand AddProductGroupCommand { get; set; }
    public DelegateCommand AddProductSpecificationCommand { get; set; }

    
    public bool CanAddCustomProductGroup
    {
      get { return _canAddCustomProductGroup; }
      set 
      { 
        _canAddCustomProductGroup = value;
        OnPropertyChanged("CanAddCustomProductGroup");
      }
    }

    public string CustomGroupName
    {
      get { return _customGroupName; }
      set { 
        _customGroupName = value;
        OnPropertyChanged("CustomGroupName");
      }
    }

    public ProductGroup SelectedProductGroup
    {
      get { return _selectedProductGroup; }
      set
      {
        _selectedProductGroup = value;
        OnProductGroupChanged(value);
        OnPropertyChanged("SelectedProductGroup");
      }
    }
    
    public ObservableCollection<ProductGroup> ProductGroups
    {
      get { return _productGroups; }
      set 
      { 
        _productGroups = value;
        OnPropertyChanged("ProductGroups");
      }
    }

    public string ProductSpecificationName
    {
      get { return _productionSpecificationName; }
      set 
      { 
        _productionSpecificationName = value;
        OnPropertyChanged("ProductSpecificationName");
      }
    }
        
    public ObservableCollection<ProductSpecification> ProductSpecifications
    {
      get { return _productSpecifications; }
      set 
      { 
        _productSpecifications = value;
        OnPropertyChanged("ProductSpecifications");
      }
    }

    #endregion

    public void OnLoad()
    {
      ProductGroups = new ObservableCollection<ProductGroup>(_productGroupManager.GetGroups());
    }

    private void OnProductGroupChanged(ProductGroup productGroup)
    {
      if(productGroup == null)
      {}
      else
      {
        ProductSpecifications = new ObservableCollection<ProductSpecification>(_productSpecificationManager.GetProductSpecification(productGroup.Id));
      }
    }

    private void OnAddProductGroup(object arg)
    {
      if (string.IsNullOrEmpty(_customGroupName)) return;
      string newName = _customGroupName.Trim().ToLower();
      if(!_productGroups.Any(t=>t.Name.ToLower() == newName))
      {
        long id = _productGroupManager.CreateGroup(new ProductGroup() { Name = _customGroupName.Trim() });
        if (id != -1)
        {
          ProductGroup group = new ProductGroup() { Id = id, Name = _customGroupName.Trim() };
          ProductGroups.Add(group);
        }
        
      }
    }
    private void OnAddProductSpecification(object arg)
    {
      if (string.IsNullOrEmpty(_productionSpecificationName)) return;
      string newName = _productionSpecificationName.Trim().ToLower();
      if(_selectedProductGroup != null && !_productSpecifications.Any(t=>t.Name.ToLower() == newName))
      {
        _productSpecificationManager.CreateProductSpecification(new ProductSpecification() { GroupId = _selectedProductGroup.Id, Name = _productionSpecificationName.Trim()});
      }
    }
  }
}
