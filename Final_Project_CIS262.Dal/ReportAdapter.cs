using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace Final_Project_CIS262.Dal
{
    public class ReportAdapter
    {

        private string _connectionString = @"Data Source=C:\Users\bryce\OneDrive\C#\C# 2 class\Final_Project_CIS262\Final_Project_CIS262\bin\School.db; datetimeformat=CurrentCulture;";

        public List<Report> GetReportRows()
        {
            // Declare the return type
            List<Report> returnValue = new List<Report>();
            // Create a connection to SQL lite. Wrap in a using statement for safety
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                // Create the commamd
                SQLiteCommand command = connection.CreateCommand();
                // Create the SQL Statement
                string sql = "SELECT COUNT(ExamId) AS 'Amount' FROM Exam INNER JOIN STUDENT ON Exam.StudentId = Student.StudentId GROUP BY Student.StudentId";
                // Pass the CommandText to the command
                command.CommandText = sql;
                // Open the database connection
                connection.Open();
                // Execute a command and return back a reader
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Call a method to retrieve the results
                    Report report = GetFromReader(reader);
                    // add the instance to the return list
                    returnValue.Add(report);
                }
                // return back the results
                return returnValue;
            }
        }

        private Report GetFromReader(DbDataReader reader)
        {
            // Create a new instance of the customer class
            Report repo = new Report();
            repo.Amount = reader.GetInt32(reader.GetOrdinal("Amount"));
            return repo;
        }
    }
}
