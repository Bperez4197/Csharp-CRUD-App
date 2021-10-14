using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Final_Project_CIS262.Dal;
using System.ComponentModel.DataAnnotations;

namespace Final_Project_CIS262.Models
{
    public class StudentModel
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class AllStudentsModel
    {
        public List<StudentModel> Students { get; set; }
    }
}