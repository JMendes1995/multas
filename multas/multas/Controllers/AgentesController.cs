using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using multas.Models;

namespace multas.Controllers
{
    [Authorize(Roles= "Agentes,GestaoPessoal")] //só pessoas autenticadas é que podem executar estes recursos
    public class AgentesController : Controller
    {
        //cria uma variavel que representa a base de dados 
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Agentes
        [AllowAnonymous]//apesat de haver restrições de acesso esta linha permite os ut anonimos verem o con
        public ActionResult Index()
        {
            //recuperar os dados pessoais da pessoa que se autenticou   
            var DadosPessoais = db.Utilizadores.Where(
                uti => uti.NomeRegistoDoUtilizador
                        .Equals(User.Identity.Name)).FirstOrDefault();
            //agora, com estes objeto, ja posso utilizar
            //os dados pessoais de um utilizaodr no meu programa
            //por exemplo :
            //Session["DadosUtilizador"]=DadosPessoais.NomeProprio+" "+DadosPessoais.Apelido+" "+DadosPessoais.DataDeNascimento+" "+DadosPessoais.NIF;

            // (LINQ)db.Agente.ToList() --> em SQL: SELECT * FROM Agentes ORDER BY 
            // constroi uma lista com os dados de todos os Agentes
            // e envia-a para a View

            var listaAgentes = db.Agentes.ToList().OrderBy(a => a.Nome);

            return View(listaAgentes);
       }

        // GET: Agentes/Details/5
        /// <summary>
        /// Apresenta os detalhes de um Agente
        /// </summary>
        /// <param name="id"> representa a PK que identifica o Agente </param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {

            // int? - significa que pode haver valores nulos

            // protege a execução do método contra a Não existencia de dados
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                // ou não foi introduzido um ID válido,
                // ou foi introduzido um valor completamente errado
                return RedirectToAction("Index");
}
            // vai procurar o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);
           
                       // se o Agente NÃO for encontrado...
                       if (agente == null)
                           {
                               // return HttpNotFound();
                               return RedirectToAction("Index");
                }
            
                        // envia para a View os dados do Agente
                        return View(agente);
                 }


        // GET: Agentes/Edit/5
        /// <summary>
        /// apresentar na view os dados de um agente para uma eventual edição
        /// </summary>
        /// <param name="id">identificar o agente a editar</param>
        /// <returns></returns>
        

        public ActionResult Edit(int? id)
        {

            // proteção para o caso de nao ter sido fornecido um ID valido 
            if (id == null)
            {
                //instrução original
                //devolve um erro q n há id
                //logo n e possivel pesquisar por um agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma pagina que controlamos
                return RedirectToAction("Index");

            }

            // procura na BD, i agente cujo o ID foi Fornecido
            Agentes agente = db.Agentes.Find(id);
            //proteção para o caso de não ter sido encontrado qq agente
            //que tenha o ID fornecido
            if (agente == null)
            {
                //n foi encontrado o agente
                // return HttpNotFound();
                return RedirectToAction("Index");
            }

            //existe o agente
            if (User.Identity.Name.Equals(agente.UserName) || User.IsInRole("GestaoPessoal"))
            {
                return View(agente);
            }

            //entrega a view os dados do agente encontrado
            return RedirectToAction("Index");
        }

        // GET: Agentes/Create
        [Authorize(Roles = "GestaoPessoal")]
        public ActionResult Create()
        {
            return View();
        }

    // POST: Agentes/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

    /// <summary>
    /// concretiza a edição dos dadosde um gente
    /// </summary>
    /// <param name="agente">dados do agente</param>
    /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia,UserName")] Agentes agente, HttpPostedFileBase editFotografia)
        {
            //se o utilizador pertence a role gestaoPessoal, pode efetuar a edição,
            //sem qualquer restrição  
            //se o ut nao pertence à roe  acima referida  e não é dono dos dados nada se pode fazer
            //se o ut nao pertence a role e é domo dos dados
            
                //1-pesquisar os dados antigos do agente na bd
                //2-substituir nos dados novos, o valor da 'esquadra' pelo dados antigos da  'esquadra'
                //3-guardar os dados na bd
                //nota:claro que a validação do nome e da fotografia também tem de acontecer
            if (User.Identity.Name.Equals(agente.UserName) || User.IsInRole("GestaoPessoal"))
            {
                string time = DateTime.Now.ToString("yyyyMMddHHmmss");
                var novoNome = "agente_" + agente.ID + "_" + time + ".jpg";
                var lastPath = Path.Combine(Server.MapPath("~/imagens/imagens/"), agente.Fotografia);


                var path = "";
                if (editFotografia != null)
                {//inserir a nova foto no disco rigido
                 //apaga a fotografia anterior do disco
                    System.IO.File.Delete(lastPath);
                    path = Path.Combine(Server.MapPath("~/imagens/imagens/"), novoNome);
                    agente.Fotografia = novoNome;
                }


                if (ModelState.IsValid)
                {
                    try
                    {
                        db.Entry(agente).State = EntityState.Modified;
                   
                        //faz o commit
                        db.SaveChanges();

                        editFotografia.SaveAs(path);

                        return RedirectToAction("Index");
                    }
                    catch (Exception) { }
                    //neste caso já existe agente 
                    //apenas quero editar os seus dados

                }
                return View(agente);
            }
            return RedirectToAction("Index");
        }




        // GET: Agentes/Delete/5
        /// <summary>
        /// apresenta na view os dados de um agente com vista  à sua, eventual, 
        /// eliminação
        /// </summary>
        /// <param name="id">identificador doo agente</param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "GestaoPessoal")]
        public ActionResult Delete(int? id)
        {

            // proteção para o caso de nao ter sido fornecido um ID valido 
            if (id == null)
            {
                //instrução original
                //devolve um erro q n há id
                //logo n e possivel pesquisar por um agente
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //redirecionar para uma pagina que controlamos
                return RedirectToAction("Index");

            }

            // procura na BD, o agente cujo o ID foi Fornecido
            Agentes agente = db.Agentes.Find(id);
            //proteção para o caso de não ter sido encontrado qq agente
            //que tenha o ID fornecido
            if (agente == null)
            {
                //n foi encontrado o agente
                // return HttpNotFound();
                return RedirectToAction("Index");
            }
            //entrega a view os dados do agente encontrado
            return View(agente);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "GestaoPessoal")]
        public ActionResult DeleteConfirmed(int id)
        {
            Agentes agentes = db.Agentes.Find(id);
            try
            {
                //remove o agente 
                db.Agentes.Remove(agentes);
                //commit
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

               ModelState.AddModelError("", string.Format("houve um erro nº {0}-{1}",id, agentes.Nome));
                
            }
            //se cheguei aqui é pqhouve um prob
            return View(agentes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
