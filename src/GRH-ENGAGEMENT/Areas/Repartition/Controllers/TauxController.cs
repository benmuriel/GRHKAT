using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_ENGAGEMENT.Areas.Repartition.Controllers
{
    public class TauxController : Controller
    {
        // GET: Repartition/Taux
        public ActionResult Index()
        {
            return View(DATACCESS.GENG.ServiceRepartition.tauxChangeLoad("USD"));
        }

        [HttpGet]
        public ActionResult Create()
        {
            taux_change current = DATACCESS.GENG.ServiceRepartition.tauxChangeGet("USD");
            return View(new taux_change { devise_id = "USD", valeur = current.valeur } );
        }

         [HttpPost]
        public ActionResult Create(taux_change modele)
        {
            if (modele.valeur <= 0)
            {
                TempData["MsgType"] = "red";
                TempData["Msg"] = "Le montant doit être supérieur  à 0 ";
            }
            else
                DATACCESS.GENG.ServiceRepartition.tauxChangeSave(modele);
             return RedirectToAction("Index");
        }


    }
}