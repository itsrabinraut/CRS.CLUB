using CRS.CLUB.APPLICATION.Filters;
using System.Web.Mvc;

namespace CRS.CLUB.APPLICATION
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new SessionExpiryFilterAttribute());
            ////filters.Add(new HandleErrorAttribute());
            //filters.Add(new ActivityLogFilter());
        }
    }
}