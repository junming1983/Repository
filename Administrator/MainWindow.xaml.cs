using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataContract;
using DataProvider;
using DBAgent;

namespace Administrator
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      IProductGroupManager productManager = new ProductGroupManager();
      IProductSpecificationManager specificationManager = new ProductSpecificationManager();
      GroupManagementVM gmVM = new GroupManagementVM(productManager, specificationManager);
      gmVM.OnLoad();
      GroupManagerItem.DataContext = gmVM;
    }
  }
}
