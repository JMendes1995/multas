using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        //descrição dos atributos da tabela agentes
        public int ID { get; set; }//PK
        [Required(ErrorMessage ="O {0} é de preenchimento obrigatório!!")]
        [RegularExpression("[A-ZÍÉÂÁ][a-záéíóúàèìòùâêîôûäëïöüçãõ]+(( |'|-| dos | da | de | e | d')[A-ZÍÉ][a-záéíóúàèìòùâêîôûäëïöüçãõ]+){1,3}"
            ,ErrorMessage ="o {0} apenas pode conter letras e espaços em branco. Cada palavra começa em Maiúscula seguida de minúsculas...")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A {0} é de preenchimento obrigatório!!")]
        //[RegularExpression("[A-ZÍÉÂÁ]*[a-záéíóúàèìòùâêîôûäëïöüçãõ -]*", ErrorMessage ="A {0} não é válida")]
        public string Esquadra { get; set; }
        public string Fotografia { get; set; }

        //referencia as multas q um condutor 'emite'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }


        //----------------------------------------------------
        //nome do login 'username' usado pelo agente 
        //na pratica, é uma gk para a tabela dos utilizadores
        //[Required]
        public string UserName { get;set; }


    }
}