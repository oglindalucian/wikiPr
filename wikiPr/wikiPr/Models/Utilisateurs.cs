using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using wikiPr.Models.Views;

namespace wikiPr.Models {

    public class Utilisateurs {

        public static bool Add(Utilisateur u) { 

           

            bool TEST = true;
            byte[] hashPassword = new UTF8Encoding().GetBytes(u.accederMDP);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);         
            string requete = 
           "INSERT INTO Utilisateurs(MDP, Prenom, NomFamille, Courriel, Langue) VALUES ('" + hashString + "', '" + u.accederPrenom + "', '" + u.accederNomFamille +
           "', '" + u.accederCourriel + "', '" + u.accederLangue + "')";
            SqlConnection connexion = new SqlConnection(ConnectionString);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                commande.ExecuteNonQuery();
            }
            catch (Exception e) {
                string msg = e.Message;
                TEST = false;
            }
            finally {
                connexion.Close();
            }
            return TEST;
        }

        public static bool Update(Utilisateur u) {
            using (SqlConnection connexion = new SqlConnection(ConnectionString)) {
               

                string requete = "UPDATE Utilisateurs SET Prenom = '" + u.accederPrenom + "', NomFamille = '"
                    + u.accederNomFamille + "', Langue = '" + u.accederLangue + "' WHERE Id = " + u.Id;      

                SqlCommand cmd = new SqlCommand(requete, connexion);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    connexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    string Message = e.Message;
                    return false;
                }
                finally {
                    connexion.Close();
                }

            }
        }

        public static bool Ajour(Utilisateur u, UtilisateurProfil up) {
            using (SqlConnection connexion = new SqlConnection(ConnectionString)) {
               

                string requete = "UPDATE Utilisateurs SET Prenom = '" + up.Prenom + "', NomFamille = '"
                    + up.NomFamille + "', Langue = '" + up.Langue + "' WHERE Id = " + u.Id;     

                SqlCommand cmd = new SqlCommand(requete, connexion);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    connexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    string Message = e.Message;
                    return false;
                }
                finally {
                    connexion.Close();
                }

            }
        }

        public static string hacherMot(string s) {
            byte[] hashPassword = new UTF8Encoding().GetBytes(s);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            return hashString;
        }

        public static bool Ajour(Utilisateur u, UtilisateurMP ump) {
            using (SqlConnection connexion = new SqlConnection(ConnectionString)) {
                byte[] hashPassword = new UTF8Encoding().GetBytes(ump.MDP2);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
                string hashString = BitConverter.ToString(hash);

                string requete = "UPDATE Utilisateurs SET MDP = '" + hashString + "' WHERE Id = " + u.Id; //etait hashString  ump.MDP2

                SqlCommand cmd = new SqlCommand(requete, connexion);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    connexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    string Message = e.Message;
                    return false;
                }
                finally {
                    connexion.Close();
                }

            }
        }

        public static bool updatelangue(Utilisateur u) {
            using (SqlConnection connexion = new SqlConnection(ConnectionString)) {
               

                string requete = "UPDATE Utilisateurs SET Langue = '" + u.accederLangue
                     + "' WHERE Id = " + u.Id;      

                SqlCommand cmd = new SqlCommand(requete, connexion);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    connexion.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    string Message = e.Message;
                    return false;
                }
                finally {
                    connexion.Close();
                }

            }
        }

       


        public static bool updates(Utilisateur u) {
         
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
                byte[] hashPassword = new UTF8Encoding().GetBytes(u.accederMDP);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
                string hashString = BitConverter.ToString(hash);

                string requete = "UPDATE Utilisateurs SET MDP = '" + hashString + "' WHERE Id = " + u.Id;    

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    string Message = e.Message;
                    return false;
                }
                finally {
                    cnx.Close();
                }

            }
        }






       

        public void Remove(int id) {
            using (SqlConnection connexion = new SqlConnection(ConnectionString)) {
                connexion.Open();
                SqlCommand commande = new SqlCommand("DeleteUtilisateur", connexion);
                commande.CommandType = CommandType.StoredProcedure;
                commande.Parameters.Add("Id", SqlDbType.Int).SqlValue = id;

                try {
                    commande.ExecuteNonQuery();
                }
                catch (Exception e) {
                    throw new Exception("Erreur de suppression", e);
                }
            }

        }

       


        public static Utilisateur FindByCourriel(string courriel) {
         
            SqlConnection connexion = new SqlConnection(ConnectionString);
            string requete = "SELECT * FROM Utilisateurs WHERE Courriel = '" + courriel + "';";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                dr.Read();
                Utilisateur u = new Utilisateur {
                    Id = (int)dr["Id"],
                    accederPrenom = (string)dr["Prenom"],
                    accederNomFamille = (string)dr["NomFamille"],
                    accederCourriel = (string)dr["Courriel"],
                    accederMDP = (string)dr["MDP"],
                    accederLangue = (string)dr["Langue"]
                };

                return u;
            }
            catch (Exception e) {
                string Message = e.Message;
            }
            finally {
                connexion.Close();
            }
            return null;
        }


      public Utilisateur FindById(int id) {
        Utilisateur u = null;
        using (SqlConnection connexion = new SqlConnection(ConnectionString)) {
            connexion.Open();

            //Création d'une commande
            SqlCommand commande = new SqlCommand("FindUtilisateurById", connexion);
            commande.CommandType = CommandType.StoredProcedure;
            commande.Parameters.Add("Id", SqlDbType.Int).SqlValue = id;

            //Traitement du DataReader
            SqlDataReader dr = commande.ExecuteReader();
            dr.Read();
            u = ExtraireUtilisateur(dr);
            dr.Close();
        }

        return u;
    }

    private static Utilisateur ExtraireUtilisateur(IDataReader dr) {
        Utilisateur u = new Utilisateur();
        u.Id = (int)dr["Id"];
        u.accederCourriel = (string)dr["Courriel"];
        u.accederMDP = (string)dr["MDP"];
        u.accederNomFamille = (string)dr["NomFamille"];
        u.accederPrenom = (string)dr["Prenom"];
        u.accederCourriel = (string)dr["courriel"];
        u.accederLangue = (string)dr["Langue"];
        return u;
    }

        
        public static bool Authentifie(string login, string passwd) {
         //  string conStr = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(ConnectionString)) {
               
                string requete = "SELECT * FROM Utilisateurs WHERE Courriel = '" + login + "'";

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) {
                        dataReader.Close();
                        return false;
                    }
                    dataReader.Read();
                  
                    var encodedPasswordOnServer = (string)dataReader["MDP"];

                    byte[] encodedPassword = new UTF8Encoding().GetBytes(passwd);
                    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                    string encodedPasswordSentToForm = BitConverter.ToString(hash);

                    dataReader.Close();
                    return encodedPasswordSentToForm.Trim() == encodedPasswordOnServer.Trim();
                   // return passwd.Trim() == encodedPasswordOnServer.Trim();
                }

                catch (Exception e) {
                   
                    return false;
                }

                finally {
                    cnx.Close();
                }
            }
        }       

        public static string ancienMp(Utilisateur u) {
            byte[] hashPassword = new UTF8Encoding().GetBytes(u.accederMDP);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            return hashString;          
        }

       

        private static string ConnectionString {
        get { return ConfigurationManager.ConnectionStrings["WikiCon"].ConnectionString; }
    }
}
}
    
