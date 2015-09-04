using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataTableMvc.Models;
using DataTableMvc.Models.DataTables;


namespace DataTableMvc.Controllers
{
    public class CompaniaController : Controller
    {
        public JsonResult Index(DataTableRequest param, CompaniaVm vm)
        {
            var todasCompanias = CompaniaRepository.Obter();

            var companiasFiltradas = todasCompanias.Where(x =>
                   (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
                && (vm.pais     == null || x.pais.ToLower().Contains(vm.pais.ToLower()))
            );

            var companiasOrdenadas = companiasFiltradas.OrderBy(_ => true); // gambiarra para criar o IOrderedEnumerable

            foreach (var order in param.order)
            {
                companiasOrdenadas =
                      order.column == 0 && order.dir == "asc"  ? companiasOrdenadas.ThenBy(x => int.Parse(x.id))
                    : order.column == 0 && order.dir == "desc" ? companiasOrdenadas.ThenByDescending(x => int.Parse(x.id))
                    : order.column == 1 && order.dir == "asc"  ? companiasOrdenadas.ThenBy(x => x.compania)
                    : order.column == 1 && order.dir == "desc" ? companiasOrdenadas.ThenByDescending(x => x.compania)
                    : order.column == 2 && order.dir == "asc"  ? companiasOrdenadas.ThenBy(x => x.pais)
                    : order.column == 2 && order.dir == "desc" ? companiasOrdenadas.ThenByDescending(x => x.pais)
                    : order.column == 3 && order.dir == "asc"  ? companiasOrdenadas.ThenBy(x => x.preco)
                    : order.column == 3 && order.dir == "desc" ? companiasOrdenadas.ThenByDescending(x => x.preco)
                    : companiasOrdenadas;
            }

            var t = companiasOrdenadas.ToArray();

            var companiasExibidas = companiasOrdenadas.Skip(param.start).Take(param.length);
            var companiasProjetadas = companiasExibidas.Select(c => (object)new { c.id, c.compania, c.pais, c.preco, options = c.id });

            var ret = new DataTablesResponse<object>
            {
                draw = param.draw,
                recordsTotal = todasCompanias.Count(),
                recordsFiltered = companiasFiltradas.Count(),
                data = companiasProjetadas.ToArray(),
            };

            return Json(ret, JsonRequestBehavior.AllowGet);
        }

        // exemplo com a api legada
        //[HttpPost]
        //public ActionResult Index(DataTableParamLegacy param, CompaniaVm vm)
        //{
        //    var todasCompanias = Models.CompaniaRepository.Obter();
        //    var companiasFiltradas = todasCompanias.Where(x =>
        //        (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
        //        && (vm.pais == null || x.pais.ToLower().Contains(vm.pais.ToLower())));
        //    var companiasExibidas = companiasFiltradas.Skip(param.iDisplayStart).Take(param.iDisplayLength);
        //    var result = from c in companiasExibidas select new[] { c.id, c.compania, c.pais, c.preco };
        //    //return Json(
        //    //    new DataTables.Mvc.DataTablesResponse(vm.Draw, result, companiasFiltradas.Count(), todasCompanias.Count())
        //    //    , JsonRequestBehavior.AllowGet);
        //    return Json(
        //        new
        //        {
        //            sEcho = param.sEcho,
        //            iTotalRecords = todasCompanias.Count(),
        //            iTotalDisplayRecords = companiasFiltradas.Count(),
        //            aaData = result
        //        },
        //        JsonRequestBehavior.AllowGet);
        //}
    }
}