using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class GradeCarriereController : Controller
    {
        public ActionResult ToValidate(long agent_id)
        {
            var data = DATACCESS.DAO.ModuleAgentSituationAgentGradeCarriereTovalidate(agent_id);
            ViewBag.agent_id = agent_id;
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "situation_id,agent_id,grade_id,started_at,reference")] GradeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var date = model.started_at.Split('/');
                    var entity = new grade_carriere
                    {
                        id = model.situation_id,
                        agent_id = model.agent_id,
                        started_at = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0])),
                        start_reference = model.reference,
                        grade_id = model.grade_id
                    };
                    DATACCESS.ModuleAgent.SituationAgentGadeCarriereSave(entity);
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


        public ActionResult Create(long agent_id)
        {
            GradeViewModel model = new GradeViewModel { agent_id = agent_id, situation_id = 0 };
            return View(model);
        }
        public ActionResult Edit(long agent_id)
        {
            var data = DATACCESS.DAO.ModuleAgentSituationAgentGradeCarriereTovalidate(agent_id);

            GradeViewModel model = new GradeViewModel
            {
                situation_id = data.id,
                agent_id = data.agent_id,
                grade_id = data.grade_id,
                reference = data.start_reference,
                started_at =  data.started_at.Day+"/"+data.started_at.Month+"/"+data.started_at.Year
            };
            return View("Create", model);
        } 
     

    }
}