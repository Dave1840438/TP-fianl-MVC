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

        Dal dal;
        public InscriptionController()
        {
            dal = new Dal();
        }

        //
        // GET: /Inscription/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ViewModels.UserValidationModel user)
        {
            ViewBag.Message = "";
            if (ModelState.IsValid)
            {
                if (!dal.usagerExiste(user.NomUtilisateur))
                {
                    dal.AjouterUtilisateur(user.NomUtilisateur, user.MotDePasse, user.NomComplet, user.Email);
                    return View("Success");
                }
                else
                    ViewBag.Message = "Le nom d'usager est déjà pris!";
            }
            
            return View();
        }

        public ActionResult Lister()
        {
            Dal dataBase = new Dal();

            return View(dataBase.ListerUtilisateur());
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