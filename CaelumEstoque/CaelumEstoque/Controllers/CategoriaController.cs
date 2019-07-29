using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            var dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categoria = dao.Lista();
            ViewBag.Categorias = categoria;

            return View();
        }

        public ActionResult Form()
        {
            return View();
        }

        public ActionResult Adiciona(CategoriaDoProduto categoria)
        {
            var dao = new CategoriasDAO();
            dao.Adiciona(categoria);

            return RedirectToAction("Index");
        }
    }
}