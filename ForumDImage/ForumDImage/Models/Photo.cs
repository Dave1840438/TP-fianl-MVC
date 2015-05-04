using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumDImage.Models
{
    [Table("Photos")]
    public class Photo
    {
        public virtual Utilisateur Auteur;

        [Required]
        public byte[] Image;

        [Required]
        public DateTime Date;

        [Required, StringLength(300)]
        public string Commentaire;
    }
}