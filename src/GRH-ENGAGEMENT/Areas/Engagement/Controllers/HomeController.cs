using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DATACCESS.GENG;
using DATACCESS.GENG.Models;

namespace GRH_ENGAGEMENT.Areas.Engagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Repartition/ProjetEngagement
        public ActionResult Index()
        {
            IEnumerable<projet_engagement> data = DATACCESS.GENG.ServiceEngagement.projetEngagementLoad();
            return View(data);
        }

        // GET: Repartition/ProjetEngagement/Details/5
        public ActionResult Details(long? id, int? tp)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projet_engagement projet_engagement = DATACCESS.GENG.ServiceEngagement.projetEngagementGet(id);
            if (projet_engagement == null)
            {
                return HttpNotFound();
            }
            if (tp != null)
                ViewBag.tp = DATACCESS.GENG.ServiceRepartition.typePriseEnChargeGet((int)tp);

            return View(projet_engagement);
        }

        // GET: Repartition/ProjetEngagement/Create
        public ActionResult Create()
        {
            return View(new projet_engagement { id = 0, designation = "Nouveau dossier" });
        }

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "id,designation")] projet_engagement projet_engagement)
        {
            if (ModelState.IsValid)
            {

                projet_engagement = DATACCESS.GENG.ServiceRepartition.projetEngagementSave(projet_engagement);
                return RedirectToAction("Details", new { id = projet_engagement.id });
            }
            return RedirectToAction("Index");
        }

        // GET: Repartition/ProjetEngagement/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projet_engagement projet_engagement = DATACCESS.GENG.ServiceEngagement.projetEngagementGet(id);
            if (projet_engagement == null)
            {
                return HttpNotFound();
            }
            return View("Create", projet_engagement);
        }

        // GET: Repartition/ProjetEngagement/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projet_engagement projet_engagement = DATACCESS.GENG.ServiceEngagement.projetEngagementGet(id);
            if (projet_engagement == null)
            {
                return HttpNotFound();
            }
            return View(projet_engagement);
        }

        // POST: Repartition/ProjetEngagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            projet_engagement projet_engagement = DATACCESS.GENG.ServiceEngagement.projetEngagementGet(id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Addtp(FormCollection form)
        {
            long id = Convert.ToInt64(form["id"]);
            int tpid = Convert.ToInt32(form["tp_id"]);
            DATACCESS.GENG.ServiceEngagement.projetEngagementAddTp(id, tpid);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveTp(FormCollection form)
        {
            long id = Convert.ToInt64(form["id"]);
            int tpid = Convert.ToInt32(form["tp_id"]);
            DATACCESS.GENG.ServiceEngagement.projetEngagementRemoveTp(id, tpid);
            return RedirectToAction("Details", new { id = id });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDep(FormCollection form)
        {
            long id = Convert.ToInt64(form["id"]);
            int tpid = Convert.ToInt32(form["dep_id"]);
            DATACCESS.GENG.ServiceEngagement.projetEngagementAddDep(id, tpid);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveDep(FormCollection form)
        {
            long id = Convert.ToInt64(form["id"]);
            int tpid = Convert.ToInt32(form["dep_id"]);
            DATACCESS.GENG.ServiceEngagement.projetEngagementRemoveDep(id, tpid);
            return RedirectToAction("Details", new { id = id });
        }

    }
}
