using System;
using System.Collections.Generic;
using System.Text;

namespace DataContract
{
  public interface IProductSpecificationManager
  {
    IEnumerable<ProductSpecification> GetProductSpecification();
    IEnumerable<ProductSpecification> GetProductSpecification(long groupId);
    long CreateProductSpecification(ProductSpecification specification);
  }
}
