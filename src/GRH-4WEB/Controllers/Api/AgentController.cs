using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{

    public class AgentController : ApiController
    {

        public IEnumerable<agent_synthese> Get(short? id = null, string profil="avec_emploi", string search = "")
        {
            IEnumerable<agent_synthese> data = null;
                try
                {
                    data = DATACCESS.ModuleAgent.AgentLoad(id,profil,search);
                }
                catch (Exception e)
                {
                    BadRequest(e.ToString());
                }
            return data;
        }
    }

}

