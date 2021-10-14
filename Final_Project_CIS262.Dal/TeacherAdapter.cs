using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace Final_Project_CIS262.Dal
{
    public class TeacherAdapter
    {
        private string _connectionString = @"Data Source= C:\Users\bryce\OneDrive\C#\C# 2 class\Final_Project_CIS262\Final_Project_CIS262\bin\School.db; datetimeformat=CurrentCulture;";
        
        public List<Teacher> GetAll()
        {
            List<Teacher> returnValue = new List<Teacher>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT TeacherId, FirstName, LastName From Teacher;";
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   Teacher teacher = GetFromReader(reader);

                    returnValue.Add(teacher);
                }

                return returnValue;
            }
        }

        public Teacher GetById(int teacherId)
        {
            Teacher returnValue = null;
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT TeacherId, FirstName, LastName FROM Teacher WHERE TeacherId = " + teacherId.ToString() + ";";
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = GetFromReader(reader);
                }

                return returnValue;
            }
        }

        public bool InsertTeacher(Teacher teacher)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Teacher (FirstName, LastName) VALUES ('" + teacher.FirstName + "', '" + teacher.LastName + "');";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateTeacher(Teacher teacher)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Teacher SET FirstName= '" + teacher.FirstName +
                    "', LastName = '" + teacher.LastName + "' WHERE TeacherId = " + teacher.TeacherId + ";";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DeleteTeacher(int teacherId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Teacher WHERE TeacherId = " + teacherId + ";";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private Teacher GetFromReader(DbDataReader reader)
        {
            Teacher teacher = new Teacher();
            teacher.TeacherId = reader.GetInt32(reader.GetOrdinal("TeacherId"));
            teacher.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
            teacher.LastName = reader.GetString(reader.GetOrdinal("LastName"));
            return teacher;
        }
    
    }
}
