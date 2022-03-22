using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.PositionTemporaire.Controllers
{
    public class PlannificationController : Controller
    {
        // GET: PositionTemporaire/Plannification
        public ActionResult Index()
        {
           
            return View();
        }
        public ActionResult Create()
        {
            return View(new planning_project { });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(planning_project entity)
        {
            if (ModelState.IsValid)
            {
                DATACCESS.ModuleAgent.PositionTemporairePlanningProjectSave(entity);
                TempData["MsgType"] = "green";
                TempData["Msg"] = "Enregistrement éffectué avec succès";

                return RedirectToAction("Detail", new { id = entity.id });
            }
            return View(entity);
        }
        [HttpPost]
        public ActionResult Lock(FormCollection form)
        {
            var entity_id = Convert.ToInt64(form["id"]);
           // return Json(entity_id);
            DATACCESS.ModuleAgent.PositionTemporairePlanningProjectLock(entity_id);
            TempData["MsgType"] = "green";
            TempData["Msg"] = "Enregistrement éffectué avec succès";

            return RedirectToAction("Detail", new { id = entity_id });
        }

        public ActionResult Detail(long id)
        {
            planning_project entity = DATACCESS.ModuleAgent.PositionTemporairePlanningProjectGet(id);
            return View(entity);
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            planning_project entity = DATACCESS.ModuleAgent.PositionTemporairePlanningProjectGet(id);
            if (entity == null)
            {
                return View("Index");
            }
            entity.selected_structures = entity.structures.Select(e => e.id).ToArray();
            return View("Create", entity);
        }
    }
}