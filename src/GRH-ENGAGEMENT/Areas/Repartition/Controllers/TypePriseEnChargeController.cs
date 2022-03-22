using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_ENGAGEMENT.Areas.Repartition.Controllers
{
    public class TypePriseEnChargeController : Controller
    {
        // GET: Repartition/TypePriseEnCharge
        public ActionResult Index()
        {
            return View(DATACCESS.GENG.ServiceRepartition.typePriseEnChargeLoad());
        }
        public ActionResult Edit(int id)
        { 
            return View(DATACCESS.GENG.ServiceRepartition.typePriseEnChargeGet(id));
        }
        [HttpPost]
        public ActionResult Update(type_prise_en_charge modele)
        {
                DATACCESS.GENG.ServiceRepartition.typePriseEnChargeSave(modele);
            return RedirectToAction("Index");
        }
    }
}