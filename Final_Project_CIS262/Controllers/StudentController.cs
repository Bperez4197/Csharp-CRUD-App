using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project_CIS262.Models;
using Final_Project_CIS262.Dal;

namespace Final_Project_CIS262.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            StudentAdapter adapter = new StudentAdapter();
            List<Student> students = adapter.GetAll();
            AllStudentsModel model = new AllStudentsModel();
            List<StudentModel> studentModels = new List<StudentModel>();
            foreach (Student student in students)
            {
                StudentModel studentModel = new StudentModel();
                studentModel.StudentId = student.StudentId;
                studentModel.FirstName = student.FirstName;
                studentModel.LastName = student.LastName;
                studentModels.Add(studentModel);
            }
            model.Students = studentModels;
            return View(model);
        }


        public ActionResult Add()
        {
            StudentModel model = new StudentModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                StudentAdapter studentAdapter = new StudentAdapter();
                Student student = new Student();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                bool returnValue = studentAdapter.InsertStudent(student);
                if (returnValue)
                {
                    return RedirectToAction("Index");
                }else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Edit(int studentId)
        {
            StudentAdapter studentAdapter = new StudentAdapter();
            Student student = studentAdapter.GetById(studentId);
            if(student == null)
            {
                return RedirectToAction("Index");
            }else
            {
                StudentModel model = new StudentModel();
                model.StudentId = student.StudentId;
                model.FirstName = student.FirstName;
                model.LastName = student.LastName;
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult Edit(StudentModel model)
        {
            if (ModelState.IsValid)
            {
                StudentAdapter studentAdapter = new StudentAdapter();
                Student student = new Student();
                student.StudentId = model.StudentId;
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
        
                bool returnValue = studentAdapter.UpdateStudent(student);
                if (returnValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult Delete(int studentId)
        {
            StudentAdapter studentAdapater = new StudentAdapter();
            bool returnValue = studentAdapater.DeleteStudent(studentId);
            return RedirectToAction("Index");
        }
    }
}