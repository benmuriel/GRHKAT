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
        public IEnumerable<v_all_validation> Get(string id)
        {
            List<v_all_validation> data = new List<v_all_validation>();
            switch (id)
            {
                case "matricule":

                    data = DATACCESS.ModuleAgent.WaitingValidationLoad(null, "matricule");
                    break;
                case "grade":

                    data = DATACCESS.ModuleAgent.WaitingValidationLoad(null, "grade");
                    break;
                case "emploi":

                    data = DATACCESS.ModuleAgent.WaitingValidationLoad(null, "emploi");
                    break;
                case "position":
                    data = DATACCESS.ModuleAgent.WaitingValidationLoad(null, "position");
                    break;
            }
            return data;
        }

        public IHttpActionResult Post(ValidationViewModel entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModuleAgent.SituationAgentValidate(entity.objet_id);
                }
                catch (Exception e)
                {
                    return Ok(e.ToString());
                }
                return Ok("Operation éffectuée avec succès");
            }
            return BadRequest("Saisie incorrect. Verifiez vos données");
        }
    }
}
