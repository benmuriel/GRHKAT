using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_ENGAGEMENT.Controllers.Api
{
    public class EngagementController : ApiController
    {
        public IEnumerable<engagement> Get(long projet_id, int dept_id, int type_pes_id)
        {
            return DATACCESS.GENG.ServiceEngagement.engagementLoad(projet_id, dept_id, type_pes_id);
        }

        [HttpPost]
        public IHttpActionResult Post(datasubmit form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DATACCESS.GENG.ServiceEngagement.projetEngagementAddEngagement(form.id, form.tpid, form.benids.Trim('-').Split('-'));
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
            return BadRequest("Echec de validation du formulaire");
        }


        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            var data = id.Split('_');
            long projet_id = Convert.ToInt64(data[0]);
            long beneficiaire_id = Convert.ToInt64(data[1]);
            int tpid = Convert.ToInt32(data[2]);
            DATACCESS.GENG.ServiceEngagement.projetEngagementRemoveEngagement(projet_id,beneficiaire_id,tpid);
            return Ok();
        }
    }


    public class datasubmit
    {
        public long id { get; set; }
        public int tpid { get; set; }
        public string benids { get; set; }
    }
}
  
 
