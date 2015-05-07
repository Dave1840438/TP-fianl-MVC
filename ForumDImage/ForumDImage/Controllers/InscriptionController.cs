using ForumDImage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumDImage.Controllers
{
    public class InscriptionController : Controller
    {
        //
        // GET: /Inscription/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Utilisateur user)
        {
            if (ModelState.IsValid)
            {
                Dal dataBase = new Dal();
                dataBase.AjouterUtilisateur(user.NomUtilisateur, user.MotDePasse, user.NomComplet, user.Email);

                return View("Success");
            }
            else
                return View();
        }

        public ActionResult Lister()
        {
            Dal dataBase = new Dal();

            return View(dataBase.ListerUtilisateur());
        }
	}
}