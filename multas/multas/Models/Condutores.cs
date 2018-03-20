using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class Condutores
    {
        public Condutores()
        {
            ListaDeMultas = new HashSet<Multas>();
        }
        [Key]
        //descrição dos atributos da tabela condutores
        public int ID { get; set; }//PK
        public string Nome { get; set; }
        public string BI { get; set; }
        public DateTime DataNasc { get; set; }
        public string Telemovel { get; set; }
        public string NumCartaConducao { get; set; }
        public string LocalEmissao { get; set; }
        public DateTime DataValidadeCarta { get; set; }

        //referencia as multas q um condutor 'recebe'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}