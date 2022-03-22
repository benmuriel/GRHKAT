using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace GRH_4WEB.Controllers.Api
{
    public class GradeController : ApiController
    {
        public IEnumerable<grade> Get()
        {
            IEnumerable<grade> data = new List<grade>();
            try
            {
                data = DATACCESS.ModulePlanning.GradeLoad();
            }
            catch (Exception e)
            {
                BadRequest(e.ToString());
            }
            return data;
        }
    }
}
