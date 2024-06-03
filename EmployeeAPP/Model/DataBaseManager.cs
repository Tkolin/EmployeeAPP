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
            Database = "employeedb",
            UserID = "root",
            Password = "root" // "tkl909"//"nrjkby99"
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
    /// Получение данных из базы

    public static List<Employee> GetEmployee()
    {
        string query = @"SELECT *
                     FROM employee e
                     INNER JOIN passport pass ON e.Passport_ID = pass.ID
                     INNER JOIN gender gen ON e.Gender_ID = gen.ID
                     INNER JOIN family_location fam ON e.Family_Location_ID = fam.ID";
        return GetData(query, reader => new Employee(
            reader.GetInt32("ID"),
            reader.GetString("First_Name"),
            reader.GetString("Last_Name"),
            reader.GetString("Patronymic"),
            reader.GetDateTime("Birth_Date"),
            reader.GetString("Birth_Adress"),
            reader.GetString("Phone_Number"),
            reader.GetString("INN"),
            reader.GetInt32("Passport_ID"),
            reader.GetInt32("Gender_ID"),
            reader.GetInt32("Family_Location_ID"),
            
            reader.GetString("Name"),
            reader.GetString("registration_address"),
            reader.GetString("Name"),
        reader.GetString("Passport_Series") + " " + reader.GetString("Passport_number"),
            
            reader.GetString("Login"),
            reader.GetString("Password")

        ));
    }
    public static List<EmployeeToWork> GetEmployeeToWork()
    {
        string query = @"SELECT *
                     FROM employee_to_work e
                     INNER JOIN position pos ON e.Position_ID = pos.ID
                     INNER JOIN unit unit ON e.Unit_ID = unit.ID";
        
        return GetData(query, reader => new EmployeeToWork(
            reader.GetInt32("ID"),
            reader.GetInt32("Employee_ID"),
            reader.GetInt32("Position_ID"),
            reader.GetInt32("Unit_ID"),
            reader.GetDateTime("Date_CreateEmployment"),
            reader.GetDateTime("Date_Employment"),
            reader.GetDateTime("Date_CreateDosmissal"),
            reader.GetDateTime("Date_Dosmissal"),
            
            reader.GetString("Name"),
            reader.GetString("Name")
         ));
    }


    public static List<FamilyLocation> GetFamilyLocation()
    {
        string query = "SELECT * FROM family_location";
        return GetData(query, reader => new FamilyLocation(
            reader.GetInt32("ID"),
            reader.GetString("name")
        ));
    }
    public static List<Gender> GetGender()
    {
        string query = "SELECT * FROM gender";
        return GetData(query, reader => new Gender(
            reader.GetInt32("ID"),
            reader.GetString("name")
        ));
    }
    public static List<Passport> GetPassport()
    {
        string query = "SELECT * FROM passport";
        return GetData(query, reader => new Passport(
            reader.GetInt32("ID"),
            reader.GetString("Passport_Series"),
            reader.GetString("Passport_number"),
            reader.GetString("registration_address"),
            reader.GetString("positions_address")
        ));
    }
    public static List<Position> GetPosition()
    {
        string query = "SELECT * FROM position";
        return GetData(query, reader => new Position(
            reader.GetInt32("ID"),
            reader.GetString("name")
        ));
    }
    public static List<Unit> GetUnit()
    {
        string query = "SELECT * FROM unit";
        return GetData(query, reader => new Unit(
            reader.GetInt32("ID"),
            reader.GetString("name")
        ));
    }
    
    
    /// Добавление Удаление Обновление
    public static void AddEmployee(Employee data)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "first_Name", data.First_Name},
            { "last_Name", data.Last_Name},
            { "patronymic",data.Patronymic},
            { "birth_Date",data.Birth_Date},
            { "birth_Adress",data.Birth_Adress},
            { "phone_Number",data.Phone_Number},
            { "inn",data.INN},
            { "passport_ID",data.Passport_ID},
            { "gender_ID",data.Gender_ID},
            { "family_Location_ID",data.Family_Location_ID},
            { "login", "l1"},
            { "password","p1"}
        };

        AddEntity(data, "employee", parameters);
    }

    public static void UpdateEmployee(Employee data)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "ID", data.ID },
            { "first_Name", data.First_Name},
            { "last_Name", data.Last_Name},
            { "patronymic",data.Patronymic},
            { "birth_Date",data.Birth_Date},
            { "birth_Adress",data.Birth_Adress},
            { "phone_Number",data.Phone_Number},
            { "inn",data.INN},
            { "passport_ID",data.Passport_ID},
            { "gender_ID",data.Gender_ID},
            { "family_Location_ID",data.Family_Location_ID},
            { "login",data.Login},
            { "password",data.Password}
        };

        UpdateEntity(data, "employee", parameters, data.ID.ToString());
    }

    public static void DeleteEmploye(Employee data)
    {
        DeleteEntity("employee", "ID", data.ID);
    }
    /// <summary>
    /// Passport
    /// </summary>
    /// <param name="data"></param>
    public static void AddPassport(Passport data)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "Passport_Series", data.Passport_Series},
            { "Passport_number", data.Passport_Number},
            { "registration_address",data.Registration_Address},
            { "positions_address",data.Positions_Address}
        };

        AddEntity(data, "passport", parameters);
    }

    public static void UpdatePassport(Passport data)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "ID", data.ID },
            { "Passport_Series", data.Passport_Series},
            { "Passport_number", data.Passport_Number},
            { "registration_address",data.Registration_Address},
            { "positions_address",data.Positions_Address}
        };

        UpdateEntity(data, "passport", parameters, data.ID.ToString());
    }

    public static void DeletePassport(Passport data)
    {
        DeleteEntity("passport", "ID", data.ID);
    }
    /// <summary>
    /// EmployeeToWork
    /// </summary>
    /// <param name="data"></param>
    public static void AddEmployeeToWork(EmployeeToWork data)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "Employee_ID", data.Employee_ID},
            { "Position_ID", data.Position_ID},
            { "Unit_ID",data.Unit_ID},
            { "Date_CreateEmployment",data.DateCreateEmployment},
            { "Date_Employment",data.DateEmployment},
            { "Date_CreateDosmissal",data.DateCreateDosmissal},
            { "Date_Dosmissal",data.DateDosmissal}
        };

        AddEntity(data, "employee_to_work", parameters);
    }

    public static void UpdateEmployeeToWork(EmployeeToWork data)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "ID", data.ID },
            { "Employee_ID", data.Employee_ID},
            { "Position_ID", data.Position_ID},
            { "Unit_ID",data.Unit_ID},
            { "Date_CreateEmployment",data.DateCreateEmployment},
            { "Date_Employment",data.DateEmployment},
            { "Date_CreateDosmissal",data.DateCreateDosmissal},
            { "Date_Dosmissal",data.DateDosmissal}
        };

        UpdateEntity(data, "employee_to_work", parameters, data.ID.ToString());
    }

    public static void DeleteEmployeeToWork(EmployeeToWork data)
    {
        DeleteEntity("employee_to_work", "ID", data.ID);
    }



    
}