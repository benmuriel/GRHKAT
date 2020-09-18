using System.Web.Mvc;

namespace GRH_4WEB.Areas.PosteEmploi
{
    public class PosteEmploiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PosteEmploi";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PosteEmploi_default",
                "PosteEmploi/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}