using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CadeMeuMedicoAPP.Models;

namespace CadeMeuMedicoAPP.Controllers
{
    public class MedicosController : Controller
    {
        private EntidadesCadeMeuMedicoBDEntities db = new EntidadesCadeMeuMedicoBDEntities();

        public ActionResult Index()
        {
            var medicos = db.Medicos.Include("Cidades").Include("Especialidades").ToList();
            return View(medicos);
        }
    }
}