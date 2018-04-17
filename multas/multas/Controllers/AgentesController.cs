using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using multas.Models;

namespace multas.Controllers
{
    public class AgentesController : Controller
    {
        //cria uma variavel que representa a base de dados 
        private MultasDB db = new MultasDB();

        // GET: Agentes
        public ActionResult Index()
        {
            //db.Agentes.ToList() -> select * from Agentes;
            //enviar para a view uma lista com todos os agentes da base de dados
            var listaDeAgentes=db.Agentes.ToList().OrderBy(a=>a.Nome);
            return View(listaDeAgentes);
        }

        // GET: Agentes/Details/5
        //int? id -> o id pode ser null
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int? id)
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
            //entrega a view os dados do agente encontrado
            return View(agente);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            //representa a view para se inserir um novo agente
            return View();
        }


        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

            /// <summary>
            /// 
            /// </summary>
            /// <param name="agente"></param>
            /// <param name="uploadFotografia"></param>
            /// <returns></returns>
        [HttpPost]
        //anotador para proteção por roubo de identidade
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Esquadra")] Agentes agente,HttpPostedFileBase uploadFotografia)
        {
            // escreve os dados ne um novo agente na BD
            //especificar o id do novo agente
            int idNovoAgente=db.Agentes.Max(a=>a.ID)+1;
            //guardar o id
            agente.ID = idNovoAgente;
            //especificar o nome do ficheiro 
            string nomeImg = "agente_"+idNovoAgente+".jpg";
            //validar se  a imagem foi fornecida
            //var aux
            var path = "";
            if (uploadFotografia != null)
            {
                //o ficheiro foi fornecido
                //validar se é uma img
                //formatar o tamanho da imagem 
                //criar o caminhao completo até ao sitio onde o ficheiro sera guardado
                path = Path.Combine(Server.MapPath("~/imagens/imagens/"),nomeImg);
                //guardar o ficheiro
                agente.Fotografia = nomeImg;
            }
            else
            {
                //n foi fornecido qq ficheiro
                ModelState.AddModelError("","nao foi fornecido uma imagem...");
                //devolver o controlo a view 
                return View(agente);
            }
            
                
            //ModelState.IsValid -> confronta os dados fornecidos da view 
           
            if (ModelState.IsValid)
            {
                //adiciona o agente à tabela agentes 
                db.Agentes.Add(agente);
                //faz commit 
                db.SaveChanges();
                //escrever o ficheiro com a fotografia no disco rigido na pasta 'imagens '
                uploadFotografia.SaveAs(path);

                return RedirectToAction("Index");
            }

            return View(agente);
        }

        // GET: Agentes/Edit/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
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
            //entrega a view os dados do agente encontrado
            return View(agente);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="agentes"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                //neste caso já existe agente 
                //apenas quero editar os seus dados
                db.Entry(agentes).State = EntityState.Modified;
                //faz o commit
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        /// <summary>
        /// apresenta na view os dados de um agente com vista  à sua, eventual, 
        /// eliminação
        /// </summary>
        /// <param name="id">identificador doo agente</param>
        /// <returns></returns>
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
        public ActionResult DeleteConfirmed(int id)
        {
            Agentes agentes = db.Agentes.Find(id);
            //remove o agente 
            db.Agentes.Remove(agentes);
            //commit
            db.SaveChanges();
            return RedirectToAction("Index");
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
