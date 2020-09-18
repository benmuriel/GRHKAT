using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.PosteEmploi.Controllers
{
    public class HomeController : Controller
    {
        // GET: ServiceEmploi
        public ActionResult Index()
        { 
            return View( );
        }
        public ActionResult Poste(long id)
        {
            v_poste data = DATACCESS.ModulePlanning.PosteGet(id);
            return View(data);
        }

    }
}