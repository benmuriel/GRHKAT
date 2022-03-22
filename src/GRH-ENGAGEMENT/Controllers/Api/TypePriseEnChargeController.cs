using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_ENGAGEMENT.Controllers.Api
{
    public class TypePriseEnChargeController : ApiController
    {
        public IEnumerable<type_prise_en_charge> Get(long? projet_id = null)
        {
            if (projet_id != null)
                return DATACCESS.GENG.ServiceEngagement.projetEngagementGet(projet_id).Type_Prise_En_Charges.OrderBy(e => e.designation).ToList();
            return DATACCESS.GENG.ServiceRepartition.typePriseEnChargeLoad();
        }
    
    [HttpPost]
    public IHttpActionResult Post(TypePriseEnChargeViewModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                DATACCESS.GENG.ServiceEngagement.projetEngagementAddTp(model.projet_engagement_id, model.id);
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
        public IHttpActionResult Delete (int id, long project_id)
        {
            DATACCESS.GENG.ServiceEngagement.projetEngagementRemoveTp(project_id,id);
            return Ok();
        }

}

    public class TypePriseEnChargeViewModel
    {

        public int id { get; set; }
        public long projet_engagement_id { get; set; }

    }
}
