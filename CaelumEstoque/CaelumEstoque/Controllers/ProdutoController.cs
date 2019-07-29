using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO; // importando dao
using CaelumEstoque.Filtros;
using CaelumEstoque.Models; // importando models

namespace CaelumEstoque.Controllers
{

    [AutorizacaoFilterAttribute] //usando a classe fiter para validar o login , antes de usar o controller
    public class ProdutoController : Controller
    ///* Cadastro de produtos em estoque*\
    {
        // GET: Produto

        //Criando método para listar produtos

        [Route("produtos", Name = "ListaProdutos")] //url customizada
        public ActionResult Index()
        {
            //verificando se o usuario está logado para acessar
            object usuario = Session["usuarioLogado"];
            
            ProdutosDAO dao = new ProdutosDAO(); //instanciando produtosDAO
            IList<Produto> produtos = dao.Lista();
          
            //ViewBag.Produtos = produtos; ///propriedade para ser madnada para a view

            return View(produtos); // podemos mandar a variavel de forma difrerente
                                   // fora da viewbag é considerada a var principal de uma view
                                   //Nesse cado é preciso passsar model na view, caso tenha um foreach
        }

        //Cadastrar produtos// formulario
        public ActionResult Form()
        {
            var categoriasDAO = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = categoriasDAO.Lista();
            ViewBag.Categorias = categorias;

            ViewBag.Produto = new Produto(); // implemento para caso else na verificação,
            //retornar formulario preenchido
            
           return View();
        }


        //Parametro para tratar as informações pegas no formulario

        //apenas aceitar tipo post e não get
        [HttpPost]
        [ValidateAntiForgeryToken] ///validando token para adicionar produto
        public ActionResult Adiciona(Produto produto)
        {
            // Validação Mais complexa(Produtos com preços maior 100 reais)
            int idDainformatica = 1;
            if(produto.CategoriaId.Equals(idDainformatica) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido", "Informatica com preço abaixo de 100 reais"); //adicionar novo erro a lista
                //produto.Invalido é a chave que será passado para o helper na view
            }

            /// (Validação padrao mvc)fazendo validação com base na anotaçãpo da classe produto
            if (ModelState.IsValid)
            { 
                var dao = new ProdutosDAO();
                dao.Adiciona(produto);
                return RedirectToAction("Index", "Produto"); //para evitar que usuário de f5 e duplique a requisição no banco
            }
            else
            {
                //para quando der else, não voltar com o formulario vazio
                ViewBag.Produto = produto;
                //
                var categoriaDAO = new CategoriasDAO();
                ViewBag.Categorias = categoriaDAO.Lista();
                return View("Form");
            }
        }

        //Pagina visualização detalhes produto
        [Route("produtos/{id}", Name ="VisualizaProduto")] //customizar urls
        public ActionResult Visualiza(int id)
        {
            var dao = new ProdutosDAO();
            var produto = dao.BuscaPorId(id);
            ViewBag.Produto = produto;

            return View();
        }

        //metodo para decrementar a quantidade de produto em estoque
        public ActionResult DecrementaQtd(int id)
        {
            var dao = new ProdutosDAO();
            var produto = dao.BuscaPorId(id);
            produto.Quantidade--;
            dao.Atualiza(produto);
            //return RedirectToAction("Index"); // para atualizar com o jquery a tempo de decremento, iremos usar outra resposta
            return Json(produto);
        }
    }
}