using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class Agentes
    {
        public Agentes()
        {
            ListaDeMultas = new HashSet<Multas>();
        }
        [Key]
        //descrição dos atributos da tabela agentes
        public int ID { get; set; }//PK
        public string Nome { get; set; }
        public string Esquadra { get; set; }
        public string Fotografia { get; set; }

        //referencia as multas q um condutor 'emite'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }

    }
}