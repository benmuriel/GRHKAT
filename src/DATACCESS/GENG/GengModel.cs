using DATACCESS.GENG.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATACCESS.GENG
{
    class GengModel : DbContext
    {
        // Votre contexte a été configuré pour utiliser une chaîne de connexion « GrhkatModel » du fichier 
        // de configuration de votre application (App.config ou Web.config). Par défaut, cette chaîne de connexion cible 
        // la base de données « GRH_4.DATA.GrhkatModel » sur votre instance LocalDb. 
        // 
        // Pour cibler une autre base de données et/ou un autre fournisseur de base de données, modifiez 
        // la chaîne de connexion « GrhkatModel » dans le fichier de configuration de l'application.
        public GengModel() : base("name=GengModel")
        {

        }

        public virtual DbSet<departement> departement { get; set; }
        public virtual DbSet<taux_change> taux_change { get; set; }
        public virtual DbSet<grade> grade { get; set; }
        public virtual DbSet<bareme> bareme { get; set; }
        public virtual DbSet<engagement> engagement { get; set; }
        public virtual DbSet<beneficiaire> Beneficiaires { get; set; }
        public virtual DbSet<projet_engagement> projet_engagement { get; set; }
        public virtual DbSet<instance_traitement> instance_execution { get; set; }
        public virtual DbSet<charge_sociale> Charge_Sociales { get; set; }
        public virtual DbSet<eligibilite_prise_en_charge> eligibilite_prise_en_charge { get; set; }
        public virtual DbSet<type_prise_en_charge> type_prise_en_charge { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
            // modelBuilder.HasDefaultSchema("public");
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<departement>()
                   .HasMany(b => b.projet_Engagements)
                   .WithMany(c => c.Departements)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("departement_id");
                       cs.MapRightKey("projet_id");
                       cs.ToTable("project_engagement_departement");
                   });

            modelBuilder.Entity<type_prise_en_charge>()
                   .HasMany(b => b.projet_Engagements)
                   .WithMany(c => c.Type_Prise_En_Charges)
                   .Map(cs =>
                   {
                       cs.MapLeftKey("type_prise_en_charge_id");
                       cs.MapRightKey("projet_id");
                       cs.ToTable("project_engagement_type_prise_en_charge");
                   });

            modelBuilder.Entity<instance_traitement>()
           .HasMany<type_prise_en_charge>(s => s.TPCExecution)
           .WithRequired(s => s.instance_execution)
           .HasForeignKey<int?>(s => s.instance_execution_id);

            modelBuilder.Entity<instance_traitement>()
           .HasMany<type_prise_en_charge>(s => s.TPCEngagement)
           .WithRequired(s => s.instance_engagement)
           .HasForeignKey<int?>(s => s.instance_engagement_id);

            modelBuilder.Entity<instance_traitement>()
           .HasMany<type_prise_en_charge>(s => s.TPCLiquidation)
           .WithRequired(s => s.instance_liquidation)
           .HasForeignKey<int?>(s => s.instance_liquidation_id);

            modelBuilder.Entity<beneficiaire>()
           .HasMany<charge_sociale>(s => s.Charge_Sociales)
           .WithRequired(s => s.beneficiaire)
           .HasForeignKey<long>(s => s.agent_id);

            modelBuilder.Entity<beneficiaire>()
             .HasMany<eligibilite_prise_en_charge>(s => s.eligibilite_prise_en_charges)
             .WithRequired(s => s.beneficiaire)
             .HasForeignKey<long>(s => s.beneficiaire_id);

            modelBuilder.Entity<beneficiaire>()
         .HasMany<engagement>(s => s.engagements)
         .WithRequired(s => s.beneficiaire)
         .HasForeignKey<long>(s => s.beneficiaire_id);



            modelBuilder.Entity<type_prise_en_charge>()
                           .HasMany<eligibilite_prise_en_charge>(s => s.eligibilite_prise_en_charges)
                           .WithRequired(s => s.type_prise_en_charge)
                           .HasForeignKey<long>(s => s.type_prise_en_charge_id);

            modelBuilder.Entity<type_prise_en_charge>()
                            .HasMany<engagement>(s => s.engagements)
                            .WithRequired(s => s.type_prise_en_charge)
                            .HasForeignKey<long>(s => s.type_prise_en_charge_id);

            modelBuilder.Entity<projet_engagement>()
                            .HasMany<engagement>(s => s.engagements)
                            .WithRequired(s => s.projet_engagement)
                            .HasForeignKey<long>(s => s.projet_id);

            modelBuilder.Entity<charge_sociale>()
                            .HasMany<engagement>(s => s.engagements)
                            .WithOptional(s => s.charge_Sociale)
                            .HasForeignKey<long?>(s => s.charge_sociale_id);

            modelBuilder.Entity<instance_traitement>()
                            .HasMany<engagement>(s => s.engagements)
                            .WithOptional(s => s.Instance_execution)
                            .HasForeignKey<int?>(s => s.instance_id); 

            modelBuilder.Entity<bareme>()
                            .HasMany<engagement>(s => s.engagements)
                            .WithOptional(s => s.bareme)
                            .HasForeignKey<int?>(s => s.bareme_id);

            modelBuilder.Entity<grade>()
                            .HasMany<bareme>(s => s.baremes)
                            .WithRequired(s => s.grade)
                            .HasForeignKey<string>(s => s.grade_id);

            modelBuilder.Entity<type_prise_en_charge>()
                             .HasMany<bareme>(s => s.baremes)
                             .WithRequired(s => s.type_prise_en_charge)
                             .HasForeignKey<int>(s => s.type_prise_en_charge_id);


            modelBuilder.Entity<eligibilite_prise_en_charge>().HasKey(s => new { s.situation_agent_id, s.beneficiaire_id, s.type_prise_en_charge_id });
            modelBuilder.Entity<engagement>().HasKey(s => new { s.projet_id, s.beneficiaire_id, s.type_prise_en_charge_id });

            // modelBuilder.Entity<planning_project>()
            //       .HasMany<structure>(s => s.structures)
            //       .WithMany(c => c.planning_projects)
            //       .Map(cs =>
            //       {
            //           cs.MapLeftKey("planning_project_id");
            //           cs.MapRightKey("structure_id");
            //           cs.ToTable("planning_project_structure");
            //       });
        }
    }
}
