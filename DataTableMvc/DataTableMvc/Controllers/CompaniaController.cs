using DataTableMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataTableMvc.Controllers
{
    public class CompaniaController : Controller
    {
        [HttpPost]
        public ActionResult Index(/*JQueryDataTableParamModel param,*/ CompaniaVm vm)
        {
            var todasCompanias = Models.CompaniaRepository.Obter();
            var companiasFiltradas = todasCompanias.Where(x =>
                (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
                && (vm.pais == null || x.pais.ToLower().Contains(vm.pais.ToLower())));
            var companiasExibidas = companiasFiltradas.Skip(vm.iDisplayStart).Take(vm.iDisplayLength);
            var result = from c in companiasExibidas select new[] { c.id, c.compania, c.pais, c.preco };

            //return Json(
            //    new DataTables.Mvc.DataTablesResponse(vm.Draw, result, companiasFiltradas.Count(), todasCompanias.Count())
            //    , JsonRequestBehavior.AllowGet);

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