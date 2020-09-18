using System.Web.Mvc;

namespace GRH_4WEB.Areas.PositionTemporaire
{
    public class PositionTemporaireAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PositionTemporaire";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PositionTemporaire_default",
                "PositionTemporaire/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}