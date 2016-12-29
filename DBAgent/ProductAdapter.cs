using DataContract;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DBAgent
{
  public class ProductAdapter
  {
    private const string _connectionString = "Data Source=MyDatabase.sqlite";
    private const string _productTableName = "Product";
    private const string _groupTableName = "ProductGroup";
    private const string _specificationTableName = "ProductSpecification";

    public static IEnumerable<Product> GetProducts(long groupId)
    {
      string cmdText = string.Format("SELECT pd.Id, pd.Name, ps.Name, pg.Name FROM  {0} as pd " +
        "JOIN {1} as ps ON ps.Id = pd.SpecificationId " +
        "JOIN {2} as pg ON pg.Id = ps.GroupId " +
        "WHERE pd.GroupId = {3}", _productTableName, _specificationTableName, _groupTableName, groupId);
      return ExecuateReader(cmdText);
    }

    public static IEnumerable<Product> GetProducts()
    {
      string cmdText = string.Format("SELECT pd.Id, pd.Name, ps.Name, pg.Name FROM  {0} as pd " +
        "JOIN {1} as ps ON ps.Id = pd.SpecificationId " +
        "JOIN {2} as pg ON pg.Id = ps.GroupId", _productTableName, _specificationTableName, _groupTableName);
      return ExecuateReader(cmdText);
    }

    private static IEnumerable<Product> ExecuateReader(string cmdText)
    {
      List<Product> products = new List<Product>();
      using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
      {
        try
        {
          conn.Open();
          SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
          SQLiteDataReader reader = cmd.ExecuteReader();
          while (reader.Read())
          {
            products.Add(Construct(reader));
          }
        }
        catch (Exception ex)
        {
          throw;
        }
        finally
        {
          if (conn != null)
          {
            conn.Close();
          }
        }
      }
      return products;
    }

    private static Product Construct(SQLiteDataReader reader)
    {
      Product product = new Product();
      product.Id = reader.GetInt64(0);
      product.Name = reader.GetString(1);
      product.TypeName = reader.GetString(2);
      product.GroupName = reader.GetString(3);
      return product;
    }

    public static long Create(string name, long groupId, long typeId)
    {
      string cmd = string.Format("INSERT INTO {0}(Name, GroupId) VALUES(\"{1}\", {2})", _productTableName, name, groupId);
      return ExecuteWithQueryId(cmd);
    }

    public static void Update(long specificationId, string name)
    {
      string cmd = string.Format("UPDATE {0} SET Name = '{1}' WHERE Id = {2}", _productTableName, name, specificationId);
      ExecuteNonQuery(cmd);
    }

    private static void ExecuteNonQuery(string cmdText)
    {
      using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
      {
        try
        {
          conn.Open();
          SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
          cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
          throw;
        }
        finally
        {
          if (conn != null)
          {
            conn.Close();
          }
        }
      }
    }

    private static long ExecuteWithQueryId(string cmdText)
    {
      long res = -1;
      using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
      {
        try
        {
          conn.Open();
          SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
          cmd.ExecuteNonQuery();
          cmd.CommandText = string.Format("SELECT MAX(Id) FROM {0}", _productTableName);

          object maxId = cmd.ExecuteScalar();

          if (maxId != DBNull.Value)
          {
            res = Convert.ToInt64(maxId);
          }
        }
        catch (Exception ex)
        {
          throw;
        }
        finally
        {
          if (conn != null)
          {
            conn.Close();
          }
        }
      }
      return res;
    }
  }
}
