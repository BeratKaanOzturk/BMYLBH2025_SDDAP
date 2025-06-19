using System.Web;
using System.Web.Mvc;

namespace BMYLBH2025_SDDAP
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
