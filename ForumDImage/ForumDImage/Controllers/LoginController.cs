using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumDImage.Models;
using System.Web.Security;

namespace ForumDImage.Controllers
{
    public class LoginController : Controller
    {
        Dal dal;
        public LoginController()
        {
            dal = new Dal();
        }



        //
        // GET: /Login/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Message = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(ForumDImage.Models.Utilisateur user)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = dal.Authentifier(user.NomUtilisateur, user.MotDePasse);
                if (utilisateur != null)
                {
                    FormsAuthentication.SetAuthCookie(utilisateur.Id.ToString(), false);
                    return RedirectToAction("Index", "ZoneClients");
                }
            }

            ViewBag.Message = "Nom d'usager ou mot de passe incorrect";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dal.Dispose();
            }
            
            base.Dispose(disposing);
        }

	}
}