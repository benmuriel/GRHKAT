using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_ENGAGEMENT.Areas.Repartition.Controllers
{
    public class BaremeController : Controller
    {
        // GET: Repartition/Bareme
        public ActionResult Index(int? id)
        {
            type_prise_en_charge data = null;
            if (id != null)
                data = DATACCESS.GENG.ServiceRepartition.typePriseEnChargeGet((int)id);
            return View(data);
        }

        public ActionResult Edit(string grade_id, int tpp_id)
        {
            bareme data = DATACCESS.GENG.ServiceRepartition.baremeGet(grade_id, tpp_id);
            return View(data);
        }

        [HttpPost]
        public ActionResult Create(bareme modele)
        {
            if (modele.montant <= 0)
            {
                TempData["MsgType"] = "red";
                TempData["Msg"] = "Le montant doit être supérieur  à 0 ";
            }
            else
                DATACCESS.GENG.ServiceRepartition.baremeSave(modele);
            return RedirectToAction("Index", new { id = modele.type_prise_en_charge_id });
        }
    }
}