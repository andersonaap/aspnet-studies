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
        public JsonResult Index(DataTableRequest req, CompaniaVm vm)
        {
            var todasCompanias = CompaniaRepository.Obter();

            var companiasFiltradas = todasCompanias.Where(x =>
                   (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
                && (vm.pais     == null || x.pais.ToLower().Contains(vm.pais.ToLower()))
            );


            var ordernadores = new Dictionary<int, Func<CompaniaVm, IComparable>>
            {
                {0, x => int.Parse(x.id)},
                {1, x => x.compania},
                {2, x => x.pais},
                {3, x => x.preco},
            };
            var direcionadores = new Dictionary<string, Func<int, IOrderedEnumerable<CompaniaVm>, IOrderedEnumerable<CompaniaVm>>>
            {
                { "asc", (i, x) => x.ThenBy(ordernadores[i]) },
                { "desc", (i, x) => x.ThenByDescending(ordernadores[i]) },
            };

            var companiasOrdenadas = companiasFiltradas.OrderBy(_ => true); // gambiarra para criar o IOrderedEnumerable

            companiasOrdenadas = req.order
                .Where(o => ordernadores.ContainsKey(o.column) && direcionadores.ContainsKey(o.dir))
                .Aggregate(companiasOrdenadas, (acc, o) => direcionadores[o.dir](o.column, acc)); ;

            var companiasExibidas = companiasOrdenadas.Skip(req.start).Take(req.length);

            var companiasProjetadas = companiasExibidas.Select(c => (object)new { c.id, c.compania, c.pais, c.preco, options = c.id });

            var resp = new DataTablesResponse<object>
            {
                draw = req.draw,
                recordsTotal = todasCompanias.Count(),
                recordsFiltered = companiasFiltradas.Count(),
                data = companiasProjetadas.ToArray(),
            };

            return Json(resp, JsonRequestBehavior.AllowGet);
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