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
            return View(db.Agentes.ToList());
        }

        // GET: Agentes/Details/5
        //int? id -> o id pode ser null
        public ActionResult Details(int? id)
        {
            // proteção para o caso de nao ter sido fornecido um ID valido 
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // procura na BD, i agente cujo o ID foi Fornecido
            Agentes agentes = db.Agentes.Find(id);
            //proteção para o caso de não ter sido encontrado qq agente
            //que tenha o ID fornecido
            if (agentes == null)
            {
                return HttpNotFound();
            }
            //entrega a view os dados do agente encontrado
            return View(agentes);
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
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
