using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using DataContract;

namespace DBAgent
{
  public class ProductGroupAdapter
  {
    private static string _connectionString = "Data Source=MyDatabase.sqlite;Version=3;Password=Junming123";
    private static string _tableName = "ProductGroup";

    //public static void CreateDB()
    //{
    //  SQLiteConnection.CreateFile("MyDatabase.db");
    //}

    //public static void CreateTable()
    //{
    //  string cmd = string.Format("CREATE TABLE {0} (Id int, Name varchar(256))", _tableName);
    //  ExecuteNonQuery(cmd); 
    //}

    //public static void RemoveTable()
    //{
    //  string cmd = string.Format("DROP TABLE '{0}'", _tableName);
    //  ExecuteNonQuery(cmd);      
    //}

    public static IEnumerable<ProductGroup> GetProductGroups()
    {
      List<ProductGroup> groups = new List<ProductGroup>();
      using (SQLiteConnection conn = new SQLiteConnection(_connectionString))
      {
        string cmdText = string.Format("SELECT * FROM '{0}'", _tableName);
        try
        {
          conn.Open();
          SQLiteCommand cmd = new SQLiteCommand(cmdText, conn);
          SQLiteDataReader reader = cmd.ExecuteReader();
          while (reader.Read())
          {
            ProductGroup group = new ProductGroup();
            group.Id = reader.GetInt64(0);
            group.Name = reader.GetString(1);
            groups.Add(group);
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
      return groups;
    }

    public static long Create(string groupName)
    {
      string cmd = string.Format("INSERT INTO '{0}'('Name') VALUES('{1}')", _tableName, groupName);
      return ExecuteWithQueryId(cmd);
    }

    public static void Update(long groupId, string groupName)
    {
      string cmd = string.Format("UPDATE '{0}' SET Name = '{1}' WHERE Id = {2}", _tableName, groupName, groupId);
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
