using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent.Controllers
{
    public class PositionTemporaireController : Controller
    {
        public ActionResult ToValidate(long id)
        {
            var data = DATACCESS.ModuleAgent.PositionTemporaireGet(id); 
            return View(data);
        }
        public ActionResult Create(long agent_id)
        {
            PositionTemporaireViewModel model = new PositionTemporaireViewModel {
                started_at = DateTime.Today.ToString("d/M/yyyy"),
                agent_id = agent_id, situation_id = 0 };
            return View(model);
        }

     
    }
}