using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{
    public class FonctionController : ApiController
    {
        public IEnumerable<fonction> Get()
        {
            IEnumerable<fonction> data = new List<fonction>();
            try
            {
                data = DATACCESS.ModulePlanning.FonctionLoad(null);
            }
            catch (Exception e)
            {
                BadRequest(e.ToString());
            }
            return data;
        }
    }
}
