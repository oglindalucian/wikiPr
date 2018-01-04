using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using wikiPr.Models;
using wikiPr.Models.Views;
using wikiPr.Ressource;
using wikiPr.Controllers;

namespace wikiPr.Controllers
{
    public class UtilisateursController : Controller
    {
        // GET: Utilisateurs

        string str;

       public static void CreateCulture(string str) {
            if (str.IndexOf("fr") != -1) {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("fr");

            }
            if (str.IndexOf("en") != -1) {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en");

            }
            else {

                Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("es");
            }

        }

        public ActionResult Connexion() {
            ViewBag.error = "";
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);
            return View();
        }

        [HttpPost]
        public ActionResult Connexion(string username, string password, string ReturnUrl = "") {
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);

            ViewBag.error = "";
            ViewBag.ReturnUrl = ReturnUrl;
            if (!Utilisateurs.Authentifie(username, password)) {
                ViewBag.error = "Nom d'utilisateur ou mot de passe invalide!";
                return View();
            }
            else {
                FormsAuthentication.SetAuthCookie(username, false);
                if (ReturnUrl == "") {
                    return RedirectToAction("index", "home");
                }
                else {
                    return Redirect(ReturnUrl);
                }
            }
        }




        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public ActionResult Inscription() { //view de UtilisateurInscription

          //  Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
           // UtilisateurInscription ui = new UtilisateurInscription();
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
          //  if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);
            return View();
        }

        [HttpPost]
        public ActionResult Inscription(UtilisateurInscription ui) {
            Utilisateur u = new Utilisateur(ui);
            if (ModelState.IsValid) { 
               // Utilisateurs.Add(u);
                EntitiesContext.ajouter(u);
                return RedirectToAction("index", "home");
            }
            return View();
        }

        

        public ActionResult Profil() { 
            string nom = User.Identity.Name;
            Utilisateur u = Utilisateurs.FindByCourriel(nom);
            UtilisateurProfil up = new UtilisateurProfil(u);
           
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);
            return View(up);

        }

        [HttpPost]
        public ActionResult Profil(UtilisateurProfil up) {
            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name); 
            if (ModelState.IsValid) {
               Utilisateurs.Ajour(u, up);
               return RedirectToAction("index", "home");
           }
           
           return View();         
            
        }

        public ActionResult ModifierMdP() {
            
            string courriel = User.Identity.Name;

            Utilisateur u = Utilisateurs.FindByCourriel(courriel);
            UtilisateurMP ump = new UtilisateurMP();

            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);

            return View(ump);
        }

        [HttpPost]
        public ActionResult ModifierMdP(UtilisateurMP ump) {
            string er = "";
            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
                str = cookie;
            }
            else CreateCulture(str);

            if (str.IndexOf("fr") != -1) {
                er = "Le mot de passe fourni est incorrect.";

            }
            if (str.IndexOf("en") != -1) {
                er = "Wrong password.";

            }
            else if (str.IndexOf("es") != -1) {

                er = "Contraseña incorrecta.";
            }
            
            if (Utilisateurs.hacherMot(ump.MDP1).Trim() == u.accederMDP.Trim()) {
          // if((ump.MDP1).Trim() == u.accederMDP.Trim()) { 
                ViewBag.error = "";
                if (ModelState.IsValid) {
                    Utilisateurs.Ajour(u, ump);
                    return RedirectToAction("Index", "home");
                }               
            }
            else { ViewBag.error = er; }
            return View();
        }

    }
}