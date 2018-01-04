using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wikiPr.Models;
using wikiPr.Controllers;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using wikiPr.Models.Views;
using System.Globalization;
using System.Data.Entity.Migrations;



namespace wikiPr.Controllers {
    
    public class EntitiesContext : DbContext {
        public DbSet<Article> ArticleEntities { get; set; }
        public DbSet<Utilisateur> UtilisateurEntities { get; set; }
        public EntitiesContext() : base("WikiCon")  //    base("defaultConnection")
        {
            Database.SetInitializer<EntitiesContext>(new CreateDatabaseIfNotExists<EntitiesContext>());  //créer la BD
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Article>().ToTable("Article", "dbo"); //creer table Article
            modelBuilder.Entity<Article>().Property(t => t.ArticleId).HasColumnName("ArticleId").HasColumnType("INT");
            modelBuilder.Entity<Article>().HasKey(t => t.ArticleId);
            modelBuilder.Entity<Article>().Property(t => t.ArticleId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();
           
            modelBuilder.Entity<Article>().Property(t => t.accederTitre).HasColumnName("Titre");
            modelBuilder.Entity<Article>().Property(t => t.accederContenu).HasColumnName("Contenu");
            modelBuilder.Entity<Article>().Property(t => t.accederDateModification).HasColumnName("DateModification");
            modelBuilder.Entity<Article>().Property(t => t.accederRevision).HasColumnName("Revision");
            modelBuilder.Entity<Article>().Property(t => t.accederIdContributeur).HasColumnName("IdContributeur");

            modelBuilder.Entity<Article>().Property(t => t.accederTitre).IsUnicode(true).HasMaxLength(50).IsRequired(); 
            modelBuilder.Entity<Article>().Property(t => t.accederContenu).IsUnicode(true).HasMaxLength(4000);
            modelBuilder.Entity<Article>().Property(p => p.accederDateModification).HasColumnType("datetime");
            modelBuilder.Entity<Article>().Property(p => p.accederRevision).HasColumnType("INT");
            modelBuilder.Entity<Article>().Property(p => p.accederIdContributeur).HasColumnType("INT");
            
            //////

            modelBuilder.Entity<Utilisateur>().ToTable("Utilisateurs", "dbo"); //creer table Utilisateur
            modelBuilder.Entity<Utilisateur>().Property(t => t.Id).HasColumnName("Id").HasColumnType("INT");
            modelBuilder.Entity<Utilisateur>().HasKey(t => t.Id);
            modelBuilder.Entity<Utilisateur>().Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            modelBuilder.Entity<Utilisateur>().Property(t => t.accederPrenom).HasColumnName("Prenom");
            modelBuilder.Entity<Utilisateur>().Property(t => t.accederNomFamille).HasColumnName("NomFamille");
            modelBuilder.Entity<Utilisateur>().Property(t => t.accederCourriel).HasColumnName("Courriel");
            modelBuilder.Entity<Utilisateur>().Property(t => t.accederMDP).HasColumnName("MDP");
            modelBuilder.Entity<Utilisateur>().Property(t => t.accederLangue).HasColumnName("Langue");
            modelBuilder.Entity<Utilisateur>().Ignore(t => t.accederConfirmation);

            modelBuilder.Entity<Utilisateur>().Property(t => t.accederPrenom).IsUnicode(true).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Utilisateur>().Property(t => t.accederNomFamille).IsUnicode(true).HasMaxLength(50);
            modelBuilder.Entity<Utilisateur>().Property(p => p.accederCourriel).IsUnicode(true).HasMaxLength(50);
            modelBuilder.Entity<Utilisateur>().Property(p => p.accederMDP).IsUnicode(true).HasMaxLength(500);
            modelBuilder.Entity<Utilisateur>().Property(p => p.accederLangue).IsUnicode(true).HasMaxLength(50);



        }

        public class EntitiesInitialize : CreateDatabaseIfNotExists<EntitiesContext> {
            protected override void Seed(EntitiesContext context) {
                base.Seed(context);
                context.ArticleEntities.Add(new Article("article1", "WIKI", new DateTime(2017 - 08 - 09))); //nouveau article
                context.UtilisateurEntities.Add(new Utilisateur("Prenom1", "Nom1", "courriel1@a.a", "aaaaaa", "fr")); //nouvel utilisateur
                context.SaveChanges();
                //var initializer = new EntitiesInitialize(); //nouvelle instance de la classe
                //initializer.InitializeDatabase(new EntitiesContext());
            }
        }

        public Article Find(int id) {
            EntitiesContext context = new EntitiesContext();
            context.Database.Log = Console.Write;
            Article a = context.ArticleEntities.Where(m => m.ArticleId.Equals(id)).FirstOrDefault();
            return a;
        }

        public Article Find(string titre) {
            EntitiesContext context = new EntitiesContext();
            context.Database.Log = Console.Write;
            Article a = context.ArticleEntities.Where(m => m.accederTitre.Equals(titre)).FirstOrDefault();
            return a;
        }

        public Utilisateur FindById(int id) {
            EntitiesContext context = new EntitiesContext();
            context.Database.Log = Console.Write;
           Utilisateur u = context.UtilisateurEntities.Where(m => m.Id.Equals(id)).FirstOrDefault();
            return u;
        }

        public Utilisateur FindByCourriel(string email) {
            EntitiesContext context = new EntitiesContext();
            context.Database.Log = Console.Write;
            Utilisateur u = context.UtilisateurEntities.Where(m => m.accederCourriel.Equals(email)).FirstOrDefault();
            return u;
        }

        public static void ajouter(Article a) {
            EntitiesContext context = new EntitiesContext();
            a.accederIdContributeur = 1;
            a.accederRevision = 0;
            a.accederDateModification = DateTime.Now;
            context.ArticleEntities.Add(a);
            context.SaveChanges();
        }

        public static void ajouter(Utilisateur u) {
            EntitiesContext context = new EntitiesContext();

            byte[] hashPassword = new UTF8Encoding().GetBytes(u.accederMDP);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            u.accederMDP = hashString;

            context.UtilisateurEntities.Add(u);
            context.SaveChanges();
        }

        public static void update(Article a) {
            EntitiesContext context = new EntitiesContext();
            //string courriel = User.Identity.Name;
            //Utilisateur u = Utilisateurs.FindByCourriel(courriel);
            //int id = u.Id;
            a.accederIdContributeur = 1;//id;

            context.SaveChanges();
        }

        public static void update(Utilisateur u) {
            EntitiesContext context = new EntitiesContext();

            byte[] hashPassword = new UTF8Encoding().GetBytes(u.accederMDP);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            u.accederMDP = hashString;

            context.SaveChanges();
        }


        public static void supprimer(Article a) {
            EntitiesContext context = new EntitiesContext();
            context.ArticleEntities.Remove(a);
            context.SaveChanges();
        }

        public void supprimer(Utilisateur u) {
            EntitiesContext context = new EntitiesContext();
            context.UtilisateurEntities.Remove(u);
            context.SaveChanges();
        }

        //public void Ajour(Utilisateur u, UtilisateurProfil up) {
        //    EntitiesContext context = new EntitiesContext();
        //  //  a = context.ArticleEntities.First(s => s.ArticleId.Equals(a.ArticleId));
        //  //  a.accederTitre = a.accederTitre;

        //}

        //public static bool Ajour(Utilisateur u, UtilisateurProfil up) {

        //    using (SqlConnection connexion = new SqlConnection(ConnectionString)) {


        //        string requete = "UPDATE Utilisateurs SET Prenom = '" + up.Prenom + "', NomFamille = '"
        //            + up.NomFamille + "', Langue = '" + up.Langue + "' WHERE Id = " + u.Id;

        //        SqlCommand cmd = new SqlCommand(requete, connexion);
        //        cmd.CommandType = System.Data.CommandType.Text;
        //        try {
        //            connexion.Open();
        //            cmd.ExecuteNonQuery();
        //            return true;

        //        }
        //        catch (Exception e) {
        //            string Message = e.Message;
        //            return false;
        //        }
        //        finally {
        //            connexion.Close();
        //        }

        //    }
        //}

        //public void Update(Article a) { //???
        //    var entity = Find(a.ArticleId);
        //    if (entity == null) {
        //        return;
        //    }

        //    Entry(entity).CurrentValues.SetValues(a);
        //}


        //   public class Database {
        //    var initializer = new CreateDatabaseIfNotExists<EntitiesContext>();
        //    initializer.InitializeDatabase(new EntitiesContext());
        //}


        //public class EntitiesContext : Controller
        //{
        //    // GET: EntitiesContext
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }
        //}
    }
}