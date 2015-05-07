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
        public int Id { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required, StringLength(300)]
        public string Commentaire { get; set; }
    }
}