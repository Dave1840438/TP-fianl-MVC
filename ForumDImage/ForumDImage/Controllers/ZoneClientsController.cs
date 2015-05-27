using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ForumDImage.Models;
using FileTypeDetective;

namespace ForumDImage.Controllers
{
    [Authorize]
    public class ZoneClientsController : Controller
    {
        Dal dal;
        public ZoneClientsController()
        {
            dal = new Dal();
        }

        //
        // GET: /ZoneClients/
        [HttpGet]
        public ActionResult Index(int? _page)
        {
            const int NB_PHOTOS_PAR_PAGE = 5;
            int page = _page != null && (int)_page > 0 ? (int)_page : 1;

            List<Photo> listeDesPhotos = dal.ListerPhotos();

            page--;

            if (page * NB_PHOTOS_PAR_PAGE >= listeDesPhotos.Count && listeDesPhotos.Count != 0)
                page = (int)Math.Ceiling((double)listeDesPhotos.Count / (double)NB_PHOTOS_PAR_PAGE) - 1;

            int nbPhotosRestantes = listeDesPhotos.Count - (page * NB_PHOTOS_PAR_PAGE);

            if (nbPhotosRestantes > NB_PHOTOS_PAR_PAGE)
                nbPhotosRestantes = NB_PHOTOS_PAR_PAGE;

            ViewBag.Photos = listeDesPhotos.GetRange(page * NB_PHOTOS_PAR_PAGE, nbPhotosRestantes);
            ViewBag.page = page + 1;
            ViewBag.HasNextPage = listeDesPhotos.Count > (page + 1) * NB_PHOTOS_PAR_PAGE;
            ViewBag.HasPreviousPage = page != 0;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string photoID, int _page = 1)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                dal.ajouterUnVote(HttpContext.User.Identity.Name, photoID);
            return RedirectToAction("Index", new { _page = _page });
        }


        public void FaireUnVote(string photoID)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                dal.ajouterUnVote(HttpContext.User.Identity.Name, photoID);
        }


        [HttpGet]
        public ActionResult PublierPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PublierPhoto(String Titre, String commentaire)
        {
            const int TITLE_LENGHT = 50;
            const int COMMENT_LENGHT = 300;
            ViewBag.Message = "";

            if (ModelState.IsValid)
            {
                if (Request.Files.Count == 1 && HttpContext.User.Identity.IsAuthenticated)
                {
                    String fileName = Request.Files[0].FileName;

                    if (fileName.EndsWith(".jpg") || fileName.EndsWith(".png") || fileName.EndsWith(".gif"))
                    {
                        try
                        {
                            byte[] Image = null;
                            using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                            {
                                Image = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                                Utilisateur currentUser = dal.recupererUtilisateur(HttpContext.User.Identity.Name);
                                if (Titre.Length > TITLE_LENGHT)
                                    ViewBag.Message = "Le titre ne doit pas dépasser " + TITLE_LENGHT + " caractères!";
                                if (Titre.Length == 0)
                                    ViewBag.Message = "Le titre ne doit pas être vide!";
                                else if (commentaire.Length > COMMENT_LENGHT)
                                    ViewBag.Message = "Le commentaire ne doit pas dépasser " + COMMENT_LENGHT + " caractères!";
                                else
                                {
                                    dal.AjouterPhoto(currentUser, Titre, Image, commentaire);
                                    return RedirectToAction("Index");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = "Le fichier est endommagé ou corrompu!";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Mauvais type de fichier!";
                    }
                }
            }

            return View();
        }

        public ActionResult getNbVotes(String photoId)
        {
            ViewBag.NbVotes = dal.getNbVotes(photoId);
            return PartialView("getNbVotes");
        }


        [HttpPost]
        public ActionResult SupprimerPhoto(String photoId, int _page = 1)
        {
            dal.SupprimerUnePhoto(photoId);
            return RedirectToAction("Index", new { _page = _page });
        }

        [HttpGet]
        public ActionResult ModifierUtilisateur()
        {
            Utilisateur user = dal.recupererUtilisateur(HttpContext.User.Identity.Name);
            ForumDImage.ViewModels.UserValidationModel userView = new ViewModels.UserValidationModel
            {
                Id = user.Id,
                NomUtilisateur = user.NomUtilisateur,
                NomComplet = user.NomComplet,
                MotDePasse = user.MotDePasse,
                ConfirmMotDePasse = user.MotDePasse,
                Email = user.Email,
                ConfirmEmail = user.Email
            };
            return View(userView);
        }

        [HttpPost]
        public ActionResult ModifierUtilisateur(ViewModels.UserValidationModel user)
        {
            if (ModelState.IsValid)
            {
                dal.ModifierUtilisateur(user.Id.ToString(), user.NomUtilisateur, user.MotDePasse, user.NomComplet, user.Email);
                return View("ModificationEffectuee");
            }
            else
            {
                Utilisateur userR = dal.recupererUtilisateur(HttpContext.User.Identity.Name);
                ForumDImage.ViewModels.UserValidationModel userView = new ViewModels.UserValidationModel
                {
                    Id = userR.Id,
                    NomUtilisateur = userR.NomUtilisateur,
                    NomComplet = userR.NomComplet,
                    MotDePasse = userR.MotDePasse,
                    ConfirmMotDePasse = userR.MotDePasse,
                    Email = userR.Email,
                    ConfirmEmail = userR.Email
                };
                return View(userView);
            }
        }

        public ActionResult ModificationEffectuee()
        {
            return RedirectToAction("Index", "ZoneClients");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dal.Dispose();
            }

            base.Dispose(disposing);
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}