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
        
        [Required, Display(Name="Titre :")]
        public string Titre { get; set; }

        [Required, Display(Name="Image :")]
        public byte[] Image { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(300), Display(Name="Commentaire :")]
        public string Commentaire { get; set; }
    }
}