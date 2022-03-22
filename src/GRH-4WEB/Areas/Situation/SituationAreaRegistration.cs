using System.Web.Mvc;

namespace GRH_4WEB.Areas.Situation
{
    public class SituationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Situation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Situation_default",
                "Situation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}