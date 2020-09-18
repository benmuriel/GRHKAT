using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class HomeController : Controller
    {
        // GET: ProfilAgent
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
                string profil = Request.QueryString["profil"];
               
                    switch (profil)
                    {
                        case "sans_emploi":
                            ViewBag.pagetitle = "Pofils sans emploi";
                            break;
                        default:
                            profil = "avec_emploi";
                            ViewBag.pagetitle = "Pofils avec emploi";
                            break;
                    }
                    ViewBag.profil = profil;
            }
            return View();
        }
        public ActionResult ProfilSynthese(long id)
        {
            var data = DATACCESS.ModuleAgent.AgentGet(id);
            return View(data);
        }

        public ActionResult Profil(long id)
        {
            string page = Request.QueryString["page"];

            switch (page)
            {
                case "etudes_faites":
                case "charge_sociale":
                case "identification":
                case "position_temporaire":
                    ViewBag.page = page;
                    break;
                default:
                    ViewBag.page = "home";
                    break;
            }
            agent_synthese data = null;
            try
            {
                data = DATACCESS.ModuleAgent.AgentGet(id);
            }
            catch (Exception e)
            {
                TempData["MsgType"] = "danger";
                TempData["Msg"] = e.ToString();
                return RedirectToAction("Index");
            }
            if (data == null)
            {
                TempData["MsgType"] = "danger";
                TempData["Msg"] = "Profil inexistant";
                return RedirectToAction("Index");
            }
            return View(data);
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
                TempData["MsgType"] = "danger";
                TempData["Msg"] = e.ToString();
            }

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "agent_id,nom,post_nom,prenom,genre,lieu_naissance," +
            "date_naissance,nationalite,etat_civil,mail_personel_1,mail_personel_2,id_pointage,telepone_personel_1,telephone_personel_2")]
        agent_synthese entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.DAO.ModuleAgentAgentSave(entity);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Enregistrement éffectué avec succès";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "danger";
                    TempData["Msg"] = e.ToString();
                }
            }
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id, page = "identification" });

        }

      
        //-------------------delete situation agent-----------------------------


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSituation([Bind(Include = "id,agent_id,type_situation")] situation_agent entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.DAO.ModuleAgentSituationAgentDelete(entity.id);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Operation éffectuée avec succès";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "danger";
                    TempData["Msg"] = e.ToString();
                }
            }
            if (entity.type_situation.Trim() == "position")
                return RedirectToAction("Index", "Home", new { area = "PositionTemporaire", id = -2, str = 0 });
            return RedirectToAction("Profil", "Home", new { id = entity.agent_id });
        }
    }
}