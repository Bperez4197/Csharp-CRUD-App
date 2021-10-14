using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace Final_Project_CIS262.Dal
{
    public class StudentAdapter
    {
        private string _connectionString = @"Data Source= C:\Users\bryce\OneDrive\C#\C# 2 class\Final_Project_CIS262\Final_Project_CIS262\bin\School.db; datetimeformat=CurrentCulture;";

        public List<Student> GetAll()
        {
            List<Student> returnValue = new List<Student>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT StudentId, FirstName, LastName From Student;";
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Student student = GetFromReader(reader);

                    returnValue.Add(student);
                }

                return returnValue;
            }

        }

        public Student GetById(int studentId)
        {

            Student returnValue = null;
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT StudentId, FirstName, LastName FROM Student WHERE StudentId = " + studentId.ToString() + ";";
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = GetFromReader(reader);
                }

                return returnValue;
            }

        }

        public bool InsertStudent(Student student)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Student (FirstName, LastName) VALUES ('" + student.FirstName + "', '" + student.LastName + "');";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }

        public bool UpdateStudent(Student student)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Student SET FirstName= '" + student.FirstName +
                    "', LastName = '" + student.LastName + "' WHERE StudentId = " + student.StudentId + ";"; 
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if(rows > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }

        public bool DeleteStudent(int studentId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Student WHERE StudentId = " + studentId + ";";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if(rows > 0)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
        }

        private Student GetFromReader(DbDataReader reader)
        {
            Student student = new Student();
            student.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
            student.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
            student.LastName = reader.GetString(reader.GetOrdinal("LastName"));
            return student;
        }
    }
}
