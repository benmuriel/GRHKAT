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
    public class PosteEmploiController : ApiController
    {

        public IEnumerable<v_poste> Get(short id, string state = null, string search = null)
        {
            IEnumerable<v_poste> data = new List<v_poste>();
            try
            {
                data = DATACCESS.ModulePlanning.PosteLoadByStr(id, state, search);
            }
            catch (Exception e)
            {
                BadRequest(e.ToString());
            }
            return data;
        }

        [HttpPost]
        public IHttpActionResult Save(v_poste model)
        {
            try
            {
                DATACCESS.ModulePlanning.PosteSave(model);
                return Ok(DATACCESS.ModulePlanning.StructureGet(model.structure_id));
            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                v_poste entity = DATACCESS.ModulePlanning.PosteGet(id);
                DATACCESS.ModulePlanning.PosteDelete(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Operation éffectuée avec succès");
        }
    }
}
