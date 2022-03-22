using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.Situation.Controllers
{
    public class RecrutementController : Controller
    {
        // GET: Situation/Recrutement
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(int id)
        {
            type_position model = DATACCESS.ModulePlanning.TypesPositionTemporaireGet(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}