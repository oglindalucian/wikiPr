using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wikiPr.Models.Views {
    public class UtilisateurProfil {
        [Required(ErrorMessage = "Indiquez votre prenom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Indiquez votre nom de famille")]
        public string NomFamille { get; set; }

        [Required(ErrorMessage = "Choisissez la langue")]
        public string Langue { get; set; }

        public UtilisateurProfil (Utilisateur u) {
            this.Prenom = u.accederPrenom;
            this.NomFamille = u.accederNomFamille;
            this.Langue = u.accederLangue;
        }

        public UtilisateurProfil() { }
    }
}