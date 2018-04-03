using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class MultasDB : DbContext 
    {
        public MultasDB():base("name=MultasDbConnectionSring")
        {

        }
        //descreve os nomes das tabelas na Base de dados
        public virtual DbSet<Multas> Multas { get; set; }
        public virtual DbSet<Viaturas> Viaturas { get; set; }
        public virtual DbSet<Condutores> Condutores { get; set; }
        public virtual DbSet<Agentes> Agentes { get; set; }

    }
}