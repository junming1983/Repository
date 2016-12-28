using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataContract;
using DBAgent;

namespace DataProvider
{
    public class ProductSpecificationManager : IProductSpecificationManager
    {
      public IEnumerable<ProductSpecification> GetProductSpecification()
      {
        return ProductSpecificationAdapter.GetProductSpecifications();
      }

      public IEnumerable<ProductSpecification> GetProductSpecification(long groupId)
      {
        return ProductSpecificationAdapter.GetProductSpecifications(groupId);
      }

      public long CreateProductSpecification(ProductSpecification specification)
      {
        return ProductSpecificationAdapter.Create(specification.Name, specification.GroupId);
      }
    }
}
