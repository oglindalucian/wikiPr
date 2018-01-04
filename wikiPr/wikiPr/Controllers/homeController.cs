using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using wikiPr.Models;
using wikiPr.Ressource;

namespace wikiPr.Controllers
{
    public class homeController : Controller
    {
        // GET: home
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

        public ActionResult Index() {
            ViewBag.lesArticles = Article.lesArticles();
            
            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);

            return View(Article.lesArticles());
        }

        [HttpPost]
        public ActionResult Index (string s) {
            string titre = Request.Form["texte"];
            ViewBag.lesArticles = Article.lesArticles();
            foreach (Article a in ViewBag.lesArticles) {
                if (a.accederTitre == titre) {
                    return RedirectToAction("afficher", "home", new { Titre = titre });
                }
            }       
                
            return RedirectToAction("ajouter", "home", new { Titre = titre });

        }
       

        public ActionResult afficher(string titre) {
            Article a = Article.Find(titre);
            ViewBag.letitre = titre;
            ViewBag.lesArticles = Article.lesArticles();

            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);

            return View(a);
        }

        [Authorize]
        public ActionResult ajouter(string titre) {
            ViewBag.lesArticles = Article.lesArticles();

            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);

            if (titre == null) { return View(new Article()); }
            else return View(new Article(titre));

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ajouter(Article a) {
             Article.Add(a);
            //EntitiesContext.ajouter(a);
            string courriel = User.Identity.Name;
            Utilisateur u = Utilisateurs.FindByCourriel(courriel);
            int id = u.Id;
            Article.Update(a, id);
            return RedirectToAction("afficher", new { Titre = a.accederTitre });
            }


        public ActionResult ajouterApercu() {
            ViewBag.apercu = Request.Form["acopier"];
            string texte = ViewBag.apercu;
            Article a = new Article("-1", texte);
           
            return View(a);
        }

       

        [Authorize]
        public ActionResult modifier(string titre) {
            ViewBag.titre = titre;
            ViewBag.lesArticles = Article.lesArticles();

            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);


            return View(Article.Find(titre));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult modifier(Article a) {
            string courriel = User.Identity.Name;
            Utilisateur u = Utilisateurs.FindByCourriel(courriel);
            int id = u.Id;
            Article.Update(a, id);
            //EntitiesContext.update(a);

            return RedirectToAction("afficher", new { Titre = a.accederTitre });
        }

        [Authorize]
        public ActionResult supprimer(string titre) {
            ViewBag.lesArticles = Article.lesArticles();
            Article a = Article.Find(titre);

            Utilisateur u = Utilisateurs.FindByCourriel(User.Identity.Name);
            str = Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"];
            if (User.Identity.IsAuthenticated && u != null) str = u.accederLangue;
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }
            else CreateCulture(str);


            return View(a);
        }

        [HttpPost]
        public ActionResult supprimer(Article a) {
            Article.Delete(a);
            //EntitiesContext.supprimer(a);
            return RedirectToAction("Index");
        }

        public ActionResult apropos() {
            string cookie = "";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) {
                cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value;
                ViewBag.cookie = cookie;
                CreateCulture(cookie);
            }

            return View();
        }

        [ChildActionOnly()]
        public ActionResult ViewUp() {
            return PartialView();
        }

        [HttpPost]
        public ActionResult ViewUp(string l) {
            string langue = Request.Form["lalang"];
            HttpCookie cookie = new HttpCookie("Cookie");
            cookie.Value = langue;
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            
            return new RedirectResult(Request.UrlReferrer.AbsoluteUri);
        }

        [ChildActionOnly()]
        public ActionResult ViewListe() {
            ViewBag.lesArticles = Article.lesArticles();
            return PartialView(ViewBag.lesArticles);
        }      

    }
}
 
 