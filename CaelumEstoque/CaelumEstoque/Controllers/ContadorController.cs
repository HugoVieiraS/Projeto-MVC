using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CaelumEstoque.Controllers
{
    //controller para contar a quantidade de vez que o usuário acessou uma página
    public class ContadorController : Controller
    {
        // GET: Contador
        public ActionResult Index()
        {
            //Session funciona como um dicionáiro, possui uma chave associada com valor.
            //chave string e valor um object
            //
            object valorNaSession =  Session["contador"]; //variavel recebe contador
            int contador = Convert.ToInt32(valorNaSession);//convertemos o contador em inteiro, caso passe nullo, vai devolver 0
            contador++;// incrementando

            Session["contador"] = contador; // guardando o valor da variavel contador

            return View(contador); // mandando para a visualização
        }
    }
}