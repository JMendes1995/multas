using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace multas.Models
{
    public class MultasDB : DbContext 
    {
        public MultasDB():base("name=MultasDbConnectionSring")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
        //descreve os nomes das tabelas na Base de dados
        public virtual DbSet<Multas> Multas { get; set; }
        public virtual DbSet<Viaturas> Viaturas { get; set; }
        public virtual DbSet<Condutores> Condutores { get; set; }
        public virtual DbSet<Agentes> Agentes { get; set; }

    }
}