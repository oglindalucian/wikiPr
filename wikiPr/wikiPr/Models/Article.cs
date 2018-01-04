using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using wikiPr.Ressource;

namespace wikiPr.Models {
    public class Article {

        public int ArticleId { get; set; }
        // [Display(Name = "Titre", ResourceType = typeof(ResourceView))]
        [Required(ErrorMessage = "Le titre est requis ")]
        [Display(Name = "Titre")]
        private string Titre { get; set; }

        //[Display(Name = "Contenu de l'article", ResourceType = typeof(ResourceView))]
        [Display(Name = "Contenu de l'article")]
        private string Contenu { get; set; }

        // [Display(Name = "Dernière date de modification", ResourceType = typeof(ResourceView))]
        [Display(Name = "Dernière date de modification")]
        private DateTime DateModification { get; set; }

        //[Display(Name = "Révision", ResourceType = typeof(ResourceView))]
        [Display(Name = "Révision")]
        private int Revision { get; set; }

        // [Display(Name = "Id du contributeur", ResourceType = typeof(ResourceView))]
        [Display(Name = "Id du contributeur")]
        public int IdContributeur { get; set; }

        public string accederTitre {
            get { return Titre; }
            set { Titre = value; }
        }

        public string accederContenu {
            get { return Contenu; }
            set { Contenu = value; }
        }

        public DateTime accederDateModification {
            get {return DateModification;}
            set { DateModification = value; }
        }

        public int accederRevision {
            get { return Revision; }
            set { Revision = value; }
        }

        public int accederIdContributeur {
            get { return IdContributeur; }
            set { IdContributeur = value; }
        }


        public Article() {}

        public Article(string titre) {
            this.accederTitre = titre;
        }

        public Article(string titre, string contenu) {
            this.accederTitre = titre;
            this.accederContenu = contenu;
        }

        public Article (string titre, string contenu, DateTime d) {
            this.accederTitre = titre;
            this.accederContenu = contenu;
            this.accederDateModification = d;
            //this.accederRevision = revision;
            //this.accederIdContributeur = id;
        }

        public static List<Article> lesArticles() {
            string chConnexion = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "dbo.GetTitresArticles";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.StoredProcedure;
            List<Article> maListe = new List<Article>();
            try {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                while (dr.Read()) {
                    Article a = new Article {
                        accederTitre = (string)dr["Titre"],
                        accederContenu = (string)dr["Contenu"],
                        accederDateModification = (DateTime)dr["DateModification"],
                        accederRevision = (int)dr["Revision"],
                        accederIdContributeur = (int)dr["IdContributeur"]
                    };
                    maListe.Add(a);
                }

                dr.Close();
                return maListe;
            }
            catch (Exception e) {
                string Message = e.Message;
            }
            finally {
                connexion.Close();
            }
            return null;
        }
        

        public static void Add(Article a) {
            string chConnexion = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "INSERT INTO Article (Titre, Contenu, DateModification, Revision, IdContributeur) VALUES ('"
            + a.accederTitre + "','" + a.accederContenu + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," + a.accederRevision + "," + 1 + ")";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
            catch { }
        }

        public static Article Find (string titre) {
            string chConnexion = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "SELECT * FROM Article WHERE Titre = '" + titre + "'";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                dr.Read();
                Article a = new Article {
                    accederTitre = (string)dr["Titre"],
                    accederContenu = (string)dr["Contenu"],
                    accederDateModification = (DateTime)dr["DateModification"],
                    accederRevision = (int)dr["Revision"],
                    accederIdContributeur = (int)dr["IdContributeur"]                    
                };                
                return a;
            }
            catch (Exception e) {
                string Message = e.Message;
            }
            finally {
                connexion.Close();
            }
            return null;
        }

       

        public static void Update(Article a, int id) {
            string chConnexion = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "UPDATE Article SET Titre = '" + a.accederTitre +
                 "', DateModification = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', Contenu = '" + a.accederContenu + "', IdContributeur = " + id
                 + " WHERE Titre = '" + a.accederTitre + "'";

            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
            catch { }
        }

        public static void Delete(Article a) {
            string chConnexion = ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "DELETE FROM Article WHERE Titre = '" + a.accederTitre + "'";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
            catch { }
        }

    }
}