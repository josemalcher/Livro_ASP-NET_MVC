﻿using System.Web;
using System.Web.Mvc;

namespace _01_PrimeiraAPPWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
