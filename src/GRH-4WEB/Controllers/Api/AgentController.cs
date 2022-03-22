using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{
    //api/agent/
    public class AgentController : ApiController
    {

        public IEnumerable<agent_synthese> Get(string search = null, string order="alpha", int limit=100)
        {
            IEnumerable<agent_synthese> data = new List<agent_synthese>();
            try
            {
                data = DATACCESS.ModuleAgent.AgentLoad( search,order,limit);
            }
            catch (Exception e)
            {
                BadRequest(e.ToString());
            }
            return data;
        }
    }

}

