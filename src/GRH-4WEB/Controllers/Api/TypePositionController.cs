using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{

    public class TypePositionController : ApiController
    {

        public IHttpActionResult Get(bool? require_planning = null, string categorie = null)//IEnumerable<type_position>
        {
            IEnumerable<type_position> data = new List<type_position>();
            try
            {
                data = DATACCESS.ModulePlanning.TypesPositionTemporaire(require_planning, categorie);
        }
            catch (Exception e)
            {
                BadRequest(e.ToString());
    }
            return Json(data);
        }
    }

}

