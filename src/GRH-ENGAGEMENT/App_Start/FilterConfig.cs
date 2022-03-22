using System.Web;
using System.Web.Mvc;

namespace GRH_ENGAGEMENT
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
