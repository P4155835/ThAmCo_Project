using System.Web;
using System.Web.Mvc;

namespace ThAmCo_WebServiceAPI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
