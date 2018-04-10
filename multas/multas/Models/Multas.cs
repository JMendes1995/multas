using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class Multas
    {

        [Key]
        //descrição dos atributos da tabela Multas
        public int ID { get; set; }//PK
        public string Infracao { get; set; }
        public string LocalDaMulta { get; set; }
        public decimal ValorMulta { get; set; }
        public DateTime DataDaMulta { get; set; }
        public string Cor { get; set; }

        //********************************************************************
        //representar as chaves forasteiras que relacionam 
        //com outras classes
        //********************************************************************

        //FK para a tabela Condutores
        [ForeignKey("Condutor")]
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }

        //FK para a tabela Viatura
        [ForeignKey("Viatura")]
        public int ViaturaFK { get; set; }
        public virtual Viaturas Viatura { get; set; }

        //FK para a tabela Agentes
        [ForeignKey("Agente")]
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }
    }
}