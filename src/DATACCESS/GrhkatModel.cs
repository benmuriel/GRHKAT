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

        public virtual DbSet<poste> poste { get; set; }
        public virtual DbSet<type_structure> type_structures { get; set; }
        public virtual DbSet<type_position> type_position { get; set; }
        public virtual DbSet<categorie_type_position> categorie_type_position { get; set; }
        public virtual DbSet<structure> structure { get; set; }
        public virtual DbSet<lieu_realisation_type_position> lieu_realisation_type_position { get; set; }
        public virtual DbSet<planning_project> planning_project { get; set; }
        public virtual DbSet<motif_type_position> motif_type_position { get; set; }
         public virtual DbSet<v_position_temporaire> v_position_temporaire { get; set; }
         public virtual DbSet<v_situation_agent_required> v_situation_agent_required { get; set; }
         public virtual DbSet<v_planning_project> v_planning_project { get; set; }
         public virtual DbSet<v_poste> v_poste { get; set; }
        public virtual DbSet<DATACCESS.Models.v_charge_sociale> v_charge_sociale { get; set; }
        public virtual DbSet<DATACCESS.Models.agent_synthese> agent_synthese { get; set; }
        public virtual DbSet<DATACCESS.Models.v_poste_planning> v_poste_planning { get; set; }
        public virtual DbSet<type_affinite_charge_sociale> type_affinite_charge_sociale { get; set; }
        public virtual DbSet<v_situation_agent_traitement> v_situation_agent_traitrement { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<categorie_type_position>()
           .HasMany<type_position>(s => s.type_positions)
           .WithRequired(s => s.categorie_Type_Position)
           .HasForeignKey<string>(s => s.categorie);

            modelBuilder.Entity<type_position>()
             .HasMany<planning_project>(s => s.planning_projects)
             .WithRequired(s => s.type_position)
             .HasForeignKey<long>(s => s.type_position_id);

            modelBuilder.Entity<v_situation_agent_traitement>().HasKey(s => new { s.situation_agent_id, s.type_traitement_id });
            modelBuilder.Entity<v_situation_agent_required>().HasKey(s => new { s.agent_id, s.type_position_id });
 
            modelBuilder.Entity<planning_project>()
                  .HasMany<structure>(s => s.structures)
                  .WithMany(c => c.planning_projects)
                  .Map(cs =>
                  {
                      cs.MapLeftKey("planning_project_id");
                      cs.MapRightKey("structure_id");
                      cs.ToTable("planning_project_structure");
                  });
        }
    }

    //pblic class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
