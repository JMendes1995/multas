using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class Utilizadores
    {

        /// <summary>
        /// os atributos q aqui vao ser adicionados serao adicionados à tabela utilizadores
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        [Key]
        public int IdUtilizador { get; set; }
        public string NomeProprio { get; set; }
        public string Apelido { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        public string NIF { get; set; }
        //************************************************
        //atributo seguinte vai criar uma chave forasteira
        //para a tebela autenticação
        //************************************************
        public string NomeRegistoDoUtilizador { get; set; }

    }
}