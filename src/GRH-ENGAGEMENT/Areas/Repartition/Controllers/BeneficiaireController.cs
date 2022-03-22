using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_ENGAGEMENT.Areas.Repartition.Controllers
{
    public class BeneficiaireController : Controller
    {
        // GET: Repartition/Beneficiaire
        public ActionResult Index(int page = 1)
        {
            var data = DATACCESS.GENG.ServiceRepartition.beneficiaireLoad(page);
            ViewBag.page = page;
            return View(data);
        }

        public ActionResult Detail(long id)
        {
            beneficiaire data = DATACCESS.GENG.ServiceRepartition.beneficiaireGet(id);
            return View(data);
        }
        public ActionResult Historique(long id)
        {
            beneficiaire data = DATACCESS.GENG.ServiceRepartition.beneficiaireGet(id);
            return View(data);
        }
    }
}