using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ForumDImage.Models
{
    [Table("Votes")]
    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public virtual Utilisateur Utilisateur { get; set; }

        [Required]
        public virtual Photo Photo { get; set; }
    }
}