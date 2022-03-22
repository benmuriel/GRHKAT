using DATACCESS.Models;
using System;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class HomeController : Controller
    {
        // GET: ProfilAgent
      
        public ActionResult ProfilSynthese(long id)
        {
            var data = DATACCESS.ModuleAgent.AgentGet(id);
            return PartialView("_EtatServicePartial",data);
        }
        public ActionResult Traitement(long id)
        {
            var data = DATACCESS.ModuleAgent.SituationAgentTraitementParAgent(id);
            return PartialView("_Traitement", data);
        }
        public ActionResult Profil(long id)
        {
            string page = Request.QueryString["page"];

            switch (page)
            {
                case "loa":
                case "charge_sociale":
                case "identification":
                case "traitement":
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
                TempData["MsgType"] = "red";
                TempData["Msg"] = e.Message;
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
    }
}