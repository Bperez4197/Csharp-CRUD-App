using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project_CIS262.Models;
using Final_Project_CIS262.Dal;

namespace Final_Project_CIS262.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            TeacherAdapter adapter = new TeacherAdapter();
            List<Teacher> teachers = adapter.GetAll();
            AllTeachersModel model = new AllTeachersModel();
            List<TeacherModel> teacherModels = new List<TeacherModel>();
            foreach (Teacher teacher in teachers)
            {
                TeacherModel teacherModel = new TeacherModel();
                teacherModel.TeacherId = teacher.TeacherId;
                teacherModel.FirstName = teacher.FirstName;
                teacherModel.LastName = teacher.LastName;
                teacherModels.Add(teacherModel);
            }
            model.Teachers = teacherModels;
            return View(model);
        }


        public ActionResult Add()
        {
            TeacherModel model = new TeacherModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(TeacherModel model)
        {
            if (ModelState.IsValid)
            {
                TeacherAdapter teacherAdapter = new TeacherAdapter();
                Teacher teacher = new Teacher();
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;
                bool returnValue = teacherAdapter.InsertTeacher(teacher);
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

        public ActionResult Edit(int teacherId)
        {
            TeacherAdapter teacherAdapter = new TeacherAdapter();
            Teacher teacher = teacherAdapter.GetById(teacherId);
            if (teacher == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TeacherModel model = new TeacherModel();
                model.TeacherId = teacher.TeacherId;
                model.FirstName = teacher.FirstName;
                model.LastName = teacher.LastName;
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult Edit(TeacherModel model)
        {
            if (ModelState.IsValid)
            {
                TeacherAdapter teacherAdapter = new TeacherAdapter();
                Teacher teacher = new Teacher();
                teacher.TeacherId = model.TeacherId;
                teacher.FirstName = model.FirstName;
                teacher.LastName = model.LastName;

                bool returnValue = teacherAdapter.UpdateTeacher(teacher);
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

        public ActionResult Delete(int teacherId)
        {
            TeacherAdapter teacherAdapater = new TeacherAdapter();
            bool returnValue = teacherAdapater.DeleteTeacher(teacherId);
            return RedirectToAction("Index");
        }
    }
}