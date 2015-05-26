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
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "ZoneClients");

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

        public ActionResult Success()
        {
            return RedirectToAction("Index", "Login");
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