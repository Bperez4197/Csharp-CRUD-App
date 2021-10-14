using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project_CIS262.Dal;
using System.ComponentModel.DataAnnotations;

namespace Final_Project_CIS262.Models
{
    public class ExamModel
    {
        public int ExamId { get; set; }
        public int StudentId { get; set; }
        public int Score { get; set; }
    }

    public class AllExamsModel
    {
        public List<ExamModel> Exams { get; set; }
    }
}