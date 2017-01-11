using System;
using System.Collections.Generic;
using System.Text;

namespace DataContract
{
  public interface IProductManager
  {
    IEnumerable<Product> GetProducts();
    IEnumerable<Product> GetProductsByGroupId(long groupId);
    IEnumerable<Product> GetProductsBySpecificationId(long specificationId);
  }
}
