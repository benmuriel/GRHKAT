using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class PositionTemporaireController : Controller
    {
        public ActionResult Index(long id)
        {
            ViewBag.agent_id = id;
            var data = DATACCESS.ModuleAgent.PositionTemporaireLoad(id, null, null, null);
            return View(data);
        }
        public ActionResult ToValidate(long id)
        {
            var data = DATACCESS.ModuleAgent.PositionTemporaireGet(id);
            return View(data);
        }
        public ActionResult Create(long id, long agent_id, int? motifid = null)
        {
            bool has_error = false;
            var type_position = DATACCESS.ModulePlanning.TypesPositionTemporaireGet(id);
            var agent = DATACCESS.ModuleAgent.AgentGet(agent_id);
            if (has_error = (type_position == null || agent == null))
            {
                TempData["Msg"] = "Type de position ou agent inexistant. ";
                TempData["MsgType"] = "red";
            }
            // verifier si l'agent n a pas cette position en cours ou en attente de validation

            if (has_error = DATACCESS.ModuleAgent.PositionTemporaireLoad(agent_id, "running-tovalidate_start-tovalidate_end-comming", null, id).Count > 0)
            {
                TempData["Msg"] = "Ce agent possède deja cette position en cours,à venir ou en attente de validation ";
                TempData["MsgType"] = "red";
            }
          
            if (has_error)
            {
                return RedirectToAction("Profil", "Home", new { id = agent_id, page = "position_temporaire" });
            }

            v_position_temporaire position_vm = new v_position_temporaire { id = 0, agent_id = agent_id, type_position_id = id, started_at = DateTime.Now };
            SitualtionViewModel model = new SitualtionViewModel { motif_type_position_id = motifid, };

            model = FillFeildViewModel(position_vm, model);
            if (type_position.is_computed_value)
            {
                string source = DATACCESS.ModuleAgent.AgentGet(agent_id).nom_complet;
                model.value_data = DATACCESS.DAO.UniqueRamdonString(source,"AA");
            }
            return View(model);
        }
        public ActionResult Edit(long id)
        {
            v_position_temporaire pos = DATACCESS.ModuleAgent.PositionTemporaireGet(id);

            if (pos == null)
            {
                TempData["Msg"] = "Objet inexistant ";
                TempData["MsgType"] = "red";
                return RedirectToAction("Index", "Home");
            }

            SitualtionViewModel model = null;

            model = FillFeildViewModel(pos, model);

            return View("Create", model);
        }

        private SitualtionViewModel FillFeildViewModel(v_position_temporaire position, SitualtionViewModel model)
        {
            if (model == null)
                model = new SitualtionViewModel();
            
            model.duree = position.duree;
            if (model.type == null)
            { 
                model.type = DATACCESS.ModulePlanning.TypesPositionTemporaireGet(position.type_position_id);
                model.duree =  model.duree_max = model.type.duree_max;
                model.duree_min = model.type.duree_min;
            }
            model.agent_id = position.agent_id;
            model.type_position_id = position.type_position_id;
            model.situation_id = position.id;
            model.started_at = position.started_at.ToString("d/M/yyyy");
            model.ended_at = position.ended_at != null ? position.ended_at.Value.ToString("d/M/yyyy") : "";
            model.value_data = position.value_data;
            model.lieu_realisation_position_id = position.lieu_realisation_position_id;
            model.planning_project_id = position.planning_project_id;
          
            model.start_reference = position.start_reference;
            model.details = position.details;
            if (model.motif_type_position_id != null)
            {
                var motif = DATACCESS.ModulePlanning.TypesPositionTemporaireMotifGet((int)(model.motif_type_position_id));
                if (motif != null)
                {
                    model.motif_type_position = motif.designation;
                    model.duree_min = motif.duree_min;
                    model.duree = model.duree_max = motif.duree_max;
                    model.value_data = motif.id.ToString();
                }
            }

            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(SitualtionViewModel model)
        {
            model.type = DATACCESS.ModulePlanning.TypesPositionTemporaireGet(model.type_position_id);
            var position = DATACCESS.ModuleAgent.PositionTemporaireGet(model.situation_id);
            position = FillFeildEntity(position, model);
           // try {

              
                bool has_error = false;
                // requirement
                if (has_error = position.value_data == null)
                {
                    TempData["Msg"] = "Une déscription de la situation agent est réquise";
                    TempData["MsgType"] = "red";
                }
                if (model.type.require_planning && (has_error = model.planning_project_id == null))
                {
                    TempData["Msg"] = "Une plannification est requise pour ce type de situation agent";
                    TempData["MsgType"] = "red";
                }
                if (model.type.require_realisation_lieu && (has_error = model.lieu_realisation_position_id == null))
                {
                    TempData["Msg"] = "Un lieu de réalisation  est requis pour ce type de situation agent";
                    TempData["MsgType"] = "red";
                }
                if (ModelState.IsValid &&!has_error)//
                {

                    DATACCESS.ModuleAgent.SituationAgentPositionSave(position);
                    TempData["MsgType"] = "green";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                }
                else
                {
                    model = FillFeildViewModel(position, model);
                    return View("Create", model);
                }
            //}
            //catch (Exception e)
            //{
            //    TempData["MsgType"] = "red";
            //    TempData["Msg"] = e.Message;

            //    model = FillFeildViewModel(position, model);
            //    return View("Create", model);
            //}
            return RedirectToAction("Profil", "Home", new { id = model.agent_id, page = "position_temporaire" });
        }

        private v_position_temporaire FillFeildEntity(v_position_temporaire position, SitualtionViewModel model)
        {
            if (position == null)
                position = new v_position_temporaire { agent_id = model.agent_id, type_position_id = model.type_position_id, id = model.situation_id };
            var date = model.started_at.Split('/');
            var end = model.ended_at != null && model.ended_at.IndexOf('/') > 0 ? model.ended_at.Split('/') : null;

            position.started_at = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
            position.ended_at = (end == null ? null : (DateTime?)new DateTime(Int32.Parse(end[2]), Int32.Parse(end[1]), Int32.Parse(end[0])));
            position.value_data = model.value_data;
            position.lieu_realisation_position_id = model.lieu_realisation_position_id;
            position.planning_project_id = model.planning_project_id;
            position.duree = model.duree;
            position.start_reference = model.start_reference;
            position.details = model.details;

            return position;
        }


        // POST: PositionTemporaire/Position/Delete/5
        [HttpPost]
        public ActionResult Delete(v_position_temporaire entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModuleAgent.SituationAgentDelete(entity.id);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Operation éffectuée avec succès";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "green";
                    TempData["Msg"] = e.ToString();
                }
            }
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id, page = "position_temporaire" });
        }
        // POST: PositionTemporaire/Position/Delete/5
        [HttpPost]
        public ActionResult Validate(v_position_temporaire entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModuleAgent.SituationAgentValidate(entity.id, "start");
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Operation éffectuée avec succès";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "green";
                    TempData["Msg"] = e.ToString();
                }
            }
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id, page = "position_temporaire" });
        }
        [HttpGet]
        public JsonResult PositionDuree(int id, int type_position_id)
        {
            return Json(DATACCESS.ModuleAgent.DureePosition(id, type_position_id), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdatePriseEnCharge(long id)
        {
            var entity = DATACCESS.ModuleAgent.PositionTemporaireGet(id);
            if (entity == null)
            {
                return RedirectToAction("Index", "Home");
            }
            DATACCESS.ModuleAgent.SituationAgentUpdateTraitementSocial(id);
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id });
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

            try
            {
                DATACCESS.ModuleAgent.SituationAgentPositionTerminate(entity.id);
                TempData["MsgType"] = "green";
                TempData["Msg"] = "Enregistrement éffectué avec succès.";
            }
            catch (Exception e)
            {
                TempData["MsgType"] = "alert";
                TempData["Msg"] = e.ToString();
            }

            return RedirectToAction("Profil", "Home", new { id = entity.agent_id, page = "position_temporaire" });
        }
    }
}