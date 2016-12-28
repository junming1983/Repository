using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataContract;
using DBAgent;

namespace DataProvider
{
  public class ProductGroupManager : IProductGroupManager
  {
    public IEnumerable<ProductGroup> GetGroups()
    {
      return ProductGroupAdapter.GetProductGroups();
    }

    public long CreateGroup(ProductGroup group)
    {
      return ProductGroupAdapter.Create(group.Name);
    }
  }
}
