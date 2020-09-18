using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.Validation.Controllers
{
    public class HomeController : Controller
    {
        // GET: Validation/Home
        public ActionResult Index(string id)
        {
             switch (id)
            {
                case "matricule":
                    ViewBag.PageTitle = "MATRICLUES";                  
                    break;
                case "grade":
                    ViewBag.PageTitle = "GRADES DE CARRIERE";
                      break;
                case "emploi":
                    ViewBag.PageTitle = "EMPLOIS";
                     break;
                case "position":
                    ViewBag.PageTitle = "POSITIONS TEMPORAIRES";
                     break;                   
            }
            ViewBag.page = id;
            return View();
        }
        //------------------- validaiton situation agent-----------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidateSituation([Bind(Include = "object_id,agent_id")] v_all_validation entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModuleAgent.SituationAgentValidate(entity.objet_id);
                    TempData["MsgType"] = "success";
                    TempData["Msg"] = "Operation éffectuée avec succès";
                }
                catch (Exception e)
                {
                    TempData["MsgType"] = "danger";
                    TempData["Msg"] = e.ToString();
                }
            } 
            return RedirectToAction("Index", new { id = entity.object_name });
        }
    }
}
