using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class EmploiController : Controller
    {

        //----------------------- emploi --------------------------------------


        public ActionResult Create(long agent_id)
        {
            EmploiViewModel model = new EmploiViewModel { agent_id = agent_id, situation_id = 0 };
            return View(model);
        }
        public ActionResult Edit(long agent_id)
        {
            var data = DATACCESS.DAO.ModuleAgentSituationAgentEmploiTovalidate(agent_id);

            EmploiViewModel model = new EmploiViewModel
            {
                situation_id = data.id,
                agent_id = data.agent_id,
                poste_id = data.poste_id,
                reference = data.start_reference,
                started_at =  data.started_at.Day+"/"+data.started_at.Month+"/"+data.started_at.Year
            };
            return View("Create", model);
        }
        public ActionResult ToValidate(long agent_id)
        {
            var data = DATACCESS.DAO.ModuleAgentSituationAgentEmploiTovalidate(agent_id);
            ViewBag.agent_id = agent_id;
            return View(data);
        }

        public string PosteServiceName(long id)
        {
            return DATACCESS.ModulePlanning.PostePlanningGet(id).structure;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "situation_id,agent_id,started_at,reference,poste_id")] EmploiViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var date = model.started_at.Split('/');
                    var entity = new v_emploi
                    {
                        id = model.situation_id,
                        agent_id = model.agent_id,
                        started_at = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0])),
                        start_reference = model.reference,
                        poste_id = model.poste_id
                    };
                     DATACCESS.DAO.ModuleAgentSituationAgentEmploiSave(entity);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                }
               else return View("Create", model);
            }
            catch (Exception e)
            {
                TempData["MsgType"] = "danger";
                TempData["Msg"] = e.ToString();
            }
         
          return RedirectToAction("Profil", "Home", new { id = model.agent_id });
        }

        public JsonResult PostesVacantsToJson(short id)
        {
            return Json(DATACCESS.ModulePlanning.PosteVacantLoad(id, null), JsonRequestBehavior.AllowGet);
        }

    }
}