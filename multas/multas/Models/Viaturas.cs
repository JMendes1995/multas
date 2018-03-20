using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class Viaturas
    {
        public Viaturas(){
            ListaDeMultas = new HashSet<Multas>();
        }
        [Key]
        //descrição dos atributos da tabela viatura
        public int ID { get; set; }//PK
        //dados da viatura
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }

        //dados do dono da viatura
        public string NomeDono { get; set; }
        public string Morada { get; set; }
        public string CodPostalDono { get; set; }
        public DateTime DataValidadeCarta { get; set; }

        //referencia as multas q um condutor 'recebe' numa viatura
        public virtual ICollection <Multas> ListaDeMultas { get; set; }

    }
}