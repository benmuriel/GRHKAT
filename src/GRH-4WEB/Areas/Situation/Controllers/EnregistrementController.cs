using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.Situation.Controllers
{
    public class EnregistrementController : Controller
    {
        // GET: Situation/Enregistrement
        public ActionResult Index()
        {
            string search = Request.QueryString["q"];

            if (!String.IsNullOrEmpty(search))
            {
                ViewBag.pagetitle = "Resultat de la recherche";
                ViewBag.q = search;
            }
            else
            {
                ViewBag.pagetitle = "Enregistrements";
            }
            return View();
        }

        //------------------Edition profil ------------------------


        public ActionResult Create(long? id)
        {
            agent_synthese data = null;
            try
            {
                data = id == null ? new agent_synthese() : DATACCESS.ModuleAgent.AgentGet(id);
            }
            catch (Exception e)
            {
                TempData["MsgType"] = "red";
                TempData["Msg"] = e.Message;
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(agent_synthese entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    entity = DATACCESS.ModuleAgent.ModuleAgentAgentSave(entity);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "red";
                    TempData["Msg"] = e.Message;
                }
            }
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id, page = "identification" });
        }
    }
}