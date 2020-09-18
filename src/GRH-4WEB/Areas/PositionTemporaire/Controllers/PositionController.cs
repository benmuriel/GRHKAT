using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.PositionTemporaire.Controllers
{
    public class PositionController : Controller
    {

        // GET: PositionTemporaire/Position/Create
        public ActionResult Create(int id)
        {
            type_position data = DATACCESS.ModulePlanning.TypesPositionTemporaireGet(id);
            if (data != null)
            {
                PositionTemporaireViewModel model = new PositionTemporaireViewModel
                {
                    has_poste = data.has_poste,
                    has_institution = data.has_institution,
                    designation = data.designation,
                    situation_id = 0,
                    type_position_id = id,
                    started_at = DateTime.Today.ToString("d/M/yyyy")
                };
                 ViewBag.str = DATACCESS.ModulePlanning.StructureGet(Convert.ToInt16(Request.QueryString["str"]));
                return View(model);
            }
            return RedirectToAction("Index", "Home", new { area = "PositionTemporaire", id = -2, str = 0 });
        }

        // POST: PositionTemporaire/Position/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "situation_id,designation,agent_id,poste_id,institution_detachement_id,type_position_id,duree,ended_at,started_at,reference")] PositionTemporaireViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var date = model.started_at.Split('/');
                    var end = model.ended_at.IndexOf('/') > 0 ? model.ended_at.Split('/') : null;

                    var entity = new v_position_temporaire
                    {
                        id = model.situation_id,
                        agent_id = model.agent_id,
                        started_at = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0])),
                        ended_at = (end == null ? null : (DateTime?)new DateTime(Int32.Parse(end[2]), Int32.Parse(end[1]), Int32.Parse(end[0]))),
                        start_reference = model.reference,
                        type_position_id = model.type_position_id,
                        details = model.details,
                        poste_id = model.poste_id,
                        institution_detachement_id = model.institution_detachement_id,
                        lieu_position_adresse = model.lieu_position_adresse
                    };

                    DATACCESS.ModuleAgent.SituationAgentPositionSave(entity);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                }
                else
                {
                    ViewBag.str = DATACCESS.ModulePlanning.StructureGet(Convert.ToInt16(Request.QueryString["str"]));
                    return View("Create", model);
                }
            }
            catch (Exception e)
            {
                TempData["MsgType"] = "alert";
                TempData["Msg"] = e.ToString();
            }
            return RedirectToAction("Index", "Home", new { area = "PositionTemporaire", id = -2, str = 0 });
        }


        // GET: PositionTemporaire/Position/Edit/5
        public ActionResult Edit(long id)
        {
            v_position_temporaire data = DATACCESS.ModuleAgent.PositionTemporaireGet(id);

            PositionTemporaireViewModel model = new PositionTemporaireViewModel
            {
                situation_id = data.id,
                agent_id = data.agent_id,
                type_position_id = data.type_position_id,
                designation = data.type_position,
                duree = data.duree,
                ended_at = data.ended_at_string,
                reference = data.start_reference,
                started_at = data.started_at_string,
                details = data.details,
                has_poste = data.poste_id != null,
                has_institution = data.institution_detachement_id != null,
                lieu_position_adresse = data.lieu_position_adresse
            };
            var agent = DATACCESS.ModuleAgent.AgentGet(data.agent_id);
            ViewBag.str = DATACCESS.ModulePlanning.StructureGet((short)agent.service_id);
            return View("Create", model);
        }

        // GET: PositionTemporaire/Position/Delete/5
        public ActionResult Delete(int id)
        {
            v_position_temporaire data = DATACCESS.ModuleAgent.PositionTemporaireGet(id);

            return View("~/Areas/ProfilAgent/Views/Home/_SituationDeletePartial.cshtml", data);
        }

        // POST: PositionTemporaire/Position/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public JsonResult PositionDuree(int id, int type_position_id)
        {
            return Json(DATACCESS.ModuleAgent.DureePosition(id, type_position_id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Terminate(int id)
        {
            v_position_temporaire data = DATACCESS.ModuleAgent.PositionTemporaireGet(id);

            return View("Terminate", data);
        }
        // POST: PositionTemporaire/Position/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Terminate(int id, FormCollection collection)
        {
            v_position_temporaire entity = DATACCESS.ModuleAgent.PositionTemporaireGet(id);
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModuleAgent.SituationAgentPositionTerminate(entity.id);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Enregistrement éffectué avec succès. L'element a été placé en attente de validation";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "alert";
                    TempData["Msg"] = e.ToString();
                }
            }
            return RedirectToAction("Index", "Home", new { area = "PositionTemporaire", id = entity.type_position_id, str = entity.service_id });
        }
    }
}
