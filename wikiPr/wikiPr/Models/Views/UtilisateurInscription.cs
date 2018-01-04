using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wikiPr.Models.Views {
    public class UtilisateurInscription {

        [Required(ErrorMessage = "Indiquez votre prenom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Indiquez votre nom de famille")]
        public string NomFamille { get; set; }

        
        [Required(ErrorMessage = "Indiquez votre courriel")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse Courriel")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Indiquez le mot de passe"), StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passse")]
        public string MDP { get; set; }

        [Required(ErrorMessage = "Confirmez le mot de passe!"), StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        //[Display(Name = "Confirmez le mot de passse:")]
        [Compare("MDP", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string Confirmation { get; set; }

        [Required(ErrorMessage = "Choisissez la langue")]
        public string Langue { get; set; }

        public UtilisateurInscription(Utilisateur u) {
            this.Prenom = u.accederPrenom;
            this.NomFamille = u.accederNomFamille;
            this.Courriel = u.accederCourriel;
            this.MDP = u.accederMDP;
            this.Confirmation = u.accederConfirmation;
            this.Langue = u.accederLangue;
        }

        public UtilisateurInscription() { }
    }
}