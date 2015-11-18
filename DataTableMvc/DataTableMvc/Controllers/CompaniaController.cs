using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataTables.Mvc;

using DataTableMvc.Models;


namespace DataTableMvc.Controllers
{
    public class CompaniaController : Controller
    {
        public JsonResult Index([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest req, CompaniaVm vm)
        {
            var todasCompanias = CompaniaRepository.Obter();
            var companiasFiltradas = todasCompanias.Where(x =>
                   (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
                && (vm.pais     == null || x.pais.ToLower().Contains(vm.pais.ToLower()))
            );

            var ordernadores = new Dictionary<string, Func<CompaniaVm, IComparable>>
                {
                    {"id",       x => int.Parse(x.id)},
                    {"compania", x => x.compania},
                    {"pais",     x => x.pais},
                    {"preco",    x => x.preco},
                };
            var direcionadores = new Dictionary<Column.OrderDirection, Func<string, IOrderedEnumerable<CompaniaVm>, IOrderedEnumerable<CompaniaVm>>>
                {
                    { Column.OrderDirection.Ascendant, (d, x) => x.ThenBy(ordernadores[d]) },
                    { Column.OrderDirection.Descendant, (d, x) => x.ThenByDescending(ordernadores[d]) },
                };

            var companiasOrdenadas = companiasFiltradas.OrderBy(_ => true); // gambiarra para criar o IOrderedEnumerable

            companiasOrdenadas = req.Columns
                .Where(o => o.OrderNumber != -1)
                .OrderBy(o => o.OrderNumber)
                .Aggregate(companiasOrdenadas, (acc, o) => direcionadores[o.SortDirection](o.Data, acc)); ;

            var companiasExibidas = companiasOrdenadas.Skip(req.Start).Take(req.Length);
            var companiasProjetadas = companiasExibidas.Select(c => (object)new { c.id, c.compania, c.pais, c.preco, options = c.id });

            var resp = new DataTablesResponse(
                req.Draw,
                companiasProjetadas.ToArray(),
                companiasFiltradas.Count(),
                todasCompanias.Count());

            return Json(resp, JsonRequestBehavior.AllowGet);
        }



        //public JsonResult Index(DataTableRequest req, CompaniaVm vm)
        //{
        //    var todasCompanias = CompaniaRepository.Obter();

        //    var companiasFiltradas = todasCompanias.Where(x =>
        //           (vm.compania == null || x.compania.ToLower().Contains(vm.compania.ToLower()))
        //        && (vm.pais     == null || x.pais.ToLower().Contains(vm.pais.ToLower()))
        //    );


        //    var ordernadores = new Dictionary<int, Func<CompaniaVm, IComparable>>
        //    {
        //        {0, x => int.Parse(x.id)},
        //        {1, x => x.compania},
        //        {2, x => x.pais},
        //        {3, x => x.preco},
        //    };
        //    var direcionadores = new Dictionary<string, Func<int, IOrderedEnumerable<CompaniaVm>, IOrderedEnumerable<CompaniaVm>>>
        //    {
        //        { "asc", (i, x) => x.ThenBy(ordernadores[i]) },
        //        { "desc", (i, x) => x.ThenByDescending(ordernadores[i]) },
        //    };

        //    var companiasOrdenadas = companiasFiltradas.OrderBy(_ => true); // gambiarra para criar o IOrderedEnumerable

        //    companiasOrdenadas = req.order
        //        .Where(o => ordernadores.ContainsKey(o.column) && direcionadores.ContainsKey(o.dir))
        //        .Aggregate(companiasOrdenadas, (acc, o) => direcionadores[o.dir](o.column, acc)); ;

        //    var companiasExibidas = companiasOrdenadas.Skip(req.start).Take(req.length);

        //    var companiasProjetadas = companiasExibidas.Select(c => (object)new { c.id, c.compania, c.pais, c.preco, options = c.id });

        //    var resp = new DataTablesResponse<object>
        //    {
        //        draw = req.draw,
        //        recordsTotal = todasCompanias.Count(),
        //        recordsFiltered = companiasFiltradas.Count(),
        //        data = companiasProjetadas.ToArray(),
        //    };

        //    return Json(resp, JsonRequestBehavior.AllowGet);
        //}
    }
}