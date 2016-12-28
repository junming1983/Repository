using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DataContract;
using Infrastructure;

namespace ProductStoreMananement
{
  public class ProductStoreVM : ViewModelBase
  {
    #region private field
    private ObservableCollection<ProductGroup> _groups;
    private ObservableCollection<ProductProfileVM> _availableProducts;
    private ProductProfileVM _selectedProduct;
    private ObservableCollection<ProductProfileVM> _candidateProducts;
    #endregion

    #region properties
    public ObservableCollection<ProductGroup> Groups
    {
      get { return _groups; }
      set 
      { 
        _groups = value;
        OnPropertyChanged("Groups");
      }
    }

    
    public ObservableCollection<ProductProfileVM> AvailableProducts
    {
      get { return _availableProducts; }
      set 
      { 
        _availableProducts = value;
        OnPropertyChanged("AvailableProducts");
      }
    }
    
    public ProductProfileVM SelectedProduct
    {
      get { return _selectedProduct; }
      set 
      { 
        _selectedProduct = value;
        OnPropertyChanged("SelectedProduct");
      }
    }    

    public ObservableCollection<ProductProfileVM> CandidateProducts
    {
      get { return _candidateProducts; }
      set 
      { 
        _candidateProducts = value;
        OnPropertyChanged("CandidateProducts");
      }
    }
    #endregion

    #region ctor
    public ProductStoreVM()
    {
      LoadAvailableProductsCommand = new DelegateCommand(OnLoadAvailableProducts);
      LoadGroupsCommand = new DelegateCommand(OnLoadGroups);
      AddToCandidateCommand = new DelegateCommand(OnAddToCandidate);
      RemoveFromCandidateCommand = new DelegateCommand(OnRemoveFromCandidate);
    }
    #endregion

    #region commands
    public DelegateCommand LoadAvailableProductsCommand { get; set; }
    public DelegateCommand LoadGroupsCommand { get; set; }
    public DelegateCommand AddToCandidateCommand { get; set; }
    public DelegateCommand RemoveFromCandidateCommand { get; set; }
    #endregion

    #region private methods
    private void OnLoadAvailableProducts(object arg) { }
    private void OnLoadGroups(object arg) { }
    private void OnAddToCandidate(object arg) { }
    private void OnRemoveFromCandidate(object arg) { }
    #endregion
  }
}
