using System.Web.Mvc;

namespace GRH_ENGAGEMENT.Areas.Repartition
{
    public class RepartitionAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Repartition";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Repartition_default",
                "Repartition/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}