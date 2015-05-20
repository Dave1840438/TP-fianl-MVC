using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ForumDImage.Models;

namespace ForumDImage.Controllers
{
    public class ZoneClientsController : Controller
    {
        //
        // GET: /ZoneClients/
        public ActionResult Index()
        {
            Dal dal = new Dal();
            ViewBag.Photos = dal.ListerPhotos();
            return View();
        }

        [HttpGet]
        public ActionResult PublierPhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PublierPhoto(String commentaire)
        {
            if (Request.Files.Count == 1 && HttpContext.User.Identity.IsAuthenticated)
            {
                Dal dal = new Dal();
                byte[] Image = null;
                using (var binaryReader = new BinaryReader(Request.Files[0].InputStream))
                {
                    Image = binaryReader.ReadBytes(Request.Files[0].ContentLength);
                    Utilisateur currentUser = dal.recupererUtilisateur(HttpContext.User.Identity.Name);
                    dal.AjouterPhoto(currentUser, Image, commentaire);
                }
                return View();
            }
            return View();
        }

    }
}