using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_Project_CIS262.Models;

namespace Final_Project_CIS262.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            ReportModel model = new ReportModel();
            model.PopulateReport();
            return View(model);
        }
    }
}