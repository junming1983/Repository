using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using DataContract;

namespace DBAgent
{
  public class ProductSpecificationAdapter
  {
    private static string _connectionString = "Data Source=MyDatabase.sqlite";
    private static string _tableName = "ProductSpecification";

    public static IEnumerable<ProductSpecification> GetProductSpecifications(long groupId)
    {
      string cmdText = string.Format("SELECT * FROM {0} WHERE GroupId={1}", _tableName, groupId);
      return ExecuateReader(cmdText);
    }

    public static IEnumerable<ProductSpecification> GetProductSpecifications()
    {
      string cmdText = string.Format("SELECT * FROM {0}", _tableName);
      return ExecuateReader(cmdText);
    }

    private static IEnumerable<ProductSpecification> ExecuateReader(string cmdText)
    {
      List<ProductSpecification> specifications = new List<ProductSpecification>();
      using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
      {
        try
        {
          conn.Open();
          SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
          SQLiteDataReader reader = cmd.ExecuteReader();
          while (reader.Read())
          {
            ProductSpecification specification = new ProductSpecification();
            specification.Id = reader.GetInt64(0);
            specification.GroupId = reader.GetInt64(1);
            specification.Name = reader.GetString(2);
            specifications.Add(specification);
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
      return specifications;
    }

    public static long Create(string name, long groupId)
    {
      string cmd = string.Format("INSERT INTO {0}('Name', 'GroupId') VALUES('{1}', {2})", _tableName, name, groupId);
      return ExecuteWithQueryId(cmd);
    }

    public static void Update(long specificationId, string name)
    {
      string cmd = string.Format("UPDATE {0} SET Name = '{1}' WHERE Id = {2}", _tableName, name, specificationId);
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
          cmd.CommandText = string.Format("SELECT MAX(Id) FROM {0}", _tableName);

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
