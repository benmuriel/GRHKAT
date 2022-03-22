using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_ENGAGEMENT.Controllers.Api
{
    public class EligibiliteController : ApiController
    {
        public IEnumerable<eligibilite_prise_en_charge> Get(int tp, int dept = 0,  string search = "")
        {
            IEnumerable<eligibilite_prise_en_charge> data = new List<eligibilite_prise_en_charge>();
            try
            {
                data = DATACCESS.GENG.ServiceRepartition.eligibiliteValideLoad(tp,dept, search);
            }
            catch (Exception e)
            {
                BadRequest(e.ToString());
            }
            return data;
        }
    }
}
