using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project_CIS262.Models;
using Final_Project_CIS262.Dal;

namespace Final_Project_CIS262.Controllers
{
    public class ExamController : Controller
    {
        // GET: Exam
        public ActionResult Index(int studentId = 0)
        {
            ExamAdapter adapter = new ExamAdapter();
            List<Exam> exams = new List<Exam>();
            if (studentId == 0)
            {
                exams = adapter.GetAll();
            }
            else
            {
                exams = adapter.GetByStudentId(studentId);
            }
            AllExamsModel model = new AllExamsModel();
            List<ExamModel> examModels = new List<ExamModel>();
            foreach (Exam exam in exams)
            {
                ExamModel examModel = new ExamModel();
                examModel.ExamId = exam.ExamId;
                examModel.StudentId = exam.StudentId;
                examModel.Score = exam.Score;
                examModels.Add(examModel);
            }
            model.Exams = examModels;
            return View(model);
        }

        public ActionResult Add(int studentId)
        {
            ExamModel model = new ExamModel();
            model.StudentId = studentId;
            return View(model);
        }



        [HttpPost]
        public ActionResult Add(ExamModel model)
        {
            if (ModelState.IsValid)
            {
                ExamAdapter examAdapter = new ExamAdapter();
                Exam exam = new Exam();
                exam.ExamId = model.ExamId;
                exam.StudentId = model.StudentId;
                exam.Score = model.Score;

                bool returnValue = examAdapter.InsertExam(exam);
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

        public ActionResult Edit(int examId)
        {
            ExamAdapter examAdapter = new ExamAdapter();
            Exam exam = examAdapter.GetById(examId);
            if (exam == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ExamModel model = new ExamModel();
                model.ExamId = exam.ExamId;
                model.StudentId = exam.StudentId;
                model.Score = exam.Score;
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult Edit(ExamModel model)
        {
            if (ModelState.IsValid)
            {
                ExamAdapter examAdapter = new ExamAdapter();
                Exam exam = new Exam();
                exam.ExamId = model.ExamId;
                exam.StudentId = model.StudentId;
                exam.Score = model.Score;
                bool returnValue = examAdapter.UpdateExam(exam);
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

        public ActionResult Delete(int examId)
        {
            ExamAdapter examAdapter = new ExamAdapter();
            bool returnValue = examAdapter.DeleteExam(examId);
            return RedirectToAction("Index");
        }
    }
}