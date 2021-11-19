using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assignment3_N01180209.Models;

namespace Assignment3_N01180209.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Class/List
        public ActionResult List(string SearchKey = null)
        {

            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ClassLists(SearchKey);
            return View(Classes);
        }

        //GET: /Class/Show/{id}
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);
            return View(NewClass);
        }
    }
}