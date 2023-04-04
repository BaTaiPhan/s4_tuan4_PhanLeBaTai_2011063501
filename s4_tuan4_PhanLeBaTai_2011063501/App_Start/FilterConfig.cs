using System.Web;
using System.Web.Mvc;

namespace s4_tuan4_PhanLeBaTai_2011063501
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
