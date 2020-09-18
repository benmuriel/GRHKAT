using DATACCESS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS
{
    public class GrhkatModel : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « GrhkatModel » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « GRH_4.DATA.GrhkatModel » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « GrhkatModel » dans le fichier de configuration de l'application.
        public GrhkatModel()
            : base("name=GrhkatModel")
        {
        }

        // Ajoutez un DbSet pour chaque type d'entité à inclure dans votre modèle. Pour plus d'informations 
        // sur la configuration et l'utilisation du modèle Code First, consultez http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<type_structure> type_structures { get; set; }
        public virtual DbSet<type_position> type_position { get; set; }
        public virtual DbSet<categorie_type_position> categorie_type_position { get; set; }
        public virtual DbSet<institution_detachement> institution_detachement { get; set; }
        public virtual DbSet<v_position_temporaire> v_position_temporaire { get; set; }
        public System.Data.Entity.DbSet<DATACCESS.Models.v_charge_sociale> v_charge_sociale { get; set; }
        public System.Data.Entity.DbSet<DATACCESS.Models.agent_synthese> agent_synthese { get; set; }
        public System.Data.Entity.DbSet<DATACCESS.Models.v_poste_planning> v_poste_planning { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<categorie_type_position>()
             .HasMany<institution_detachement>(s => s.institution_detachements)
             .WithRequired(s => s.categorie_Type_Position)
             .HasForeignKey<string>(s => s.categorie);

            modelBuilder.Entity<categorie_type_position>()
           .HasMany<type_position>(s => s.type_positions)
           .WithRequired(s => s.categorie_Type_Position)
           .HasForeignKey<string>(s => s.categorie);
        }


    }

    //pblic class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
