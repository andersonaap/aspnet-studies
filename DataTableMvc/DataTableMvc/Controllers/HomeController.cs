using DataTableMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataTableMvc.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Obter(JQueryDataTableParamModel param, CompaniaVm vm)
        {
            var todasCompanias = Models.CompaniaRepository.Obter();
            var companiasFiltradas = todasCompanias.Where(x => 
                (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
                && (vm.pais == null || x.pais.ToLower().Contains(vm.pais.ToLower())));
            var companiasExibidas = companiasFiltradas.Skip(vm.iDisplayStart).Take(vm.iDisplayLength);
            var result = from c in companiasExibidas select new[] { c.id, c.compania, c.pais, c.preco };

            return Json(
                new
                {
                    sEcho = vm.sEcho,
                    iTotalRecords = todasCompanias.Count(),
                    iTotalDisplayRecords = companiasFiltradas.Count(),
                    aaData = result
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}