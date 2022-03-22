using System.Web.Mvc;

namespace GRH_ENGAGEMENT.Areas.Engagement
{
    public class EngagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Engagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Engagement_default",
                "Engagement/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}