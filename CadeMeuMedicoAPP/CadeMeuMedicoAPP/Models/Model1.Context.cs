﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CadeMeuMedicoAPP.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntidadesCadeMeuMedicoBDEntities : DbContext
    {
        public EntidadesCadeMeuMedicoBDEntities()
            : base("name=EntidadesCadeMeuMedicoBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cidades> Cidades { get; set; }
        public virtual DbSet<Especialidades> Especialidades { get; set; }
        public virtual DbSet<Medicos> Medicos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
