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
    public class PositionTemporairePlannificationController : ApiController
    {

       
        [HttpGet]
        public IHttpActionResult Get( short? str_id = null, int? type_position_id = null , string state ="running" )
        {

            //return Ok( (type_position_id == null)+ " " + (str_id == null));
            return Ok(DATACCESS.ModulePlanning.PlanningPositionTemporaireLoad(type_position_id, str_id, state));
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
