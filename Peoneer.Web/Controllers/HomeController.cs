using System.Collections.Generic;
using System.Web.Mvc;
using Peoneer.Library.Core;
using Peoneer.Web.Models;

namespace Peoneer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new ProjectsViewModel());
        }
    }
}