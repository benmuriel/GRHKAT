using DATACCESS;
using DATACCESS.ViewModels;
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
            var data = DAO.ModuleAgentChargeSocialeLoad(id);
            return View(data);
        }

        // GET: ChargeSociale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChargeSociale/Create
        public ActionResult Create(long agent_id)
        {
            return View(new ChargeSocialViewModel
            {
                id = 0,
                agent_id = agent_id,
                genre = "H",
                affinite = "conjoint",
                etat = true,
                date_naissance = DateTime.Today
            });
        }

        // POST: ChargeSociale/Create
        [System.Web.Mvc.HttpPost]
        public ActionResult Create([FromBody] ChargeSocialViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.DAO.ModuleAgentChargeSocialeSave(model);

                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                    return RedirectToAction("Profil", "Home", new { id = model.agent_id, page = "charge_sociale" });
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "alert";
                    TempData["Msg"] = e.ToString();
                }

            }
            return View(model);
        }

        // GET: ChargeSociale/Edit/5
        public ActionResult Edit( long id)
        {
            var item = DAO.ModuleAgentChargeSocialeGet(id);
            if (item == null)
            {
                TempData["MsgType"] = "alert";
                TempData["Msg"] = "";
                return RedirectToAction("Index", "Home");
            }

            return View("Create",
                new ChargeSocialViewModel
                {
                    id = item.id,
                    agent_id = item.agent_id,
                    nom = item.nom,
                    post_nom = item.post_nom,
                    prenom = item.prenom,
                    genre = item.genre,
                    affinite = item.affinite,
                    date_naissance = item.date_naissance,
                    lieu_naissance = item.lieu_naissance,
                    etat = item.etat,
                    occupation = item.occupation
                });
        }

        // POST: ChargeSociale/Edit/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(long id, [FromBody] ChargeSocialViewModel model)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ChargeSociale/Delete/5
        public ActionResult Delete(long id)
        {
            return View(DAO.ModuleAgentChargeSocialeGet(id));
        }

        // POST: ChargeSociale/Delete/5
        [System.Web.Mvc.HttpPost]
        public ActionResult Delete(long id, FormCollection collection)
        {
            try
            {
                DAO.ModuleAgentChargeSocialeDelete(id);

                TempData["MsgType"] = "success";
                TempData["Msg"] = "Enregistrement éffectué avec succès";
           }
            catch(Exception e)
            {
                TempData["MsgType"] = "alert";
                TempData["Msg"] = e.ToString();
            }
            return RedirectToAction("Profil", "Home", new { id = collection["agent_id"], page = "charge_sociale" });
     
        }
    }
}
