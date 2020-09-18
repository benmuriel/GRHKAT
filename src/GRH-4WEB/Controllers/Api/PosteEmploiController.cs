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
        public IHttpActionResult Save(PosteViewModel model)
        { 
            try
            {
                if (ModelState.IsValid)
                {
                    var item = DATACCESS.ModulePlanning.PosteSave(new v_poste
                    {
                        id = model.id,
                        structure_id = model.structure_id,
                        fonction_id = model.fonction_id,
                        designation = model.designation
                    });
                    return Ok(item);  
                }                
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return BadRequest("Echec de validation du formulaire");
        }

        public IHttpActionResult Delete(v_poste entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DATACCESS.ModulePlanning.PosteDelete(entity);
                }
                catch (Exception e)
                {
                  return  BadRequest(e.Message);
                }
                return Ok("Operation éffectuée avec succès");
            }
          return  BadRequest("Echec de validation du formulaire");
        }
    }
}
