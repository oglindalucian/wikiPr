using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using wikiPr.Models.Views;
using wikiPr.Ressource;


namespace wikiPr.Models {
    public class Utilisateur {
             
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Indiquez votre prenom")]
       // [Display(Name = "Prenom", ResourceType = typeof(ResourceView))]
        [Display(Name = "Prenom")]
        private string Prenom { get; set; }

        [Required(ErrorMessage = "Indiquez votre nom de famille")]
       // [Display(Name = "Nom de famille", ResourceType = typeof(ResourceView))]
        [Display(Name = "Nom de famille")]     
        private string NomFamille { get; set; }

        [Required(ErrorMessage = "Indiquez votre courriel")]
        //  [Display(Name = "Adresse courriel", ResourceType = typeof(ResourceView))]
        [Display(Name = "Adresse courriel")]       
        [DataType(DataType.EmailAddress)]
         private string Courriel { get; set; }

        
        
        [Required, StringLength(50, MinimumLength = 6, ErrorMessage = "Le mot de passe doit compter minimum 6 caractères et maximum 50!")]
        [DataType(DataType.Password)]               
       // [Display(Name = "Mot de passe", ResourceType = typeof(ResourceView))]
        [Display(Name = "Mot de passse")]
        private string MDP { get; set; }

        [Required, StringLength(50, MinimumLength = 6, ErrorMessage = "Confirmez le mot de passe: minimum 6 caractères et maximum 50!")]
        [DataType(DataType.Password)]
       //[Display(Name = "Confirmez le mot de passse:")]
         [Compare("MDP", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]        
        private string Confirmation { get; set; }

       
       // [Display(Name = "Langue", ResourceType = typeof(ResourceView))]
        [Display(Name = "Langue")]
        private string Langue { get; set; }

       
        public Utilisateur() {; }
        
        public Utilisateur(int id) {
            this.Id = id;
        }

        public Utilisateur (int id, string langue) {
            this.Id = id;
            this.Langue = langue;
        }

        public Utilisateur(UtilisateurInscription ui) {
            this.Prenom = ui.Prenom;
            this.NomFamille = ui.NomFamille;
            this.Courriel = ui.Courriel;
            this.MDP = ui.MDP;
            this.Langue = ui.Langue;
        }

        public Utilisateur(UtilisateurProfil up) {
            this.Prenom = up.Prenom;
            this.NomFamille = up.NomFamille;
            this.Langue = up.Langue;
        } 

        public Utilisateur(string p, string nf, string c, string mdp, string l) {
            this.accederPrenom = p;
            this.accederNomFamille = nf;
            this.accederCourriel = c;
            this.accederMDP = mdp;
            this.accederLangue = l;
        }
        
        public string accederPrenom {
            get { return Prenom; }
            set { Prenom = value; }
        } 
        
        public string accederNomFamille {
            get { return NomFamille; }
            set { NomFamille = value; }
        } 
        
        public string accederCourriel {
            get { return Courriel; }
            set { Courriel = value; }
        }

        public string accederMDP {
            get { return MDP; }
            set { MDP = value; }
        }

        public string accederConfirmation {
            get { return Confirmation; }
            set { Confirmation = value; }
        }

        public string accederLangue {
            get { return Langue; }
            set { Langue = value; }
        }


    }
}