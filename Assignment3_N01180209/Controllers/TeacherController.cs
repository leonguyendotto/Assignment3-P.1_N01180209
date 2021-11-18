using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01180209.Models;

namespace Assignment3_N01180209.Controllers
{
    public class TeacherController : Controller
    {
        //Purpose - dynamic render webpage
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        //GET: /Teacher/List
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.TeacherList();
            return View(Teachers);
        }



        //GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }
    }
}