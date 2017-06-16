﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeMeuMedicoAPP.Controllers
{
    public class MensagensController : Controller
    {
        public ActionResult BomDia()
        {
            return Content("Bom dia... hoje você acordou cedo!");
        }
        public ActionResult BoaTarde()
        {
            return Content("Boa tarde... não durma na mesa do trabalho!");
        }
    }
}