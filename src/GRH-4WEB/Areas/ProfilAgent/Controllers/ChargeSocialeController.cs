using DATACCESS;
using DATACCESS.Models;
using DATACCESS.ViewModels;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class ChargeSocialeController : Controller
    {
        // GET: ChargeSociale
        public ActionResult Index(long id)
        {
            ViewBag.agent_id = id;
            var data = ModuleAgent.ChargeSocialeLoad(id);
            return View(data);
        }

        // GET: ChargeSociale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChargeSociale/Create
        public ActionResult Create(int id,long agent_id)
        {
            type_affinite_charge_sociale type = DATACCESS.ModulePlanning.TypeAffiniteChargeSocialGet(id);
            bool haserror = false;
            if(haserror = type == null)
            {
                TempData["MsgType"] = "red";
                TempData["Msg"] = "Type d'affinité incorrect"; 
            }
            int count = DATACCESS.ModuleAgent.CountElement(agent_id,type.id,"charge_sociale");
            if(haserror = type.occurence == count)
            {
                TempData["MsgType"] = "red";
                TempData["Msg"] = "Le quota pour ce type d'affinité est atteint"; 
            }
            if(haserror)
            return RedirectToAction("Profil", "Home", new { id = agent_id, page = "charge_sociale" });
            ChargeSocialViewModel model = new ChargeSocialViewModel {  type_affinite_charge_sociale = type.designation};
            model = FillFieldModel(model, new v_charge_sociale { agent_id = agent_id, id = 0, date_naissance = DateTime.Now , type_affinite_charge_sociale_id = type.id });
            return View( model);
        }

        // POST: ChargeSociale/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create(ChargeSocialViewModel model)
        {
            if (ModelState.IsValid)
            {
                //try   {
                    v_charge_sociale entity = null;
                    entity = FillFieldEntity(entity, model);

                    DATACCESS.ModuleAgent.ChargeSocialeSave(entity);

                    TempData["MsgType"] = "green";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                //}
                //catch (Exception e)
                //{
                //    TempData["MsgType"] = "red";
                //    TempData["Msg"] = e.ToString();
                //}
                return RedirectToAction("Profil", "Home", new { id = model.agent_id, page = "charge_sociale" });

            }
            return View(model);
        }

        private v_charge_sociale FillFieldEntity(v_charge_sociale entity, ChargeSocialViewModel model)
        {
            if (entity == null)
                entity = new v_charge_sociale();
            var date = model.date_naissance != null && model.date_naissance.IndexOf('/') > 0 ? model.date_naissance.Split('/') : null;

            entity.id = model.charge_id;
            entity.agent_id = model.agent_id;
            entity.nom = model.nom;
            entity.post_nom = model.post_nom;
            entity.prenom = model.prenom;
            entity.date_naissance = new DateTime(Int32.Parse(date[2]), Int32.Parse(date[1]), Int32.Parse(date[0]));
            entity.lieu_naissance = model.lieu_naissance;
            entity.type_affinite_charge_sociale_id = model.type_affinite_charge_sociale_id;
            entity.genre = model.genre;
            entity.occupation = model.occupation;
             return entity;
        }

        // GET: ChargeSociale/Edit/5
        public ActionResult Edit(long id)
        {
            v_charge_sociale item = DAO.ModuleAgentChargeSocialeGet(id);
            if (item == null)
            {
                TempData["MsgType"] = "alert";
                TempData["Msg"] = "";
                return RedirectToAction("Index", "Home");
            }

            ChargeSocialViewModel model = null;

            model = FillFieldModel(model, item);
            type_affinite_charge_sociale type = DATACCESS.ModulePlanning.TypeAffiniteChargeSocialGet(item.type_affinite_charge_sociale_id);
            model.type_affinite_charge_sociale_id = type.id;
            model.type_affinite_charge_sociale = type.designation;
            return View("Create", model);

        }

        private ChargeSocialViewModel FillFieldModel(ChargeSocialViewModel model, v_charge_sociale entity)
        {
            if (model == null)
                model = new ChargeSocialViewModel();
            model.charge_id = entity.id;
            model.agent_id = entity.agent_id;
            model.nom = entity.nom;
            model.post_nom = entity.post_nom;
            model.prenom = entity.prenom;
            model.date_naissance = entity.date_naissance.ToString("d/M/yyyy");
            model.lieu_naissance = entity.lieu_naissance;
            model.type_affinite_charge_sociale_id = entity.type_affinite_charge_sociale_id;
            model.genre = entity.genre;
            model.occupation =entity.occupation;
        
            return model;
        }

      
     

        // POST: ChargeSociale/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(ChargeSocialViewModel entity)
        {
            try
            {
                ModuleAgent.ChargeSocialeDelete(entity.charge_id);

                TempData["MsgType"] = "success";
                TempData["Msg"] = "Enregistrement éffectué avec succès";
            }
            catch (Exception e)
            {
                TempData["MsgType"] = "alert";
                TempData["Msg"] = e.ToString();
            }
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id, page = "charge_sociale" });

        }
    }
}
