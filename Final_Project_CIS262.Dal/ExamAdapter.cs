using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace Final_Project_CIS262.Dal
{
    public class ExamAdapter
    {
        private string _connectionString = @"Data Source= C:\Users\bryce\OneDrive\C#\C# 2 class\Final_Project_CIS262\Final_Project_CIS262\bin\School.db; datetimeformat=CurrentCulture;";

        public List<Exam> GetAll()
        {
            List<Exam> returnValue = new List<Exam>();

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "Select ExamId, StudentId, Score FROM Exam";
                connection.Open();

                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Exam exam = GetFromReader(reader);

                    returnValue.Add(exam);
                }
                return returnValue;
            }
        }

        public List<Exam> GetByStudentId(int studentId)
        {
            List<Exam> returnValue = new List<Exam>();
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {

                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ExamId, StudentId, Score FROM Exam WHERE StudentId = " + studentId.ToString() + ";";
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Exam exam = GetFromReader(reader);
                    returnValue.Add(exam);
                }

                return returnValue;
            }
        }

        public Exam GetById(int examId)
        {
            Exam returnValue = null;
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT ExamId, StudentId, Score FROM Exam WHERE ExamId = " + examId + ";";
                connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = GetFromReader(reader);
                }

                return returnValue;
            }
        }

        public bool InsertExam(Exam exam)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Exam (ExamId, StudentId, Score) VALUES ('" + exam.ExamId + "', '" + exam.StudentId + "', '" + exam.Score + "');";
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

        public bool UpdateExam(Exam exam)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Exam SET Score = " + exam.Score + " WHERE ExamId = " + exam.ExamId +";";
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

        public bool DeleteExam(int examId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Exam Where ExamId = " + examId + ";";
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

        private Exam GetFromReader(DbDataReader reader)
        {
            Exam exam = new Exam();
            exam.ExamId = reader.GetInt32(reader.GetOrdinal("ExamId"));
            exam.StudentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
            exam.Score = reader.GetInt32(reader.GetOrdinal("Score"));
            return exam;

        }
    }
}
