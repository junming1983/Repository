using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataContract;
using DBAgent;

namespace DataProvider
{
  public class ProductManager : IProductManager
  {
    public IEnumerable<Product> GetProducts()
    {
      return ProductAdapter.GetProducts();
    }

    public IEnumerable<Product> GetProductsByGroupId(long groupId)
    {
      return ProductAdapter.GetProductsByGroupId(groupId);
    }

    public IEnumerable<Product> GetProductsBySpecificationId(long specificationId)
    {
      return ProductAdapter.GetProducts();
    }
  }

}
