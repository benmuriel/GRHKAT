using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{
    public class StructureController : ApiController
    {
 
        public IEnumerable<v_structure> Get()
        {
            IEnumerable<v_structure> data = new List<v_structure>();
            try
            {
                data = DATACCESS.ModulePlanning.StructurePrincipaleLoad(null);
            }
            catch (Exception e)
            {
                BadRequest(e.ToString());
            }
            return data;
        }
    }
}
