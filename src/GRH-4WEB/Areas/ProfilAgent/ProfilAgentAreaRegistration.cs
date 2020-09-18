using System.Web.Mvc;

namespace GRH_4WEB.Areas.ProfilAgent
{
    public class ProfilAgentAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProfilAgent";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProfilAgent_default",
                "ProfilAgent/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}