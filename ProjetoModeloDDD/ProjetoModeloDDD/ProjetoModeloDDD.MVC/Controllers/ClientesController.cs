using AutoMapper;
using ProjetoModeloDDD.Domain.Entities;
using ProjetoModeloDDD.Infra.Data.Repositories;
using ProjetoModeloDDD.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjetoModeloDDD.MVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteRepository _clienteRepository = new ClienteRepository();

        //
        // GET: /Clientes/
        public ActionResult Index()
        {
            var clienteViewModel = Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteRepository.GetAll());
            return View(clienteViewModel);
        }

        //
        // GET: /Clientes/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteDomain = Mapper.Map<ClienteViewModel, Cliente>(cliente);
                _clienteRepository.Add(clienteDomain);

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        //
        // GET: /Clientes/Edit/5
        public ActionResult Edit(int id)
        {
            var clienteDomain = _clienteRepository.GetById(id);
            var cliente = Mapper.Map<Cliente, ClienteViewModel>(clienteDomain);
            return View(cliente);
        }

        //
        // POST: /Clientes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ClienteViewModel cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteDomain = Mapper.Map<ClienteViewModel, Cliente>(cliente);
                _clienteRepository.Update(clienteDomain);

                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        //
        // GET: /Clientes/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Clientes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
