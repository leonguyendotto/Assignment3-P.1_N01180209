﻿using System.Web;
using System.Web.Mvc;

namespace Assignment3_N01180209
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
