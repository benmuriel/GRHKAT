using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GRH_4WEB.Areas.PositionTemporaire.Controllers
{
    public class HomeController : Controller
    {
        // GET: PositionTemporaire
        public ActionResult Index(int? id)
        { 
            string str_id =  Request.QueryString["str"];
            string etat = Request.QueryString["e"];
            short str = 0;
            Int16.TryParse(str_id, out str);

             List<v_position_temporaire> datas = new List<v_position_temporaire>();

            if (id != null)
            {
                if (id == 0)
                {
                    ViewBag.PageTitle = "SITUATION EN COURS";
                    datas = DATACCESS.ModuleAgent.PositionTemporaireLoad(null, "running", str, null);
                }
                else if(id== -1){
                    ViewBag.PageTitle = "ARCHIVES";
                    datas = DATACCESS.ModuleAgent.PositionTemporaireLoad(null, "passed", str, null);
                }
                else if(id== -2){
                    ViewBag.PageTitle = "EN ATTENTE DE VALIDATION";
                    datas = DATACCESS.ModuleAgent.PositionTemporaireLoad(null, "tovalidate", str, null);
                }
                else
                {
                    ViewBag.PageTitle = DATACCESS.ModulePlanning.TypesPositionTemporaireGet((int)id).designation;
                    datas = DATACCESS.ModuleAgent.PositionTemporaireLoad(null, "running", str, id);
                }
            }
           
            ViewBag.str = str_id;
            ViewBag.id = id; 
            return View(datas);
        }

        //public ActionResult CurrentPosition()
        //{
        //    return DATACCESS.ModuleAgent.PositionTemporaireCurrentPositionLoad(null, str, id);
        //}
    }
}