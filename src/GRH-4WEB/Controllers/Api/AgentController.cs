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

        public IEnumerable<agent_synthese> Get(string search, string profil)
        {
            IEnumerable<agent_synthese> data = null;
            if (!String.IsNullOrEmpty(search))
            {
                try
                {
                    data = DATACCESS.ModuleAgent.AgentLoad(search);
                }
                catch (Exception e)
                {
                    BadRequest(e.ToString());
                }
            }
            else
            {
                try
                {
                    switch (profil)
                    {
                        case "sans_emploi":
                            data = DATACCESS.ModuleAgent.AgentLoad("").Where(e => e.emploi_id == null).ToList();
                            break;
                        default:
                            profil = "avec_emploi";
                            data = DATACCESS.ModuleAgent.AgentLoad("").Where(e => e.emploi_id != null).ToList();
                            break;
                    }
                }
                catch (Exception e)
                {
                    BadRequest(e.ToString());
                }
            }
            return data;
        }
    }

}

