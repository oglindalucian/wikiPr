using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wikiPr.Models.Views {
    public class UtilisateursConnexion {
        [Required(ErrorMessage = "Indiquez votre courriel")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Adresse Courriel")]
        public string Courriel { get; set; }

        [Required(ErrorMessage = "Indiquez le mot de passe"), StringLength(50, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passse")]
        public string MDP { get; set; }

       
    }
}