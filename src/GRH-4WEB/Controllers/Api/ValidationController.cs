using DATACCESS.Models;
using GRH_4WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{
    public class ValidationController : ApiController
    {
        public IEnumerable<v_position_temporaire> Get(short id, string profil=null, long? agent_id=null)
        {
            List<v_position_temporaire> data = new List<v_position_temporaire>(); 
            data = DATACCESS.ModuleAgent.WaitingValidationLoad(id, profil, agent_id);
            return data;
        }

        public IHttpActionResult Post(ValidationViewModel entity)
        {
            if (ModelState.IsValid)
            {
                try
                {                  
                    DATACCESS.ModuleAgent.SituationAgentValidate(entity.id, entity.request_for);
                }
                catch (Exception e)
                {
                    throw(e);
                }
                return Ok("Operation éffectuée avec succès");
            }
            return BadRequest("Saisie incorrect. Verifiez vos données");
        }
    }
}
