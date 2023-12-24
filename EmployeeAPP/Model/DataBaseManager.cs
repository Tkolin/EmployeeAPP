using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;

namespace EmployeeAPP.Model;

public class DataBaseManager
{
    /// Настройки подключения
    public static MySqlConnectionStringBuilder ConnectionString =
        new MySqlConnectionStringBuilder
        {
            Server = "localhost",
            Database = "up_an",
            UserID = "root",
            Password = "tkl909" // "tkl909"//"nrjkby99"
        };
    
    /// Шаблоны
    public static List<T> GetData<T>(string query, Func<MySqlDataReader, T> mapFunction)
    {
        List<T> data = new List<T>();
        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
            using (var comand = connection.CreateCommand())
            {
                comand.CommandText = query;
                using (var reader = comand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        data.Add(mapFunction(reader));
                    }
                }
            }

            connection.Close();
        }

        return data;
    }
    public static void AddEntity<T>(T entity, string tableName, Dictionary<string, object> parameters)
    {
        string columns = string.Join(", ", parameters.Keys);
        string values = string.Join(", ", parameters.Keys.Select(key => "@" + key));

        string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                }

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public static void UpdateEntity<T>(T entity, string tableName, Dictionary<string, object> parameters, string idRow)
    {
        string setClause = string.Join(", ", parameters.Keys.Select(key => $"{key} = @{key}"));
        string query = $"UPDATE {tableName} SET {setClause} WHERE ID = {idRow}";

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value);
                }

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    public static void DeleteEntity(string tableName, string idColumnName, int id)
    {
        string query = $"DELETE FROM {tableName} WHERE {idColumnName} = @ID";

        using (var connection = new MySqlConnection(ConnectionString.ConnectionString))
        {
            connection.Open();
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID", id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }
    
    /// Функцианальные запросы

}