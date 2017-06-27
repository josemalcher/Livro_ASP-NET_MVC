using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedicoAPP.Models;

namespace CadeMeuMedicoAPP.Controllers
{
    public class HomeController : BaseController
    {
        private EntidadesCadeMeuMedicoBDEntities db = new EntidadesCadeMeuMedicoBDEntities();

        public ActionResult Login()
        {
            ViewBag.Title = "Seja bem vindo(a)";
            return View();
        }

        public ActionResult Index()
        {
            //ViewBag.Especialidades = new SelectList(db.Especialidades, "IDEspecialidade", "Nome");
            //ViewBag.Cidades = new SelectList(db.Cidades, "IDCidade", "Nome");

            return View();
        }

       
    }
}
