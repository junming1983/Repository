using System;
using System.Collections.Generic;
using System.Text;

namespace DataContract
{
  public interface IProductGroupManager
  {
    IEnumerable<ProductGroup> GetGroups();
    long CreateGroup(ProductGroup group);
  }
}
