using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wikiPr.Models.Views {
    public class UtilisateurMP {

        [Required(ErrorMessage = "Indiquez le mot de passe ancien"), StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe ancien")]
        public string MDP1 { get; set; }

        [Required(ErrorMessage = "Indiquez le mot de passe nouveau"), StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe nouveau")]
        public string MDP2 { get; set; }

        public UtilisateurMP() { }

        public UtilisateurMP(Utilisateur u) {
            this.MDP1 = u.accederMDP;
        }

    }
}