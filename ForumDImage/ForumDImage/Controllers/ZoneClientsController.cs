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
            const int NB_PHOTOS_PAR_PAGE = 3;
            int page = _page != null && (int)_page > 0  ? (int)_page : 1;

            List<Photo> listeDesPhotos = dal.ListerPhotos();

            page--;

            if (page * NB_PHOTOS_PAR_PAGE >= listeDesPhotos.Count)
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


        [HttpGet]
        public ActionResult PublierPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PublierPhoto(String Titre, String commentaire)
        {
            ViewBag.Message = "";
            if (Request.Files.Count == 1 && HttpContext.User.Identity.IsAuthenticated)
            {
                String fileName = Request.Files[0].FileName;
                //FileInfo file = new FileInfo(Request.Files[0].FileName);
                //FileType type = file.GetFileType();
                //Temporary fix
                //if (type == Detective.GIF || type == Detective.JPEG || type == Detective.PNG)
                if (fileName.EndsWith(".jpg") || fileName.EndsWith(".pgn") || fileName.EndsWith(".gif"))
                {
                    try 
                    {
                        byte[] Image = null;
                        using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                        {
                            Image = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                            Utilisateur currentUser = dal.recupererUtilisateur(HttpContext.User.Identity.Name);
                            dal.AjouterPhoto(currentUser, Titre, Image, commentaire);
                        }
                        return View();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Le fichier est endommagé ou corrompu";
                    }
                }
                else
                {
                    ViewBag.Message = "Mauvais type de fichier";
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
            return View();
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

        public ActionResult Test()
        {
            return View();
        }

    }
}