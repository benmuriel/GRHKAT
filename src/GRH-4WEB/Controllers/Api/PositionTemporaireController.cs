using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{

    //api/
    public class PositionTemporaireController : ApiController
    {

       
        [HttpGet]
        public IHttpActionResult Get(long?id=null, long? agent_id= null, string state=null, long? type_id=null)
        {
            if (id != null)
                return Json(DATACCESS.ModuleAgent.PositionTemporaireGet((long)id));
            return Ok(DATACCESS.ModuleAgent.PositionTemporaireLoad(agent_id, state, null, type_id));
        }

        public IHttpActionResult Post(v_position_temporaire entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModuleAgent.SituationAgentPositionSave(entity);
                }
                catch (Exception e)
                {
                    throw (e);
                }
                return Ok("Operation éffectuée avec succès");
            }
            return BadRequest("Saisie incorrect. Verifiez vos données");
        }
    }
}
